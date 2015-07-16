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
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.Reports.UserControls;

namespace TimeAttendanceSystem.Reports.ReportForms
{
    /// <summary>
    /// Interaction logic for SDailySection.xaml
    /// </summary>
    public partial class SDailySection : Page
    {
        public SDailySection()
        {
            InitializeComponent();
            selectedSecs = new List<TimeAttendanceSystem.Model.Section>();
        }
        public DateTime StartDate
        {
            get { return (DateTime)startDate.SelectedValue; }
        }
        public DateTime EndDate
        {
            get { return (DateTime)endDate.SelectedValue; }
        }
        #region -- Section filter --
        //TAS2013Entities context = new TAS2013Entities();
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
        TAS2013Entities ctx = new TAS2013Entities();
        private void ButtonGenerate(object sender, RoutedEventArgs e)
        {
            List<DailySummary> _TempViewList = new List<DailySummary>();
            List<DailySummary> _ViewList = ctx.DailySummaries.ToList();


            //for department
            if (selectedSecs.Count > 0)
            {
                foreach (var dept in selectedSecs)
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
            string _Header = "By Section";
            this.rptViewer.LocalReport.DisplayName = "By Section";
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
