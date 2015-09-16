using Microsoft.Reporting.WinForms;
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
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.Reports.UserControls;

namespace TimeAttendanceSystem.Reports.ReportForms
{
    /// <summary>
    /// Interaction logic for SDailyShift.xaml
    /// </summary>
    public partial class SDailyShift : Page
    {
        public SDailyShift()
        {
            InitializeComponent();
            selectedShift = new List<Shift>();
            startDate.SelectedDate = DateTime.Today.AddDays(-7);
            endDate.SelectedDate = DateTime.Today;
            RBConsolidated.IsChecked = true;
            LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", ctx.DailySummaries.Where(aa => aa.Criteria == "S").ToList(), "Consolidated Shift Summary Report");

        }
        public DateTime StartDate
        {
            get { return (DateTime)startDate.SelectedValue; }
        }
        public DateTime EndDate
        {
            get { return (DateTime)endDate.SelectedValue; }
        }
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
        TAS2013Entities ctx = new TAS2013Entities();
        private void ButtonGenerate(object sender, RoutedEventArgs e)
        {
            List<DailySummary> _TempViewList = new List<DailySummary>();
            List<DailySummary> _ViewList = ctx.DailySummaries.Where(aa => aa.Criteria == "S" && aa.Date >= StartDate && aa.Date <= EndDate).ToList();


            //for department
            if (selectedShift.Count > 0)
            {
                foreach (var shift in selectedShift)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CriteriaValue == shift.ShiftID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();
            if (RBConsolidated.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", _ViewList, "Consolidated Section Summary");
            if (RBWorkTime.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSWorkSummary.rdlc", _ViewList, "Shift Work Time Summary");
            if (RBEmpstrength.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSEmpStrength.rdlc", _ViewList, "Shift Strength Summary");


            LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", _ViewList, Title);

        }
        private void LoadReport(string Path, List<DailySummary> _List,string Title)
        {
            //rptViewer.Reset();
            string Date = "From: " + StartDate.ToString("dd-MMM-yyyy") + " To: " + EndDate.ToString("dd-MMM-yyyy");
            this.rptViewer.LocalReport.DisplayName = Title;
           
            //rptViewer.ProcessingMode = ProcessingMode.Local;
            //rptViewer.LocalReport.ReportPath = "WpfApplication1.Report1.rdlc";
            rptViewer.LocalReport.ReportPath = Path;
            //System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            //rptViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", _List.AsQueryable());
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(datasource1);
            //ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            //ReportParameter rp2 = new ReportParameter("CompanyName", CommanVariables.CompanyName, false);
            //this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            ReportParameter rp1 = new ReportParameter("Title", Title, false);
            ReportParameter rp2 = new ReportParameter("CompanyName", CommanVariables.CompanyName, false);
            ReportParameter rp3 = new ReportParameter("Date", Date, false);
            this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
            rptViewer.RefreshReport();
        }
    }
}
