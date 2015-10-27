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

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttendanceProcess.xaml
    /// </summary>
    public partial class AttendanceProcess : Page
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
       
        public AttendanceProcess()
        {
            try
            {
                InitializeComponent();
                DateTime DateFrom = new DateTime();
                DateTime DateTo = new DateTime();
                bool isActive = false;
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Object[] arg = e.Argument as Object[];
            TAS2013Entities ctx = new TAS2013Entities();

            // Download Attendance From Readers
          //  Downloader d = new Downloader();
         //   d.DownloadDataInIt();
            DateTime dateStart = (DateTime)arg[0] ;
            DateTime dateEnd = (DateTime)arg[1];
            List<Emp> emps = new List<Emp>();
            List<AttData> attdata = new List<AttData>();
            emps = ctx.Emps.Where(aa => aa.Status == true).ToList();
            
            while (dateStart <= dateEnd)
            {
                attdata.Clear();
                if (ctx.AttProcesses.Where(aa => aa.ProcessDate == dateStart).Count() == 0)
                {
                    ProcessAttendance p = new ProcessAttendance();
                    p.ProcessDailyAttendance();
                }
                else
                {
                    ManualProcess mp = new ManualProcess();
                    attdata = ctx.AttDatas.Where(aa => aa.AttDate == dateStart).ToList();
                    mp.ManualProcessAttendance(dateStart, emps, attdata);
                }
                dateStart = dateStart.AddDays(1);
            }
            // Process Edit Attendance Entries if any
            ProcessEditAttendanceEntries pea = new ProcessEditAttendanceEntries();
            pea.ProcessManualEditAttendance(dateStart, dateEnd);
            //Process Job cards if any
            ApplyJobCard(dateStart, dateEnd);
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
            Object[] args = { dtpStart.SelectedDate.Value, dtpEnd.SelectedDate.Value};
            worker.RunWorkerAsync(args );
            
        }
    }
}
