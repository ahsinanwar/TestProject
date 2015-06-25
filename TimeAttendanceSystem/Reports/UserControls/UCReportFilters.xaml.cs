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
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Reports.UserControls
{
    /// <summary>
    /// Interaction logic for UCReportFilters.xaml
    /// </summary>
    public partial class UCReportFilters : UserControl
    {
        public UCReportFilters()
        {
            InitializeComponent();
            selectedEmps = new List<Emp>();
            selectedDepts = new List<Department>();
            selectedSecs = new List<Model.Section>();
            selectedLoc = new List<Location>();
        }

        public List<Emp> selectedEmps;
        RFEmployees window;
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmp.Items.Clear();
            window = new RFEmployees(selectedEmps);
            if ((bool)window.ShowDialog())
            {
                selectedEmps.Clear();
                selectedEmps = window.selectedEmps;
            }
            foreach (var item in selectedEmps)
                ListBoxEmp.Items.Add(item.EmpName);
        }

        private void btnClearEmployee_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmp.Items.Clear();
            selectedEmps.Clear();
        }
        public List<Department> selectedDepts;
        RFDepts windowDept;
        private void btnAddDept_Click(object sender, RoutedEventArgs e)
        {
            ListBoxDept.Items.Clear();
            windowDept = new RFDepts(selectedDepts);
            if ((bool)windowDept.ShowDialog())
            {
                selectedDepts.Clear();
                selectedDepts = windowDept.selectedDepts;
            }
            foreach (var item in selectedDepts)
                ListBoxDept.Items.Add(item.DeptName);
        }

        private void btnClearDept_Click(object sender, RoutedEventArgs e)
        {
            ListBoxDept.Items.Clear();
            selectedDepts.Clear();
        }

        public List<TimeAttendanceSystem.Model.Section> selectedSecs;
        RFSections windowSec;
        private void btnAddSec_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSec.Items.Clear();
            windowSec = new RFSections(selectedSecs);
            if ((bool)windowSec.ShowDialog())
            {
                selectedSecs.Clear();
                selectedSecs = windowSec.selectedSecs;
            }
            foreach (var item in selectedSecs)
                ListBoxDept.Items.Add(item.SectionName);
        }

        private void btnClearSec_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSec.Items.Clear();
            selectedSecs.Clear();
        }

        public List<Location> selectedLoc;
        RFLocations windowLoc;
        private void btnAddLoc_Click(object sender, RoutedEventArgs e)
        {
            ListBoxLoc.Items.Clear();
            windowLoc = new RFLocations(selectedLoc);
            if ((bool)windowLoc.ShowDialog())
            {
                selectedLoc.Clear();
                selectedLoc = windowLoc.selectedLocs;
            }
            foreach (var item in selectedLoc)
                ListBoxLoc.Items.Add(item.LocName);
        }

        private void btnClearLoc_Click(object sender, RoutedEventArgs e)
        {
            ListBoxLoc.Items.Clear();
            selectedLoc.Clear();
        }
        public List<Location> selectedShift;
        RFLocations windowShift;
        private void btnAddShift_Click(object sender, RoutedEventArgs e)
        {
            ListBoxLoc.Items.Clear();
            windowLoc = new RFLocations(selectedShift);
            if ((bool)windowLoc.ShowDialog())
            {
                selectedShift.Clear();
                selectedShift = windowLoc.selectedLocs;
            }
            foreach (var item in selectedShift)
                ListBoxLoc.Items.Add(item.LocName);
        }

        private void btnClearShift_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShift.Items.Clear();
            selectedShift.Clear();
        }
    }
}
