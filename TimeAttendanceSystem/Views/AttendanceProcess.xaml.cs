using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
//using TimeAttendanceSystem.AttendanceProcessor;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;
using Mantin.Controls.Wpf.Notification;
using TASDownloadService;
using TASDownloadService.AttProcessDaily;
using WMSFFService;
using TimeAttendanceSystem.Controllers;
using TASDownloadService.Helper;
using Telerik.Windows.Controls;
using TimeAttendanceSystem.AttendanceProcessor;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttendanceProcess.xaml
    /// </summary>
    public partial class AttendanceProcess : Page
    {
        string ProcessType = "";
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private readonly BackgroundWorker worker2 = new BackgroundWorker();
       
        public AttendanceProcess()
        {

            try
            {
                InitializeComponent();
                this.DataContext = ProcessType;
                DateTime DateFrom = new DateTime();
                DateTime DateTo = new DateTime();
                bool isActive = false;
                worker.DoWork += worker_DoWork;
                worker2.DoWork += worker_DoWork2;
                worker2.RunWorkerCompleted += worker2_RunWorkerCompleted;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }

        private void worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PopUp.popUp("PollData", "Data Transferred From Reader to PollData", NotificationType.Information);
        }

        private void worker_DoWork2(object sender, DoWorkEventArgs e)
        {
            Downloader d = new Downloader();
            d.DownloadDataInIt();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Object[] arg = e.Argument as Object[];
            TAS2013Entities ctx = new TAS2013Entities();

            // Download Attendance From Readers

            DateTime dateStart = (DateTime)arg[0];
            DateTime dateEnd = (DateTime)arg[1];
            List<Emp> emps = new List<Emp>();
            List<AttData> attdata = new List<AttData>();
            //emps = ctx.Emps.Where(aa => aa.Status == true).ToList();
            emps = ctx.Emps.Where(aa => aa.EmpID == 6965).ToList();
            if (ProcessType == "Daily")
            {
                while (dateStart <= dateEnd)
                {
                    attdata.Clear();
                    
                        if (ctx.AttProcesses.Where(aa => aa.ProcessDate == dateStart).Count() == 0)
                        {
                            ProcessAttendance p = new ProcessAttendance();
                            p.CreateAttendance(dateStart, ctx.Remarks.ToList());
                            p.ProcessDailyAttendance(dateStart);
                        }
                        else
                        {
                            ManualProcess mp = new ManualProcess();
                            attdata = ctx.AttDatas.Where(aa => aa.AttDate == dateStart).ToList();
                            mp.ManualProcessAttendance(dateStart, emps, attdata);
                        }
                    
                    
                    dateStart = dateStart.AddDays(1);

                }
            }
            else {


                DailySummaryClass dailysum = new DailySummaryClass(dateStart, dateEnd);
                DateTime dtStart = DateTime.Today.AddDays(-2);
                DateTime dtend = DateTime.Today;
                CorrectAttEntriesWithWrongFlags(dtStart, dtend);
                ProcessMonthlyAttendance(dateEnd);
            
            
            }
             
            // Process Edit Attendance Entries if any
            ProcessEditAttendanceEntries pea = new ProcessEditAttendanceEntries();
            pea.ProcessManualEditAttendance(dateStart, dateEnd);
            //Process Job cards if any
            ApplyJobCard(dateStart, dateEnd);
        }

        private void ProcessMonthlyAttendance(DateTime date)
        {
            //Process Month till end of month
            DateTime endDate = date.Date;
            int currentDay = date.Date.Day;
            int currentMonth = date.Date.Month;
            int currentYear = date.Date.Year;
            DateTime startDate = new DateTime(currentYear, currentMonth, 1);
            if (currentMonth == 1 && currentDay < 10)
            {
                currentMonth = 13;
                currentYear = currentYear - 1;
            }
            if (endDate.Day < 10)
            {
                currentMonth = currentMonth - 1;
                int DaysInPreviousMonth = System.DateTime.DaysInMonth(currentYear, currentMonth);
                endDate = new DateTime(currentYear, currentMonth, DaysInPreviousMonth);
                startDate = new DateTime(currentYear, currentMonth, 1);
            }
            else
            {
                startDate = new DateTime(currentYear, currentMonth, 1);
            }


            ProcessMonthly(startDate, endDate);
        }

        private void ProcessMonthly(DateTime startDate, DateTime endDate)
        {
            TAS2013Entities ctx = new TAS2013Entities();
            // Pass list of selected emp Attendance data to optimize sql query 
            List<AttData> _AttData = new List<AttData>();
            List<AttData> _EmpAttData = new List<AttData>();
            _AttData = ctx.AttDatas.Where(aa => aa.AttDate >= startDate && aa.AttDate <= endDate).ToList();
            int count = 0;
            List<Emp> _Emp = ctx.Emps.Where(em => em.Status == true).ToList();
            List<Emp> _oEmp = ctx.Emps.Where(em => em.Status == true).ToList();
            _Emp.AddRange(_oEmp);
            int _TE = _Emp.Count;
            foreach (Emp emp in _Emp)
            {
                count++;
                try
                {
                    ContractualMonthlyProcessor cmp = new ContractualMonthlyProcessor();
                    _EmpAttData = _AttData.Where(aa => aa.EmpID == emp.EmpID).ToList();
                    if (!cmp.processContractualMonthlyAttSingle(startDate, endDate, emp, _EmpAttData))
                    {
                    }

                }
                catch (Exception ex)
                {

                }
            }
        }

        private void CorrectAttEntriesWithWrongFlags(DateTime startdate, DateTime endDate)
        {
            using (var ctx = new TAS2013Entities())
            {
                // where StatusGZ ==1 and DutyCode != G
                List<AttData> _attDataForGZ = ctx.AttDatas.Where(aa => aa.AttDate >= startdate && aa.AttDate <= endDate && aa.StatusGZ == true && aa.DutyCode != "G").ToList();
                foreach (var item in _attDataForGZ)
                {
                    item.DutyCode = "G";
                }
                ctx.SaveChanges();

                // where StatusDO ==1 and DutyCode != R
                List<AttData> _attDataForDO = ctx.AttDatas.Where(aa => aa.AttDate >= startdate && aa.AttDate <= endDate && aa.StatusDO == true && aa.DutyCode != "R").ToList();
                foreach (var item in _attDataForGZ)
                {
                    item.DutyCode = "R";
                }
                ctx.SaveChanges();
                ctx.Dispose();
            }
        }
            private void ApplyJobCard(DateTime dateStart, DateTime dateEnd)
        {
            TAS2013Entities ctx = new TAS2013Entities();
            List<JobCardEmp> jcsEmp = new List<JobCardEmp>();
            jcsEmp = ctx.JobCardEmps.Where(aa=>aa.Dated>=dateStart&& aa.Dated<=dateEnd).ToList();
            foreach(var jcEmp in jcsEmp)
            {
                JobCardController JCController = new JobCardController();
                int _empID = (int)jcEmp.EmpID;
              
                string _empDate = _empID.ToString() + jcEmp.Dated.Value.ToString("yyMMdd");
                DateTime _Date = (DateTime)jcEmp.Dated;
                if (ctx.AttProcesses.Where(aa => aa.ProcessDate == jcEmp.Dated).Count() > 0)
                {
                    switch (jcEmp.WrkCardID)
                    {
                        case 1://Present
                            JCController.AddJCNorrmalDayAttData(_empDate, _empID, _Date, (short)jcEmp.WrkCardID);
                            break;
                        case 2://Absent
                            JCController.AddJCAbsentToAttData(_empDate, _empID, _Date, (short)jcEmp.WrkCardID);
                            break;
                        case 3://Day Off
                            JCController.AddJCDayOffToAttData(_empDate, _empID, _Date, (short)jcEmp.WrkCardID);
                            break;
                        case 4://GZ Day
                            JCController.AddJCODDayToAttData(_empDate, _empID, _Date, (short)jcEmp.WrkCardID);
                            break;
                        case 5://Official Duty
                            JCController.AddJCODDayToAttData(_empDate, _empID, _Date, (short)jcEmp.WrkCardID);
                            break;
                    }
                }
            }
        // run all background tasks here
        }
          
   // run all background tasks here


private void worker_RunWorkerCompleted(object sender, 
                                       RunWorkerCompletedEventArgs e)
{
    PopUp.popUp("Proccess Attendance", "Your Attendance Has Been Proccessed", NotificationType.Information);
}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this.DataContext);
            Object[] args = { dtpStart.SelectedDate.Value, dtpEnd.SelectedDate.Value};
            worker.RunWorkerAsync(args );
            
        }

        private void ProcessType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedValue =(string)((RadComboBox)sender).SelectedValue;
            if (selectedValue == "Daily")
            
                ProcessType = "Daily";
            
            else
            ProcessType = "Monthly"; 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            worker2.RunWorkerAsync();
           
        }
    }
}
