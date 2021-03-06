﻿ using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMSFFService;
using TASDownloadService.AttProcessDaily;
using TimeAttendanceSystem.Model;

namespace WMSFFService
{
    class CreateMissingAttendance
    {
        public void CreatemissingAttendance(DateTime _startDate, DateTime _endDate)
        {

            using (var ctx = new TAS2013Entities())
            {
                List<AttData> _AttData = new List<AttData>();
                List<Emp> _Emp = new List<Emp>();
                DateTime CurrentDate = _startDate;
                _Emp = ctx.Emps.Where(aa => aa.Status == true).ToList();
                List<Remark> remarks = ctx.Remarks.ToList();
                while (CurrentDate <= _endDate)
                {
                    _AttData = ctx.AttDatas.Where(aa => aa.AttDate == CurrentDate).ToList();
                    foreach (var emp in _Emp)
                    {
                        if (_AttData.Where(aa => aa.EmpID == emp.EmpID).Count() == 0)
                        {
                            //Create Attendance
                            CreateAttendance(CurrentDate, _Emp.Where(aa => aa.EmpID == emp.EmpID && aa.JoinDate>=CurrentDate).ToList(), remarks);
                        }
                    }
                    CurrentDate = CurrentDate.AddDays(1);
                }
            }
        }

        private void CreateAttendance(DateTime dateTime, List<Emp> _emp,List<Remark> remarks)
        {
            using (var ctx = new TAS2013Entities())
            {
                List<Roster> _Roster = new List<Roster>();
                _Roster = ctx.Rosters.Where(aa => aa.RosterDate == dateTime).ToList();
                List<RosterDetail> _NewRoster = new List<RosterDetail>();
                _NewRoster = ctx.RosterDetails.Where(aa => aa.RosterDate == dateTime).ToList();
                List<LvData> _LvData= new List<LvData>();
                _LvData = ctx.LvDatas.Where(aa => aa.AttDate == dateTime).ToList();
                List<LvShort> _lvShort = new List<LvShort>();
                _lvShort = ctx.LvShorts.Where(aa => aa.DutyDate == dateTime).ToList();
                List<AttData> _AttData = ctx.AttDatas.Where(aa => aa.AttDate == dateTime).ToList();
                List<Holiday> _Holidays = ctx.Holidays.ToList();
                foreach (var emp in _emp)
                {
                    string empDate = emp.EmpID + dateTime.ToString("yyMMdd");
                    if (_AttData.Where(aa => aa.EmpDate == empDate).Count() == 0)
                    {
                        try
                        {
                            /////////////////////////////////////////////////////
                            //  Mark Everyone Absent while creating Attendance //
                            /////////////////////////////////////////////////////
                            //Set DUTYCODE = D, StatusAB = true, and Remarks = [Absent]
                            AttData att = new AttData();
                            att.AttDate = dateTime.Date;
                            att.DutyCode = "D";
                            att.StatusAB = true;
                            att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Absent").First().RemarkValue;
                            if (emp.Shift != null)
                                att.DutyTime = emp.Shift.StartTime;
                            else
                                att.DutyTime = new TimeSpan(07, 45, 00);
                            att.EmpID = emp.EmpID;
                            att.EmpNo = emp.EmpNo;
                            att.EmpDate = emp.EmpID + dateTime.ToString("yyMMdd");
                            att.ShifMin = ProcessSupportFunc.CalculateShiftMinutes(emp.Shift, dateTime.DayOfWeek);
                            //////////////////////////
                            //  Check for Rest Day //
                            ////////////////////////
                            //Set DutyCode = R, StatusAB=false, StatusDO = true, and Remarks=[DO]
                            //Check for 1st Day Off of Shift
                            if (emp.Shift.DaysName.Name == ProcessSupportFunc.ReturnDayOfWeek(dateTime.DayOfWeek))
                            {
                                att.DutyCode = "R";
                                att.StatusAB = false;
                                att.StatusDO = true;
                                att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                            }
                            //Check for 2nd Day Off of shift
                            if (emp.Shift.DaysName1.Name == ProcessSupportFunc.ReturnDayOfWeek(dateTime.DayOfWeek))
                            {
                                att.DutyCode = "R";
                                att.StatusAB = false;
                                att.StatusDO = true;
                                att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                            }

                            //////////////////////////
                            //  Check for GZ Day //
                            ////////////////////////
                            //Set DutyCode = R, StatusAB=false, StatusGZ = true, and Remarks=[GZ]
                            if (emp.Shift.GZDays == true)
                            {
                                foreach (var holiday in _Holidays)
                                {
                                    DateTime dt = holiday.HolDateFrom;
                                    while (dt <= holiday.HolDateTo)
                                    {
                                        if (dt.Month == att.AttDate.Value.Month && dt.Day == att.AttDate.Value.Day)
                                        {
                                            att.DutyCode = "G";
                                            att.StatusAB = false;
                                            att.StatusGZ = true;
                                            att.Remarks = remarks.Where(aa => aa.RemarkLabel == "GZ").First().RemarkValue;
                                            att.ShifMin = 0;
                                        }
                                        dt = dt.AddDays(1);
                                    }
                                }
                            }

                            //////////////////////////
                            //  Check for Roster   //
                            ////////////////////////
                            //If Roster DutyCode is Rest then change the StatusAB and StatusDO
                            foreach (var roster in _Roster.Where(aa => aa.EmpDate == att.EmpDate))
                            {
                                att.DutyCode = roster.DutyCode.Trim();
                                if (att.DutyCode == "R")
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.DutyCode = "R";
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                }

                                att.ShifMin = roster.WorkMin;
                                att.DutyTime = roster.DutyTime;
                            }

                            ////New Roster
                            string empCdate = "Emp" + emp.EmpID.ToString() + dateTime.ToString("yyMMdd");
                            string sectionCdate = "Section" + emp.SecID.ToString() + dateTime.ToString("yyMMdd");
                            string crewCdate = "Crew" + emp.CrewID.ToString() + dateTime.ToString("yyMMdd");
                            string shiftCdate = "Shift" + emp.ShiftID.ToString() + dateTime.ToString("yyMMdd");
                            if (_NewRoster.Where(aa => aa.CriteriaValueDate == empCdate).Count() > 0)
                            {
                                var roster = _NewRoster.FirstOrDefault(aa => aa.CriteriaValueDate == empCdate);
                                if (roster.WorkMin == 0)
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                    att.DutyCode = "R";
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }
                            else if (_NewRoster.Where(aa => aa.CriteriaValueDate == sectionCdate).Count() > 0)
                            {
                                var roster = _NewRoster.FirstOrDefault(aa => aa.CriteriaValueDate == sectionCdate);
                                if (roster.WorkMin == 0)
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                    att.DutyCode = "R";
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }
                            else if(_NewRoster.Where(aa => aa.CriteriaValueDate == crewCdate).Count() > 0)
                            {
                                var roster = _NewRoster.FirstOrDefault(aa => aa.CriteriaValueDate == crewCdate);
                                if (roster.WorkMin == 0)
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                    att.DutyCode = "R";
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }
                            else if (_NewRoster.Where(aa => aa.CriteriaValueDate == shiftCdate).Count() > 0)
                            {
                                var roster = _NewRoster.FirstOrDefault(aa => aa.CriteriaValueDate == shiftCdate);
                                if (roster.WorkMin == 0)
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                    att.DutyCode = "R";
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }
                            ////////////////////////////
                            //TODO Check for Job Card//
                            //////////////////////////



                            ////////////////////////////
                            //  Check for Short Leave//
                            //////////////////////////
                            foreach (var sLeave in _lvShort.Where(aa => aa.EmpDate == att.EmpDate))
                            {
                                if (_lvShort.Where(lv => lv.EmpDate == att.EmpDate).Count() > 0)
                                {
                                    att.StatusSL = true;
                                    att.StatusAB = null;
                                    att.DutyCode = "L";
                                    att.Remarks = "[Short Leave]";
                                }
                            }

                            //////////////////////////
                            //   Check for Leave   //
                            ////////////////////////
                            //Set DutyCode = R, StatusAB=false, StatusGZ = true, and Remarks=[GZ]
                            foreach (var Leave in _LvData)
                            {
                                var _Leave = _LvData.Where(lv => lv.EmpDate == att.EmpDate && lv.HalfLeave != true);
                                if (_Leave.Count() > 0)
                                {
                                    att.StatusLeave = true;
                                    att.StatusAB = false;
                                    att.DutyCode = "L";
                                    att.StatusDO = false;
                                    if (Leave.LvCode == "A")
                                    {
                                        //att.Remarks = "[CL]";
                                        att.Remarks = _Leave.FirstOrDefault().LvType.LvDesc;
                                    }
                                    else if (Leave.LvCode == "B")
                                    {
                                        //att.Remarks = "[AL]";
                                        att.Remarks = _Leave.FirstOrDefault().LvType.LvDesc;
                                    }
                                    else if (Leave.LvCode == "C")
                                    {
                                        //att.Remarks = "[SL]";
                                        att.Remarks = _Leave.FirstOrDefault().LvType.LvDesc;
                                    }
                                    else
                                        att.Remarks = "[" + _Leave.FirstOrDefault().LvType.LvDesc + "]";
                                }
                                else
                                {
                                    att.StatusLeave = false;
                                }
                            }

                            /////////////////////////
                            //Check for Half Leave///
                            ////////////////////////
                            var _HalfLeave = _LvData.Where(lv => lv.EmpDate == att.EmpDate && lv.HalfLeave == true);
                            if (_HalfLeave.Count() > 0)
                            {
                                att.StatusLeave = true;
                                att.StatusAB = false;
                                att.DutyCode = "L";
                                att.StatusHL = true;
                                att.StatusDO = false;
                                if (_HalfLeave.FirstOrDefault().LvCode == "A")
                                {
                                    //att.Remarks = "[H-CL]";
                                    att.Remarks = _HalfLeave.FirstOrDefault().LvType.HalfLvCode;
                                }
                                else if (_HalfLeave.FirstOrDefault().LvCode == "B")
                                {
                                    //att.Remarks = "[S-AL]";
                                    att.Remarks = _HalfLeave.FirstOrDefault().LvType.HalfLvCode;
                                }
                                else if (_HalfLeave.FirstOrDefault().LvCode == "C")
                                {
                                    //att.Remarks = "[H-SL]";
                                    att.Remarks = _HalfLeave.FirstOrDefault().LvType.HalfLvCode;
                                }
                                else
                                    att.Remarks = "[Half Leave]";
                            }
                            else
                            {
                                att.StatusLeave = true;
                                att.StatusHL = true;
                            }
                            ctx.AttDatas.Add(att);
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }
    }
}
