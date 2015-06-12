using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Controllers
{
    class JobCardController
    {
        TAS2013Entities db = new TAS2013Entities();
        private void AddJobCardData(Emp _selEmp, short _WorkCardID, DateTime _dateStart, DateTime _dateEnd, int _userID)
        {
            int _empID = _selEmp.EmpID;
            string _empDate = "";
            DateTime _CreatedDate = DateTime.Now;
            DateTime _Date = _dateStart;
            while (_Date <= _dateEnd)
            {
                _empDate = _selEmp.EmpID + _Date.ToString("yyMMdd");
                AddJobCardDataToDatabase(_empDate, _empID, _Date, _userID, _WorkCardID, _CreatedDate);
                if (db.AttProcesses.Where(aa => aa.ProcessDate == _Date).Count() > 0)
                {
                    switch (_WorkCardID)
                    {
                        case 1://Day Off
                            AddJCDayOffToAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 2://GZ Holiday
                            AddJCGZDayToAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 3://Absent
                            AddJCAbsentToAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 4://official Duty
                            AddJCODDayToAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 5://Normal Day 565
                            AddJCNorrmalDayAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 6://Normal Day 540
                            AddJCNorrmalDayAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 7://Normal Day 480
                            AddJCNorrmalDayAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                        case 8://Late In Margin
                            AddLateInMarginAttData(_empDate, _empID, _Date, _userID, _WorkCardID);
                            break;
                    }
                }
                _Date = _Date.AddDays(1);
            }
        }

        private bool AddJobCardDataToDatabase(string _empDate, int _empID, DateTime _currentDate, int _userID, short _WorkCardID, DateTime dateTime)
        {
            bool check = false;
            try
            {
                JobCardEmp _jobCardEmp = new JobCardEmp();
                _jobCardEmp.EmpDate = _empDate;
                _jobCardEmp.EmpID = _empID;
                _jobCardEmp.Dated = _currentDate;
                _jobCardEmp.SubmittedFrom = _userID;
                _jobCardEmp.WrkCardID = _WorkCardID;
                _jobCardEmp.DateCreated = dateTime;
                db.JobCardEmps.Add(_jobCardEmp);
                if (db.SaveChanges() > 0)
                {
                    check = true;
                }
            }
            catch (Exception ex)
            {
                check = false;
            }
            return check;
        }

        #region --Job Cards - AttData ---

        private bool AddJCNorrmalDayAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {
            bool check = false;
            try
            {
                //Normal Duty
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    JobCard _jcCard = context.JobCards.FirstOrDefault(aa => aa.WorkCardID == _WorkCardID);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "D";
                        _attdata.StatusAB = false;
                        _attdata.StatusDO = false;
                        _attdata.StatusLeave = false;
                        _attdata.StatusP = true;
                        _attdata.WorkMin = _jcCard.WorkMin;
                        _attdata.ShifMin = _jcCard.WorkMin;
                        _attdata.Remarks = "[Present][Manual]";
                        _attdata.TimeIn = null;
                        _attdata.TimeOut = null;
                        _attdata.EarlyIn = null;
                        _attdata.EarlyOut = null;
                        _attdata.LateIn = null;
                        _attdata.LateOut = null;
                        _attdata.OTMin = null;
                        _attdata.StatusEI = null;
                        _attdata.StatusEO = null;
                        _attdata.StatusLI = null;
                        _attdata.StatusLO = null;
                        _attdata.StatusP = true;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
            }
            return check;
        }

        private bool AddJCODDayToAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {

            bool check = false;
            try
            {
                //Official Duty
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "O";
                        _attdata.StatusAB = false;
                        _attdata.StatusDO = false;
                        _attdata.StatusLeave = false;
                        _attdata.StatusP = true;
                        _attdata.WorkMin = _attdata.ShifMin;
                        _attdata.Remarks = "[Official Duty][Manual]";
                        _attdata.TimeIn = null;
                        _attdata.TimeOut = null;
                        _attdata.WorkMin = null;
                        _attdata.EarlyIn = null;
                        _attdata.EarlyOut = null;
                        _attdata.LateIn = null;
                        _attdata.LateOut = null;
                        _attdata.OTMin = null;
                        _attdata.StatusEI = null;
                        _attdata.StatusEO = null;
                        _attdata.StatusLI = null;
                        _attdata.StatusLO = null;
                        _attdata.StatusP = null;
                        _attdata.StatusGZ = false;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            return check;
        }

        private bool AddJCAbsentToAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {
            bool check = false;
            try
            {
                //Absent
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "D";
                        _attdata.StatusAB = true;
                        _attdata.StatusDO = false;
                        _attdata.StatusLeave = false;
                        _attdata.Remarks = "[Absent][Manual]";
                        _attdata.TimeIn = null;
                        _attdata.TimeOut = null;
                        _attdata.WorkMin = null;
                        _attdata.EarlyIn = null;
                        _attdata.EarlyOut = null;
                        _attdata.LateIn = null;
                        _attdata.LateOut = null;
                        _attdata.OTMin = null;
                        _attdata.StatusEI = null;
                        _attdata.StatusEO = null;
                        _attdata.StatusLI = null;
                        _attdata.StatusLO = null;
                        _attdata.StatusP = null;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            return check;
        }

        private bool AddJCGZDayToAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {
            bool check = false;
            try
            {
                //GZ Holiday
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "G";
                        _attdata.StatusAB = false;
                        _attdata.StatusDO = true;
                        _attdata.StatusLeave = false;
                        _attdata.StatusGZ = true;
                        _attdata.Remarks = "[GZ][Manual]";
                        _attdata.TimeIn = null;
                        _attdata.TimeOut = null;
                        _attdata.WorkMin = null;
                        _attdata.EarlyIn = null;
                        _attdata.EarlyOut = null;
                        _attdata.LateIn = null;
                        _attdata.LateOut = null;
                        _attdata.OTMin = null;
                        _attdata.StatusEI = null;
                        _attdata.StatusEO = null;
                        _attdata.StatusLI = null;
                        _attdata.StatusLO = null;
                        _attdata.StatusP = null;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            return check;
        }

        private bool AddJCDayOffToAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {
            bool check = false;
            try
            {
                //Day Off
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "R";
                        _attdata.StatusAB = false;
                        _attdata.StatusDO = true;
                        _attdata.StatusLeave = false;
                        _attdata.Remarks = "[DO][Manual]";
                        _attdata.TimeIn = null;
                        _attdata.TimeOut = null;
                        _attdata.WorkMin = null;
                        _attdata.EarlyIn = null;
                        _attdata.EarlyOut = null;
                        _attdata.LateIn = null;
                        _attdata.LateOut = null;
                        _attdata.OTMin = null;
                        _attdata.StatusEI = null;
                        _attdata.StatusEO = null;
                        _attdata.StatusLI = null;
                        _attdata.StatusLO = null;
                        _attdata.StatusP = null;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            return check;
        }

        private bool AddLateInMarginAttData(string _empDate, int _empID, DateTime _Date, int _userID, short _WorkCardID)
        {
            bool check = false;
            try
            {
                //Late In Job Card
                short LateInMins = 0;
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    if (_attdata != null)
                    {
                        _attdata.StatusAB = false;
                        _attdata.Remarks.Replace("[LI]", "");
                        _attdata.Remarks = _attdata.Remarks + "[LI Job Card]";
                        _attdata.LateIn = 0;
                        _attdata.WorkMin = (short)(_attdata.WorkMin + (short)LateInMins);
                        _attdata.StatusLI = false;
                    }
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                        check = true;
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
            return check;
        }
        #endregion
    }
}
