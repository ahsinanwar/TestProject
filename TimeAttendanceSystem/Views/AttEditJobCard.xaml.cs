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
                    ListBoxData.Items.Add(item.ShiftName);
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
                    ListBoxData.Items.Add(item.SectionName);
            }

            if (rbEmployee.IsChecked == true)
            {
                windowEmp = new RFEmployees(selectedEmps);
                if ((bool)windowEmp.ShowDialog())
                {
                    ListBoxData.Items.Clear();
                    selectedEmps = windowEmp.selectedEmps;

                }

                foreach (var item in selectedEmps)
                    ListBoxData.Items.Add(item.EmpName);
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
                    ListBoxData.Items.Add(item.CrewName);
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
                    JobCardApp jobCardApp = new JobCardApp();
                    short _WorkCardID = new short();
                    jobCardApp.CardType = _WorkCardID;
                    jobCardApp.DateCreated = DateTime.Now;
                    //jobCardApp.DateStarted = db.JobCardApps.FirstOrDefault(where DateTime = D)
                    //jobCardApp.DateEnded = db.JobCardApps.FirstOrDefault();
                    context.JobCardApps.Add(jobCardApp);
                    context.SaveChanges();
                    if (context.SaveChanges() > 0)
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
           
       }
    }
}
