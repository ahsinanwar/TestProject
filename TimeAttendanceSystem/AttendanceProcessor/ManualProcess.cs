using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeAttendanceSystem.Model;
using WMSFFService;

namespace TASDownloadService.AttProcessDaily
{
    public class ManualProcess
    {
        #region --Process Daily Attendance--

        TAS2013Entities context = new TAS2013Entities();

        Emp employee = new Emp();

        public void ManualProcessAttendance(DateTime date,List<Emp> emps, List<AttData> _AttDatas)
        {
            BootstrapAttendance(date, emps);
            DateTime dateEnd = date.AddDays(1);
            List<Remark> remarks = new List<Remark>();
            remarks = context.Remarks.ToList();
            List<AttData> attData = new List<AttData>();
            attData= context.AttDatas.Where(aa => aa.AttDate == date).ToList();
            List<PollData> unprocessedPolls = context.PollDatas.Where(p => (p.EntDate >= date && p.EntDate <=dateEnd)).OrderBy(e => e.EntTime).ToList();
            foreach (PollData up in unprocessedPolls)
            {
                try
                {
                    //Check AttData with EmpDate
                    if (attData.Where(attd => attd.EmpDate == up.EmpDate).Count() > 0)
                    {
                        AttData attendanceRecord = attData.First(attd => attd.EmpDate == up.EmpDate);
                        employee = attendanceRecord.Emp;
                        Shift shift = employee.Shift;
                        //Set Time In and Time Out in AttData
                        if (attendanceRecord.Emp.Shift.OpenShift == true)
                        {
                            //Set Time In and Time Out for open shift
                            PlaceTimeInOuts.CalculateTimeINOUTOpenShift(attendanceRecord, up);
                        }
                        else
                        {
                            TimeSpan checkTimeEnd = new TimeSpan();
                            DateTime TimeInCheck = new DateTime();
                            if (attendanceRecord.TimeIn == null)
                            {
                                TimeInCheck = attendanceRecord.AttDate.Value.Add(attendanceRecord.DutyTime.Value);
                            }
                            else
                                TimeInCheck = attendanceRecord.TimeIn.Value;
                            if (attendanceRecord.ShifMin == 0)
                                checkTimeEnd = TimeInCheck.TimeOfDay.Add(new TimeSpan(0, 480, 0));
                            else
                                checkTimeEnd = TimeInCheck.TimeOfDay.Add(new TimeSpan(0, (int)attendanceRecord.ShifMin, 0));
                            if (checkTimeEnd.Days > 0)
                            {
                                //if Time out occur at next day
                                if (up.RdrDuty == 5)
                                {
                                    DateTime dt = new DateTime();
                                    dt = up.EntDate.Date.AddDays(-1);
                                    var _attData = context.AttDatas.FirstOrDefault(aa => aa.AttDate == dt && aa.EmpID == up.EmpID);
                                    if (_attData != null)
                                    {

                                        if (_attData.TimeIn != null)
                                        {
                                            if ((up.EntTime - _attData.TimeIn.Value).Hours < 18)
                                            {
                                                attendanceRecord = _attData;
                                                up.EmpDate = up.EmpID.ToString() + dt.Date.ToString("yyMMdd");
                                            }

                                        }
                                        else
                                        {
                                            attendanceRecord = _attData;
                                            up.EmpDate = up.EmpID.ToString() + dt.Date.ToString("yyMMdd");
                                        }

                                    }
                                }
                            }
                            //Set Time In and Time Out
                            //Set Time In and Time Out
                            if (up.RdrDuty == 5)
                            {
                                if (attendanceRecord.TimeIn != null)
                                {
                                    TimeSpan dt = (TimeSpan)(up.EntTime.TimeOfDay - attendanceRecord.TimeIn.Value.TimeOfDay);
                                    if (dt.Minutes < 0)
                                    {
                                        DateTime dt1 = new DateTime();
                                        dt1 = up.EntDate.Date.AddDays(-1);
                                        var _attData = context.AttDatas.FirstOrDefault(aa => aa.AttDate == dt1 && aa.EmpID == up.EmpID);
                                        attendanceRecord = _attData;
                                        up.EmpDate = up.EmpID.ToString() + dt1.Date.ToString("yyMMdd");
                                        PlaceTimeInOuts.CalculateTimeINOUT(attendanceRecord, up);
                                    }
                                    else
                                        PlaceTimeInOuts.CalculateTimeINOUT(attendanceRecord, up);
                                }
                                else
                                    PlaceTimeInOuts.CalculateTimeINOUT(attendanceRecord, up);
                            }
                            else
                                PlaceTimeInOuts.CalculateTimeINOUT(attendanceRecord, up);
                        }
                        if (employee.Shift.OpenShift == true)
                        {
                            if (up.EntTime.TimeOfDay < PlaceTimeInOuts.OpenShiftThresholdEnd)
                            {
                                DateTime dt = up.EntDate.Date.AddDays(-1);
                                CalculateWorkMins.CalculateOpenShiftTimes(context.AttDatas.FirstOrDefault(aa => aa.AttDate == dt && aa.EmpID == up.EmpID), shift, remarks);
                            }
                        }
                        //If TimeIn and TimeOut are not null, then calculate other Atributes
                        if (attendanceRecord.TimeIn != null && attendanceRecord.TimeOut != null)
                        {
                            if (context.Rosters.Where(r => r.EmpDate == up.EmpDate).Count() > 0)
                            {
                                CalculateWorkMins.CalculateRosterTimes(attendanceRecord, context.Rosters.FirstOrDefault(r => r.EmpDate == up.EmpDate), shift, remarks);
                            }
                            else
                            {
                                if (shift.OpenShift == true)
                                {
                                    if (up.EntTime.TimeOfDay < PlaceTimeInOuts.OpenShiftThresholdEnd)
                                    {
                                        DateTime dt = up.EntDate.Date.AddDays(-1);
                                        CalculateWorkMins.CalculateOpenShiftTimes(context.AttDatas.FirstOrDefault(aa => aa.AttDate == dt && aa.EmpID == up.EmpID), shift, remarks);
                                        CalculateWorkMins.CalculateOpenShiftTimes(attendanceRecord, shift, remarks);
                                    }
                                    else
                                    {
                                        //Calculate open shifft time of the same date
                                        CalculateWorkMins.CalculateOpenShiftTimes(attendanceRecord, shift, remarks);
                                    }
                                }
                                else
                                {
                                    CalculateWorkMins.CalculateShiftTimes(attendanceRecord, shift, remarks);
                                }
                            }
                        }
                        up.Process = true;
                    }
                }
                catch (Exception ex)
                {
                    string _error = "";
                    if (ex.InnerException.Message != null)
                        _error = ex.InnerException.Message;
                    else
                        _error = ex.Message;
                }
                context.SaveChanges();
            }
            context.Dispose();
        }


        public void BootstrapAttendance(DateTime dateTime, List<Emp> emps)
        {
            using (var ctx = new TAS2013Entities())
            {
                List<Roster> _Roster = new List<Roster>();
                _Roster = ctx.Rosters.Where(aa => aa.RosterDate == dateTime).ToList();
                List<RosterDetail> _NewRoster = new List<RosterDetail>();
                _NewRoster = ctx.RosterDetails.Where(aa => aa.RosterDate == dateTime).ToList();
                List<LvData> _LvData = new List<LvData>();
                _LvData = ctx.LvDatas.Where(aa => aa.AttDate == dateTime).ToList();
                List<LvShort> _lvShort = new List<LvShort>();
                _lvShort = ctx.LvShorts.Where(aa => aa.DutyDate == dateTime).ToList();
                //List<AttData> _AttData = context.AttDatas.Where(aa => aa.AttDate == dateTime).ToList();
                List<AttData> attData = new List<AttData>();
                attData = ctx.AttDatas.Where(aa => aa.AttDate == dateTime).ToList();
                List<Remark> remarks = new List<Remark>();
                remarks = ctx.Remarks.ToList();
                foreach (var emp in emps)
                {
                    string empDate = emp.EmpID + dateTime.ToString("yyMMdd");
                    if (attData.Where(aa => aa.EmpDate == empDate).Count() > 0)
                    {
                        try
                        {
                            /////////////////////////////////////////////////////
                            //  Mark Everyone Absent while creating Attendance //
                            /////////////////////////////////////////////////////
                            //Set DUTYCODE = D, StatusAB = true, and Remarks = [Absent]
                            AttData att = attData.First(aa => aa.EmpDate == empDate);
                            //Reset Flags
                            att.TimeIn = null;
                            att.TimeOut = null;
                            att.Tin0 = null;
                            att.Tout0 = null;
                            att.Tin1 = null;
                            att.Tout1 = null;
                            att.Tin2 = null;
                            att.Tout2 = null;
                            att.Tin3 = null;
                            att.Tout3 = null;
                            att.Tin4 = null;
                            att.Tout4 = null;
                            att.Tin5 = null;
                            att.Tout5 = null;
                            att.Tin6 = null;
                            att.Tout6 = null;
                            att.Tin7 = null;
                            att.Tout7 = null; 
                            att.Tin8 = null;
                            att.Tout8 = null;
                            att.StatusP = null;
                            att.StatusDO = null;
                            att.StatusEO = null;
                            att.StatusGZ = null;
                            att.StatusGZOT = null;
                            att.StatusHD = null;
                            att.StatusHL = null;
                            att.StatusIN = null;
                            att.StatusLeave = null;
                            att.StatusLI = null;
                            att.StatusLO = null;
                            att.StatusMN = null;
                            att.StatusOD = null;
                            att.StatusOT = null;
                            att.StatusSL = null;
                            att.WorkMin = null;
                            att.LateIn = null;
                            att.LateOut = null;
                            att.OTMin = null;
                            att.EarlyIn = null;
                            att.EarlyOut = null;
                            att.Remarks = null;
                            att.ShifMin = null;
                            att.SLMin = null;

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
                            att.BreakMin = ProcessSupportFunc.CalculateShiftBreakMinutes(emp.Shift, dateTime.DayOfWeek);
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
                                    att.ShifMin = 0;
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
                                    att.ShifMin = 0;
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }
                            else if (_NewRoster.Where(aa => aa.CriteriaValueDate == crewCdate).Count() > 0)
                            {
                                var roster = _NewRoster.FirstOrDefault(aa => aa.CriteriaValueDate == crewCdate);
                                if (roster.WorkMin == 0)
                                {
                                    att.StatusAB = false;
                                    att.StatusDO = true;
                                    att.Remarks = remarks.Where(aa => aa.RemarkLabel == "Rest").First().RemarkValue;
                                    att.DutyCode = "R";
                                    att.ShifMin = 0;
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
                                    att.ShifMin = 0;
                                }
                                else
                                {
                                    att.ShifMin = roster.WorkMin;
                                    att.DutyCode = "D";
                                    att.DutyTime = roster.DutyTime;
                                }
                            }

                            //////////////////////////
                            //  Check for GZ Day //
                            ////////////////////////
                            //Set DutyCode = R, StatusAB=false, StatusGZ = true, and Remarks=[GZ]
                            if (emp.Shift.GZDays == true)
                            {
                                foreach (var holiday in context.Holidays)
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
                            ////////////////////////////
                            //TODO Check for Job Card//
                            //////////////////////////

                            ProcessJobCard pjc = new ProcessJobCard();
                            pjc.ProcessJobCards(dateTime);

                            ////////////////////////////
                            //  Check for Short Leave//
                            //////////////////////////
                            foreach (var sLeave in _lvShort.Where(aa => aa.EmpDate == att.EmpDate))
                            {
                                    if (_lvShort.Where(lv => lv.EmpDate == att.EmpDate).Count() > 0)
                                    {
                                        att.StatusSL = true;
                                        att.StatusAB = null;
                                        att.SLMin = sLeave.THour;
                                        att.ShifMin = (short)(att.ShifMin - Convert.ToInt16(sLeave.THour.Value.TotalMinutes));
                                        att.DutyCode = "D";
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
                                    att.Remarks = _Leave.FirstOrDefault().LvType.ReportName;
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
                                att.Remarks = _HalfLeave.FirstOrDefault().LvType.HalfLvCode;
                            }
                            else
                            {
                                att.StatusHL = false;
                            }
                            ctx.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                ctx.Dispose();
            }
        }

        #endregion
    }
}
