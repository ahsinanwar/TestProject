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
    /// Interaction logic for SDailyEmpType.xaml
    /// </summary>
    public partial class SDailyEmpType : Page
    {
         public SDailyEmpType()
         {
            InitializeComponent();
            selectedType = new List<EmpType>();
            startDate.SelectedDate = DateTime.Today.AddDays(-7);
            endDate.SelectedDate = DateTime.Today;
            RBConsolidated.IsChecked = true;
            LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", ctx.DailySummaries.Where(aa => aa.Criteria == "T").ToList(), "Consolidated Summary Report by Employement Type");

    
         }
      public DateTime StartDate
        {
            get { return (DateTime)startDate.SelectedValue; }
        }
        public DateTime EndDate
        {
            get { return (DateTime)endDate.SelectedValue; }
        }
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
      
        TAS2013Entities ctx = new TAS2013Entities();
        private void ButtonGenerate(object sender, RoutedEventArgs e)
        {
            List<DailySummary> _TempViewList = new List<DailySummary>();
            List<DailySummary> _ViewList = ctx.DailySummaries.Where(aa => aa.Criteria == "T" && aa.Date >= StartDate && aa.Date <= EndDate).ToList();


            //for department
            if (selectedType.Count > 0)
            {
                foreach (var type in selectedType)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CriteriaValue == type.TypeID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();
            if (RBConsolidated.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", _ViewList, "Consolidated Summary by Employement Type");
            if (RBWorkTime.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSWorkSummary.rdlc", _ViewList, "Work Time Summary by Employement Type");
            if (RBEmpstrength.IsChecked == true)
                LoadReport(Properties.Settings.Default.ReportPath + "DSEmpStrength.rdlc", _ViewList, "Strength Summary by Employement Type");


            LoadReport(Properties.Settings.Default.ReportPath + "DSConsolidated.rdlc", _ViewList, Title);
        }
        
        private void LoadReport(string Path, List<DailySummary> _List, string Title)
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
            ReportParameter rp1 = new ReportParameter("Title", Title, false);
            ReportParameter rp2 = new ReportParameter("CompanyName", CommanVariables.CompanyName, false);
            ReportParameter rp3 = new ReportParameter("Date", Date, false);
            this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 }); rptViewer.RefreshReport();
        }
    }
}