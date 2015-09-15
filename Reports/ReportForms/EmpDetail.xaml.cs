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
using Microsoft.Reporting.WinForms;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.BaseClasses;

namespace TimeAttendanceSystem.Reports.ReportForms
{
    /// <summary>
    /// Interaction logic for EmpDetail.xaml
    /// </summary>
    public partial class EmpDetail : Page
    {
        public EmpDetail()
        {
            InitializeComponent();
            LoadReport(Properties.Settings.Default.ReportPath + "EmployeeDetail.rdlc", ctx.EmpViews.ToList());
        }
        TAS2013Entities ctx = new TAS2013Entities();
        private void ButtonGenerate(object sender, RoutedEventArgs e)
        {
            List<EmpView> _TempViewList = new List<EmpView>();
            DateTime dateFrom = UserControlReport.StartDate;
            DateTime dateTo = UserControlReport.EndDate;
            List<EmpView> _ViewList = ctx.EmpViews.ToList();

            if (UserControlReport.selectedEmps.Count > 0)
            {
                foreach (var emp in UserControlReport.selectedEmps)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.EmpID == emp.EmpID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();


            //for department
            if (UserControlReport.selectedDepts.Count > 0)
            {
                foreach (var dept in UserControlReport.selectedDepts)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.DeptID == dept.DeptID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for sections
            if (UserControlReport.selectedSecs.Count > 0)
            {
                foreach (var sec in UserControlReport.selectedSecs)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.SecID == sec.SectionID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for crews
            if (UserControlReport.selectedCrew.Count > 0)
            {
                foreach (var cre in UserControlReport.selectedCrew)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.CrewID == cre.CrewID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for location
            if (UserControlReport.selectedLoc.Count > 0)
            {
                foreach (var loc in UserControlReport.selectedLoc)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.LocID == loc.LocID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for shifts
            if (UserControlReport.selectedShift.Count > 0)
            {
                foreach (var shift in UserControlReport.selectedShift)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.ShiftID == shift.ShiftID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();


            _TempViewList.Clear();

            //for emptype
            if (UserControlReport.selectedType.Count > 0)
            {
                foreach (var cat in UserControlReport.selectedType)
                {
                    _TempViewList.AddRange(_ViewList.Where(aa => aa.TypeID == cat.TypeID).ToList());
                }
                _ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            LoadReport(Properties.Settings.Default.ReportPath + "EmployeeDetail.rdlc", _ViewList);

        }
        private void LoadReport(string Path, List<EmpView> _List)
        {
            //rptViewer.Reset();
            string DateToFor = "";
            string _Header = "Employee Detail Report";
            this.rptViewer.LocalReport.DisplayName = "Employee Detail(Excel)";
            this.rptViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            //rptViewer.ProcessingMode = ProcessingMode.Local;
            //rptViewer.LocalReport.ReportPath = "WpfApplication1.Report1.rdlc";
            rptViewer.LocalReport.ReportPath = Path;
            //System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            //rptViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", _List.AsQueryable());
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(datasource1);
            ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            ReportParameter rp2 = new ReportParameter("CompanyName", CommanVariables.CompanyName, false);
            this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp1,rp2 });
            rptViewer.RefreshReport();
        }
    }
}
