using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.Reports.UserControls;
using TimeAttendanceSystem.ViewModels.VMAttJobCard;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttEditJobCard.xaml
    /// </summary>
    public partial class AttEditJobCard : Page
    {
        TAS2013Entities db = new TAS2013Entities();
        VMAttJobCard vmAttJobCard;
        public AttEditJobCard()
        {
            InitializeComponent();
            //rbShift.IsChecked = true;
            vmAttJobCard = new VMAttJobCard();
            this.DataContext = vmAttJobCard;
        }

        private void btn_JobCardSelect_Click(object sender, RoutedEventArgs e)
        {
            if (rbShift.IsChecked == true)
            {
                windowShift = new RFShifts(selectedShifts);
                if ((bool)windowShift.ShowDialog())
                {
                    ListBoxData.Items.Clear();
                    selectedShifts = windowShift.selectedShifts;
                }
                foreach (var item in selectedShifts)
                    ListBoxData.Items.Add(item.ShiftName+"-"+item.ShiftID.ToString());
            }

            if (rbSection.IsChecked == true)
            {
                windowSec = new RFSections(selectedSecs);
                if ((bool)windowSec.ShowDialog())
                {
                    ListBoxData.Items.Clear();
                    selectedSecs = windowSec.selectedSecs;
                }
                foreach (var item in selectedSecs)
                    ListBoxData.Items.Add(item.SectionName + "-" + item.SectionID.ToString());
            }

            if (rbEmployee.IsChecked == true)
            {
                windowEmp = new RFEmployees(selectedEmps);
                if ((bool)windowEmp.ShowDialog())
                {
                    ListBoxData.Items.Clear();
                    selectedEmps = windowEmp.selectedEmps;
                }
                if(selectedEmps!=null)
                foreach (var item in selectedEmps)
                    ListBoxData.Items.Add(item.EmpName + "-" + item.EmpID.ToString());
            }

            if (rbCrew.IsChecked == true)
            {
                windowCrew = new RFCrews(selectedCrews);
                if ((bool)windowCrew.ShowDialog())
                {
                    ListBoxData.Items.Clear();
                    selectedCrews = windowCrew.selectedCrews;
                }
                foreach (var item in selectedCrews)
                    ListBoxData.Items.Add(item.CrewName + "-" + item.CrewID.ToString());
            }
        }

        RFEmployees windowEmp;
        RFSections windowSec;
        RFCrews windowCrew;
        RFShifts windowShift;
        public List<Emp> selectedEmps;
        public List<Model.Section> selectedSecs;
        public List<Crew> selectedCrews;
        public List<Shift> selectedShifts;

        private void btn_Apply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new TAS2013Entities())
                {
                    
                    List<Emp> _Emp = new List<Emp>();
                    short _WorkCardID = (short)cbJobCard.SelectedValue;
                    //First Save Job Card Application
                    JobCardApp jobCardApp = new JobCardApp();
                    jobCardApp.CardType = _WorkCardID;
                    jobCardApp.DateCreated = DateTime.Now;
                    jobCardApp.UserID = GlobalClasses.Global.user.UserID;
                    jobCardApp.DateStarted = dtpStart.SelectedDate;
                    jobCardApp.DateEnded = dtpEnd.SelectedDate;
                    jobCardApp.Status = false;
                    string JobCardCriteria = "";
                    if (rbCrew.IsChecked == true)
                        JobCardCriteria = "crew";
                    if (rbEmployee.IsChecked == true)
                        JobCardCriteria = "employee";
                    if (rbSection.IsChecked == true)
                        JobCardCriteria = "section";
                    if (rbShift.IsChecked == true)
                        JobCardCriteria = "shift";
                    foreach (var item in ListBoxData.Items)
                    {
                        
                        string[] values = item.ToString().Split('-');
                        int criteriaData = Convert.ToInt32(values[1]);
                        switch (JobCardCriteria)
                        {
                            case "shift":
                                jobCardApp.CriteriaData = criteriaData;
                                jobCardApp.JobCardCriteria = "S";
                                db.JobCardApps.Add(jobCardApp);
                                if (db.SaveChanges() > 0)
                                {
                                    AddJobCardAppToJobCardData();
                                }
                                break;
                            case "crew":
                                jobCardApp.CriteriaData = criteriaData;
                                jobCardApp.JobCardCriteria = "C";
                                db.JobCardApps.Add(jobCardApp);
                                if (db.SaveChanges() > 0)
                                {
                                    AddJobCardAppToJobCardData();
                                }
                                break;
                            case "section":
                                jobCardApp.CriteriaData = criteriaData;
                                jobCardApp.JobCardCriteria = "T";
                                db.JobCardApps.Add(jobCardApp);
                                if (db.SaveChanges() > 0)
                                {
                                    AddJobCardAppToJobCardData();
                                }
                                break;
                            case "employee":
                                    _Emp = db.Emps.Where(aa => aa.EmpID == criteriaData).ToList();
                                    if (_Emp.Count > 0)
                                    {
                                        jobCardApp.CriteriaData = _Emp.FirstOrDefault().EmpID;
                                        jobCardApp.JobCardCriteria = "E";
                                        db.JobCardApps.Add(jobCardApp);
                                        if (db.SaveChanges() > 0)
                                        {
                                            AddJobCardAppToJobCardData();
                                        }
                                    }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
       }
        private bool ValidateJobCard(DateTime dateStart, short CardType)
        {
            bool check = false;
            using (var ctx = new TAS2013Entities())
            {
                List<JobCardApp> jcApp = new List<JobCardApp>();
                if (ctx.JobCardApps.Where(aa => aa.DateStarted == dateStart && aa.CardType == CardType).Count() > 0)
                    check = true;
                ctx.Dispose();
            }
            return check;
        }
        //Add Job Card To Job Card Data
        private void AddJobCardAppToJobCardData()
        {
            using (var ctx = new TAS2013Entities())
            {
                List<JobCardApp> _jobCardApp = new List<JobCardApp>();
                _jobCardApp = ctx.JobCardApps.Where(aa => aa.Status == false).ToList();
                List<Emp> _Emp = new List<Emp>();
                foreach (var jcApp in _jobCardApp)
                {
                    jcApp.Status = true;
                    switch (jcApp.JobCardCriteria)
                    {
                        case "S":
                            short _shiftID = Convert.ToByte(jcApp.CriteriaData);
                            _Emp = ctx.Emps.Where(aa => aa.ShiftID == _shiftID).ToList();
                            break;
                        case "C":
                            short _crewID = Convert.ToByte(jcApp.CriteriaData);
                            _Emp = ctx.Emps.Where(aa => aa.CrewID == _crewID).ToList();
                            break;
                        case "T":
                            short _secID = Convert.ToByte(jcApp.CriteriaData);
                            _Emp = ctx.Emps.Where(aa => aa.SecID == _secID).ToList();
                            break;
                        case "E":
                            int _EmpID = (int)jcApp.CriteriaData;
                            _Emp = ctx.Emps.Where(aa => aa.EmpID == _EmpID).ToList();
                            break;
                    }
                    foreach (var selectedEmp in _Emp)
                    {
                        AddJobCardData(selectedEmp, jcApp);
                    }
                }
                ctx.SaveChanges();
                ctx.Dispose();
            }
        }

        private void AddJobCardData(Emp _selEmp, JobCardApp jcApp)
        {
            int _empID = _selEmp.EmpID;
            string _empDate = "";
            int _userID = (int)jcApp.UserID;
            DateTime _Date = (DateTime)jcApp.DateStarted;
            while (_Date <= jcApp.DateEnded)
            {
                _empDate = _selEmp.EmpID + _Date.ToString("yyMMdd");
                AddJobCardDataToDatabase(_empDate, _empID, _Date, _userID, jcApp);
                if (db.AttProcesses.Where(aa => aa.ProcessDate == _Date).Count() > 0)
                {
                    switch (jcApp.CardType)
                    {
                        case 1://Present
                            AddJCNorrmalDayAttData(_empDate, _empID, _Date, _userID, (short)jcApp.CardType);
                            break;
                        case 2://Absent
                            AddJCAbsentToAttData(_empDate, _empID, _Date, _userID, (short)jcApp.CardType);
                            break;
                        case 3://Day Off
                            AddJCDayOffToAttData(_empDate, _empID, _Date, _userID, (short)jcApp.CardType);
                            break;
                        case 4://GZ Day
                            AddJCODDayToAttData(_empDate, _empID, _Date, _userID, (short)jcApp.CardType);
                            break;
                        case 5://Official Duty
                            AddJCODDayToAttData(_empDate, _empID, _Date, _userID, (short)jcApp.CardType);
                            break;
                    }
                }
                _Date = _Date.AddDays(1);
            }
        }

        private bool AddJobCardDataToDatabase(string _empDate, int _empID, DateTime _currentDate, int _userID, JobCardApp jcApp)
        {
            bool check = false;
            try
            {
                JobCardEmp _jobCardEmp = new JobCardEmp();
                _jobCardEmp.EmpDate = _empDate;
                _jobCardEmp.EmpID = _empID;
                _jobCardEmp.Dated = _currentDate;
                _jobCardEmp.SubmittedFrom = _userID;
                _jobCardEmp.WrkCardID = jcApp.CardType;
                _jobCardEmp.DateCreated = DateTime.Now;
                _jobCardEmp.WorkMin = jcApp.WorkMin;
                //_jobCardEmp.JCAppID = jcApp.JobCardID;
                //_jobCardEmp.OtherValue = jcApp.OtherValue;
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

        private bool AddDoubleDutyAttData(string _empDate, int _empID, DateTime _Date, int _userID, JobCardApp jcApp)
        {
            bool check = false;
            try
            {
                //Normal Duty
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    JobCard _jcCard = context.JobCards.FirstOrDefault(aa => aa.WorkCardID == jcApp.CardType);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "D";
                        _attdata.StatusAB = false;
                        _attdata.StatusDO = false;
                        _attdata.StatusLeave = false;
                        _attdata.StatusP = true;
                        _attdata.WorkMin = _jcCard.WorkMin;
                        _attdata.Remarks = _attdata.Remarks + "[DD][Manual]";
                        _attdata.StatusMN = true;
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

        private bool AddBadliAttData(string _empDate, int _empID, DateTime _Date, int _userID, JobCardApp jcApp)
        {
            bool check = false;
            try
            {
                //Normal Duty
                using (var context = new TAS2013Entities())
                {
                    AttData _attdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == _empDate);
                    JobCard _jcCard = context.JobCards.FirstOrDefault(aa => aa.WorkCardID == jcApp.CardType);
                    if (_attdata != null)
                    {
                        _attdata.DutyCode = "D";
                        _attdata.StatusAB = false;
                        _attdata.StatusLeave = false;
                        _attdata.StatusP = true;
                        _attdata.Remarks = _attdata.Remarks + "[Badli][Manual]";
                        _attdata.StatusMN = true;
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
                        _attdata.Remarks = _attdata.Remarks + "[Official Duty][Manual]";
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
        private void AddJCPresentToAttData(string _empDate, int _empID, DateTime _Date, int _userID, short p)
        {

        }

        #endregion

        private void ClearListBox_Click(object sender, RoutedEventArgs e)
        {
            ListBoxData.Items.Clear();
        }
    }
}
