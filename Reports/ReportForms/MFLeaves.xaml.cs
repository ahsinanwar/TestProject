using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace TimeAttendanceSystem.Reports.ReportForms
{
    /// <summary>
    /// Interaction logic for MFLeaves.xaml
    /// </summary>
    public partial class MFLeaves : Page
    {
        public MFLeaves()
        {
            InitializeComponent();
            DateTime dateFrom = UserControlReport.StartDate;
            DateTime dateTo = UserControlReport.EndDate;
            LoadReport(Properties.Settings.Default.ReportPath + "MRLeaveBal.rdlc", GYL(ctx.EmpViews.ToList() , 3));
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
                //foreach (var dept in UserControlReport.selectedDepts)
                //{
                //    _TempViewList.AddRange(_ViewList.Where(aa => aa.De == dept.DeptID).ToList());
                //}
                //_ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for sections
            if (UserControlReport.selectedSecs.Count > 0)
            {
                //foreach (var sec in UserControlReport.selectedSecs)
                //{
                //    _TempViewList.AddRange(_ViewList.Where(aa => aa.Section == sec.SectionID).ToList());
                //}
                //_ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for crews
            if (UserControlReport.selectedCrew.Count > 0)
            {
                //foreach (var cre in UserControlReport.selectedCrew)
                //{
                //    _TempViewList.AddRange(_ViewList.Where(aa => aa.CrewID == cre.CrewID).ToList());
                //}
                //_ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for location
            if (UserControlReport.selectedLoc.Count > 0)
            {
                //foreach (var loc in UserControlReport.selectedLoc)
                //{
                //    _TempViewList.AddRange(_ViewList.Where(aa => aa.LocID == loc.LocID).ToList());
                //}
                //_ViewList = _TempViewList.ToList();
            }
            else
                _TempViewList = _ViewList.ToList();
            _TempViewList.Clear();

            //for shifts
            if (UserControlReport.selectedShift.Count > 0)
            {
                //foreach (var shift in UserControlReport.selectedShift)
                //{
                //    _TempViewList.AddRange(_ViewList.Where(aa => aa.ShiftID == shift.ShiftID).ToList());
                //}
                //_ViewList = _TempViewList.ToList();
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

            LoadReport(Properties.Settings.Default.ReportPath + "MRLeaveBal.rdlc", GYL(_ViewList,3));

        }
        private void LoadReport(string Path, DataTable _List)
        {
            //rptViewer.Reset();
            string DateToFor = "";
            string _Header = "Summary Report";
            this.rptViewer.LocalReport.DisplayName = "Summary Report";
            //rptViewer.ProcessingMode = ProcessingMode.Local;
            //rptViewer.LocalReport.ReportPath = "WpfApplication1.Report1.rdlc";
            rptViewer.LocalReport.ReportPath = Path;
            //System.Security.PermissionSet sec = new System.Security.PermissionSet(System.Security.Permissions.PermissionState.Unrestricted);
            //rptViewer.LocalReport.SetBasePermissionsForSandboxAppDomain(sec);
            ReportDataSource datasource1 = new ReportDataSource("DataSet1", _List);
            rptViewer.LocalReport.DataSources.Clear();
            rptViewer.LocalReport.DataSources.Add(datasource1);
            //ReportParameter rp1 = new ReportParameter("Header", _Header, false);
            //ReportParameter rp2 = new ReportParameter("CompanyName", CommanVariables.CompanyName, false);
            //this.rptViewer.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2 });
            rptViewer.RefreshReport();
        }
        #region --Leave Process--
        private DataTable GYL(List<EmpView> _Emp, int month)
        {
            using (var ctx = new TAS2013Entities())
            {
                DateTime dt = new DateTime();
                List<LvConsumed> _lvConsumed = new List<LvConsumed>();
                LvConsumed _lvTemp = new LvConsumed();
                _lvConsumed = ctx.LvConsumeds.ToList();
                List<LvType> _lvTypes = ctx.LvTypes.ToList();
                foreach (var emp in _Emp)
                {
                    float BeforeCL = 0, UsedCL = 0, BalCL = 0;
                    float BeforeSL = 0, UsedSL = 0, BalSL = 0;
                    float BeforeAL = 0, UsedAL = 0, BalAL = 0;
                    string _month = "";
                    List<LvConsumed> entries = ctx.LvConsumeds.Where(aa => aa.EmpID == emp.EmpID).ToList();
                    LvConsumed eCL = entries.FirstOrDefault(lv => lv.LeaveType == "A");
                    LvConsumed eSL = entries.FirstOrDefault(lv => lv.LeaveType == "C");
                    LvConsumed eAL = entries.FirstOrDefault(lv => lv.LeaveType == "B");
                    if (entries.Count > 0)
                    {
                        switch (month)
                        {
                            case 1:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear;
                                UsedCL = (float)eCL.JanConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear;
                                UsedSL = (float)eSL.JanConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear;
                                UsedAL = (float)eAL.JanConsumed;
                                _month = "January";
                                break;
                            case 2:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - (float)eCL.JanConsumed;
                                UsedCL = (float)eCL.FebConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - (float)eSL.JanConsumed;
                                UsedSL = (float)eSL.FebConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - (float)eAL.JanConsumed;
                                UsedAL = (float)eAL.FebConsumed;
                                break;
                                _month = "Febu";
                            case 3:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed);
                                UsedCL = (float)eCL.MarchConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed);
                                UsedSL = (float)eSL.MarchConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed);
                                UsedAL = (float)eAL.MarchConsumed;
                                break;
                            case 4:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed);
                                UsedCL = (float)eCL.AprConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed);
                                UsedSL = (float)eSL.AprConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed);
                                UsedAL = (float)eAL.AprConsumed;
                                break;
                            case 5:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed);
                                UsedCL = (float)eCL.MayConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed);
                                UsedSL = (float)eSL.MayConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed);
                                UsedAL = (float)eAL.MayConsumed;
                                break;
                            case 6:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed);
                                UsedCL = (float)eCL.JuneConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed);
                                UsedSL = (float)eSL.JuneConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed);
                                UsedAL = (float)eAL.JuneConsumed;
                                break;
                            case 7:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed);
                                UsedCL = (float)eCL.JulyConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed);
                                UsedSL = (float)eSL.JulyConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed);
                                UsedAL = (float)eAL.JulyConsumed;
                                break;
                            case 8:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed + (float)eCL.JulyConsumed);
                                UsedCL = (float)eCL.AugustConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed + (float)eSL.JulyConsumed);
                                UsedSL = (float)eSL.AugustConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed + (float)eAL.JulyConsumed);
                                UsedAL = (float)eAL.AugustConsumed;
                                break;
                            case 9:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed + (float)eCL.JulyConsumed + (float)eCL.AugustConsumed);
                                UsedCL = (float)eCL.SepConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed + (float)eSL.JulyConsumed + (float)eSL.AugustConsumed);
                                UsedSL = (float)eSL.SepConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed + (float)eAL.JulyConsumed + (float)eAL.AugustConsumed);
                                UsedAL = (float)eAL.SepConsumed;
                                break;
                            case 10:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed + (float)eCL.JulyConsumed + (float)eCL.AugustConsumed + (float)eCL.SepConsumed);
                                UsedCL = (float)eCL.OctConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed + (float)eSL.JulyConsumed + (float)eSL.AugustConsumed + (float)eSL.SepConsumed);
                                UsedSL = (float)eSL.OctConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed + (float)eAL.JulyConsumed + (float)eAL.AugustConsumed + (float)eAL.AugustConsumed);
                                UsedAL = (float)eAL.SepConsumed;
                                break;
                            case 11:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed + (float)eCL.JulyConsumed + (float)eCL.AugustConsumed + (float)eCL.SepConsumed + (float)eCL.OctConsumed);
                                UsedCL = (float)eCL.NovConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed + (float)eSL.JulyConsumed + (float)eSL.AugustConsumed + (float)eSL.SepConsumed + (float)eSL.OctConsumed);
                                UsedSL = (float)eSL.NovConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed + (float)eAL.JulyConsumed + (float)eAL.AugustConsumed + (float)eAL.AugustConsumed + (float)eAL.OctConsumed);
                                UsedAL = (float)eAL.NovConsumed;
                                break;
                            case 12:
                                // casual
                                BeforeCL = (float)eCL.TotalForYear - ((float)eCL.JanConsumed + (float)eCL.FebConsumed + (float)eCL.MarchConsumed + (float)eCL.AprConsumed + (float)eCL.MayConsumed + (float)eCL.JuneConsumed + (float)eCL.JulyConsumed + (float)eCL.AugustConsumed + (float)eCL.SepConsumed + (float)eCL.OctConsumed + (float)eCL.NovConsumed);
                                UsedCL = (float)eCL.DecConsumed;
                                //Sick
                                BeforeSL = (float)eSL.TotalForYear - ((float)eSL.JanConsumed + (float)eSL.FebConsumed + (float)eSL.MarchConsumed + (float)eSL.AprConsumed + (float)eSL.MayConsumed + (float)eSL.JuneConsumed + (float)eSL.JulyConsumed + (float)eSL.AugustConsumed + (float)eSL.SepConsumed + (float)eSL.OctConsumed + (float)eSL.NovConsumed);
                                UsedSL = (float)eSL.DecConsumed;
                                //Anual
                                BeforeAL = (float)eAL.TotalForYear - ((float)eAL.JanConsumed + (float)eAL.FebConsumed + (float)eAL.MarchConsumed + (float)eAL.AprConsumed + (float)eAL.MayConsumed + (float)eAL.JuneConsumed + (float)eAL.JulyConsumed + (float)eAL.AugustConsumed + (float)eAL.AugustConsumed + (float)eAL.OctConsumed + (float)eAL.NovConsumed);
                                UsedAL = (float)eAL.DecConsumed;
                                break;

                        }
                        BalCL = (float)(BeforeCL - UsedCL);
                        BalSL = (float)(BeforeSL - UsedSL);
                        BalAL = (float)(BeforeAL - UsedAL);
                        AddDataToDT(emp.EmpNo, emp.EmpName, emp.DesignationName, emp.SectionName, emp.DeptName, emp.TypeName, emp.CatName, emp.LocName, BeforeCL, BeforeSL, BeforeAL, UsedCL, UsedSL, UsedAL, BalCL, BalSL, BalAL, _month);
                    }

                }
            }
            return LvSummaryMonth;
        }
        public void AddDataToDT(string EmpNo, string EmpName, string Designation, string Section,
                                 string Department, string EmpType, string Category, string Location,
                                 float TotalCL, float TotalSL, float TotalAL,
                                 float ConsumedCL, float ConsumedSL, float ConsumedAL,
                                 float BalanaceCL, float BalanaceSL, float BalananceAL, string Month)
        {
            LvSummaryMonth.Rows.Add(EmpNo, EmpName, Designation, Section, Department, EmpType, Category, Location,
                TotalCL, TotalSL, TotalAL, ConsumedCL, ConsumedSL, ConsumedAL,
                BalanaceCL, BalanaceSL, BalananceAL, Month);
        }
        DataTable LvSummaryMonth = new DataTable();

        public void CreateDatatable()
        {
            LvSummaryMonth.Columns.Add("EmpNo", typeof(string));
            LvSummaryMonth.Columns.Add("EmpName", typeof(string));
            LvSummaryMonth.Columns.Add("Designation", typeof(string));
            LvSummaryMonth.Columns.Add("Section", typeof(string));
            LvSummaryMonth.Columns.Add("Department", typeof(string));
            LvSummaryMonth.Columns.Add("EmpType", typeof(string));
            LvSummaryMonth.Columns.Add("Category", typeof(string));
            LvSummaryMonth.Columns.Add("Location", typeof(string));
            LvSummaryMonth.Columns.Add("TotalCL", typeof(float));
            LvSummaryMonth.Columns.Add("TotalSL", typeof(float));
            LvSummaryMonth.Columns.Add("TotalAL", typeof(float));
            LvSummaryMonth.Columns.Add("ConsumedCL", typeof(float));
            LvSummaryMonth.Columns.Add("ConsumedSL", typeof(float));
            LvSummaryMonth.Columns.Add("ConsumedAL", typeof(float));
            LvSummaryMonth.Columns.Add("BalanceCL", typeof(float));
            LvSummaryMonth.Columns.Add("BalanceSL", typeof(float));
            LvSummaryMonth.Columns.Add("BalanceAL", typeof(float));
            LvSummaryMonth.Columns.Add("Remarks", typeof(string));
            LvSummaryMonth.Columns.Add("Month", typeof(string));
        }
        #endregion
    }
}
