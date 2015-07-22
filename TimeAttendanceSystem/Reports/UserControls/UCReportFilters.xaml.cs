using System;
using System.Collections.Generic;
using System.Globalization;
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
            selectedShift = new List<Shift>();
            selectedType = new List<EmpType>();
            selectedCrew = new List<Crew>();
            startDate.SelectedDate = new DateTime(2015,03,20);
            endDate.SelectedDate = DateTime.Today;
        }
        public DateTime StartDate
        {
            get { return (DateTime)startDate.SelectedValue; }
        }
        public DateTime EndDate
        {
            get { return (DateTime)endDate.SelectedValue; }
        }

        #region -- Employee filter --
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
        #endregion

        #region -- Department filter --
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
        #endregion

        #region -- Section filter --
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
                ListBoxSec.Items.Add(item.SectionName);
        }

        private void btnClearSec_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSec.Items.Clear();
            selectedSecs.Clear();
        }
        #endregion

        #region -- Location filter --
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
        #endregion

        #region -- Shift filter --
        public List<Shift> selectedShift;
        RFShifts windowShift;
        private void btnAddShift_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShift.Items.Clear();
            windowShift = new RFShifts(selectedShift);
            if ((bool)windowShift.ShowDialog())
            {
                selectedShift.Clear();
                selectedShift = windowShift.selectedShifts;
            }
            foreach (var item in selectedShift)
                ListBoxShift.Items.Add(item.ShiftName);
        }

        private void btnClearShift_Click(object sender, RoutedEventArgs e)
        {
            ListBoxShift.Items.Clear();
            selectedShift.Clear();
        }
        #endregion

        #region -- Employee Type filter --
        public List<EmpType> selectedType;
        RFEmpTypes windowType;
        private void btnAddEmpType_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmpType.Items.Clear();
            windowType = new RFEmpTypes(selectedType);
            if ((bool)windowType.ShowDialog())
            {
                selectedType.Clear();
                selectedType = windowType.selectedEmpTypes;
            }
            foreach (var item in selectedType)
                ListBoxEmpType.Items.Add(item.TypeName);
        }

        private void btnClearEmpType_Click(object sender, RoutedEventArgs e)
        {
            ListBoxEmpType.Items.Clear();
            selectedType.Clear();
        }
        #endregion

        #region -- Crew filter --
        public List<Crew> selectedCrew;
        RFCrews windowCrew;
        private void btnAddCrew_Click(object sender, RoutedEventArgs e)
        {
            ListBoxCrew.Items.Clear();
            windowCrew = new RFCrews(selectedCrew);
            if ((bool)windowCrew.ShowDialog())
            {
                selectedCrew.Clear();
                selectedCrew = windowCrew.selectedCrews;
            }
            foreach (var item in selectedCrew)
                ListBoxCrew.Items.Add(item.CrewName);
        }

        private void btnClearCrew_Click(object sender, RoutedEventArgs e)
        {
            ListBoxCrew.Items.Clear();
            selectedCrew.Clear();
        }
        #endregion
    }
}
