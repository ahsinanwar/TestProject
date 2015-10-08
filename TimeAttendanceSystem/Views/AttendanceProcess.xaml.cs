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
using TimeAttendanceSystem.AttendanceProcessor;
using TimeAttendanceSystem.Model;

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
            InitializeComponent();
            DateTime DateFrom = new DateTime();
            DateTime DateTo = new DateTime();
            bool isActive = false;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;

        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Object[] arg = e.Argument as Object[];
            TAS2013Entities ctx = new TAS2013Entities();
            // Download Attendance From Readers
            Downloader d = new Downloader();
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
   // run all background tasks here
}

private void worker_RunWorkerCompleted(object sender, 
                                       RunWorkerCompletedEventArgs e)
{
  //update ui once worker complete his work
}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Object[] args = { dtpStart.SelectedDate.Value, dtpEnd.SelectedDate.Value};
            worker.RunWorkerAsync(args );
            
        }
    }
}
