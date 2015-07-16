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
    /// Interaction logic for SDailyDept.xaml
    /// </summary>
    public partial class SDailyDept : Page
    {
        public SDailyDept()
        {
            InitializeComponent();
            selectedDepts = new List<Department>();
        }
        public DateTime StartDate
        {
            get { return (DateTime)startDate.SelectedValue; }
        }
        public DateTime EndDate
        {
            get { return (DateTime)endDate.SelectedValue; }
        }
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
        TAS2013Entities ctx = new TAS2013Entities();
        private void ButtonGenerate(object sender, RoutedEventArgs e)
        {
            List<DailySummary> _TempViewList = new List<DailySummary>();
            List<DailySummary> _ViewList = ctx.DailySummaries.ToList();


            //for department
            if (selectedDepts.Count > 0)
            {
                foreach (var dept in selectedDepts)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CriteriaValue == dept.DeptID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();


            

            LoadReport(Properties.Settings.Default.ReportPath + "DROverTime.rdlc", _ViewList);

        }
        private void LoadReport(string Path, List<DailySummary> _List)
        {
            //rptViewer.Reset();
            string DateToFor = "";
            string _Header = "Daily Attendance Report";
            this.rptViewer.LocalReport.DisplayName = "Daily Attendance Report";
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
            rptViewer.RefreshReport();
        }
    }
}
