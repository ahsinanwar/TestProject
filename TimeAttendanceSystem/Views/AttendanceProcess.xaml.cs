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
        public AttendanceProcess()
        {
            InitializeComponent();
            DateTime DateFrom = new DateTime();
            DateTime DateTo = new DateTime();
            bool isActive = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            TAS2013Entities ctx = new TAS2013Entities();
            // Download Attendance From Readers
            Downloader d = new Downloader();
            d.DownloadDataInIt();
            DateTime dateStart = dtpStart.SelectedDate.Value;
            DateTime dateEnd = dtpEnd.SelectedDate.Value;
            List<Emp> emps = new List<Emp>();
            List<AttData> attdata = new List<AttData>();
            emps = ctx.Emps.Where(aa=>aa.Status==true).ToList();
            while(dateStart <= dateEnd)
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
                dateStart  = dateStart.AddDays(1);
            }
            
        }
    }
}
