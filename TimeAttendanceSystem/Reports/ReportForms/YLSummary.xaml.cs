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
using System.Windows.Shapes;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Reports.ReportForms
{
    /// <summary>
    /// Interaction logic for YLSummary.xaml
    /// </summary>
    public partial class YLSummary : Page
    {
        public YLSummary()
        {
            InitializeComponent();
            DateTime dateFrom = UserControlReport.StartDate;
            DateTime dateTo = UserControlReport.EndDate;
            LoadReport(Properties.Settings.Default.ReportPath + "YLSummary.rdlc",GYL(ctx.EmpViews.ToList()));
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

            LoadReport(Properties.Settings.Default.ReportPath + "YLSummary.rdlc", GYL(_ViewList));

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
        private DataTable GYL(List<EmpView> _Emp)
        {
            List<LvConsumed> leaveQuota = new List<LvConsumed>();
            List<LvConsumed> tempLeaveQuota = new List<LvConsumed>();
            leaveQuota = ctx.LvConsumeds.ToList();
            foreach (var emp in _Emp)
            {
                int EmpID;
                string EmpNo = ""; string EmpName = "";
                float TotalAL = 0; float BalAL = 0; float TotalCL = 0; float BalCL = 0; float TotalSL = 0; float BalSL = 0;
                float JanAL = 0; float JanCL = 0; float JanSL = 0; float FebAL = 0; float FebCL = 0; float FebSL = 0;
                float MarchAL = 0; float MarchCL = 0; float MarchSL = 0;
                float AprilAL = 0; float AprilCL = 0; float AprilSL = 0;
                float MayAL = 0; float MayCL = 0; float MaySL = 0;
                float JunAL = 0; float JunCL = 0; float JunSL = 0;
                float JullyAL = 0; float JullyCL = 0; float JullySL = 0;
                float AugAL = 0; float AugCL = 0; float AugSL = 0;
                float SepAL = 0; float SepCL = 0; float SepSL = 0;
                float OctAL = 0; float OctCL = 0; float OctSL = 0;
                float NovAL = 0; float NovCL = 0; float NovSL = 0;
                float DecAL = 0; float DecCL = 0; float DecSL = 0;
                string Remarks = ""; string DeptName; short DeptID; string LocationName; short LocationID; string SecName; short SecID; string DesgName; short DesigID; string CrewName; short CrewID; string CompanyName; short CompanyID;
                tempLeaveQuota = leaveQuota.Where(aa => aa.EmpID == emp.EmpID).ToList();
                foreach (var leave in tempLeaveQuota)
                {
                    EmpID = emp.EmpID;
                    EmpNo = emp.EmpNo;
                    EmpName = emp.EmpName;
                    DeptID = (short)emp.DeptID;
                    DeptName = emp.DeptName;
                    LocationName = emp.LocName;
                    LocationID = (short)emp.LocID;
                    SecName = emp.SectionName;
                    SecID = (short)emp.SecID;
                    DesgName = emp.DesignationName;
                    DesigID = (short)emp.DesigID;
                    CrewName = emp.CrewName;
                    CrewID = (short)emp.CrewID;
                    switch (leave.LeaveType)
                    {
                        case "A"://Casual
                            JanCL = (float)leave.JanConsumed;
                            FebCL = (float)leave.FebConsumed;
                            MarchCL = (float)leave.MarchConsumed;
                            AprilCL = (float)leave.AprConsumed;
                            MayCL = (float)leave.MayConsumed;
                            JunCL = (float)leave.JuneConsumed;
                            JullyCL = (float)leave.JulyConsumed;
                            AugCL = (float)leave.AugustConsumed;
                            SepCL = (float)leave.SepConsumed;
                            OctCL = (float)leave.OctConsumed;
                            NovCL = (float)leave.NovConsumed;
                            DecCL = (float)leave.DecConsumed;
                            TotalCL = (float)leave.TotalForYear;
                            BalCL = (float)leave.YearRemaining;
                            break;
                        case "B"://Anual
                            JanAL = (float)leave.JanConsumed;
                            FebAL = (float)leave.FebConsumed;
                            MarchAL = (float)leave.MarchConsumed;
                            AprilAL = (float)leave.AprConsumed;
                            MayAL = (float)leave.MayConsumed;
                            JunAL = (float)leave.JuneConsumed;
                            JullyAL = (float)leave.JulyConsumed;
                            AugAL = (float)leave.AugustConsumed;
                            SepAL = (float)leave.SepConsumed;
                            OctAL = (float)leave.OctConsumed;
                            NovAL = (float)leave.NovConsumed;
                            DecAL = (float)leave.DecConsumed;
                            TotalAL = (float)leave.TotalForYear;
                            BalAL = (float)leave.YearRemaining;
                            break;
                        case "C"://Sick
                            JanSL = (float)leave.JanConsumed;
                            FebSL = (float)leave.FebConsumed;
                            MarchSL = (float)leave.MarchConsumed;
                            AprilSL = (float)leave.AprConsumed;
                            MaySL = (float)leave.MayConsumed;
                            JunSL = (float)leave.JuneConsumed;
                            JullySL = (float)leave.JulyConsumed;
                            AugSL = (float)leave.AugustConsumed;
                            SepSL = (float)leave.SepConsumed;
                            OctSL = (float)leave.OctConsumed;
                            NovSL = (float)leave.NovConsumed;
                            DecSL = (float)leave.DecConsumed;
                            TotalSL = (float)leave.TotalForYear;
                            BalSL = (float)leave.YearRemaining;
                            break;
                    }
                    AddDataToDT(EmpID, EmpNo, EmpName, TotalAL, BalAL, TotalCL, BalCL, TotalSL, BalSL, JanAL, JanCL, JanSL, FebAL, FebCL, FebSL, MarchAL, MarchCL, MarchSL, AprilAL, AprilCL, AprilSL, MayAL, MayCL, MaySL, JunAL, JunCL, JunSL, JullyAL, JullyCL, JullySL, AugAL, AugCL, AugSL, SepAL, SepCL, SepSL, OctAL, OctCL, OctSL, NovAL, NovCL, NovSL, DecAL, DecCL, DecSL, Remarks, DeptName, (short)DeptID, LocationName, (short)LocationID, SecName, (short)SecID, DesgName, DesigID, CrewName, CrewID);
                }
            }
            return MYLeaveSummaryDT;
        }



        public void AddDataToDT(int EmpID, string EmpNo, string EmpName, float TotalAL, float BalAL,
            float TotalCL, float BalCL, float TotalSL, float BalSL,
            float JanAL, float JanCL, float JanSL,
            float FebAL, float FebCL, float FebSL,
            float MarchAL, float MarchCL, float MarchSL,
            float AprilAL, float AprilCL, float AprilSL,
            float MayAL, float MayCL, float MaySL,
            float JunAL, float JunCL, float JunSL,
            float JullyAL, float JullyCL, float JullySL,
            float AugAL, float AugCL, float AugSL,
            float SepAL, float SepCL, float SepSL,
            float OctAL, float OctCL, float OctSL,
            float NovAL, float NovCL, float NovSL,
            float DecAL, float DecCL, float DecSL,
            string Remarks, string DeptName, short DeptID, string LocationName, short LocationID, string SecName, short SecID, string DesgName, short DesgID, string CrewName, short CrewID)
        {
            MYLeaveSummaryDT.Rows.Add(EmpID, EmpNo, EmpName, TotalAL, BalAL, TotalCL, BalCL, TotalSL, BalSL, JanAL, JanCL, JanSL, FebAL, FebCL, FebSL, MarchAL, MarchCL, MarchSL,
                AprilAL, AprilCL, AprilSL, MayAL, MayCL, MaySL, JunAL, JunCL, JunSL, JullyAL, JullyCL, JullySL, AugAL, AugCL, AugSL,
                SepAL, SepCL, SepSL, OctAL, OctCL, OctSL, NovAL, NovCL, NovSL, DecAL, DecCL, DecSL, Remarks, DeptName, DeptID, LocationName, LocationID, CrewName, CrewID, SecName, SecID);
        }
        DataTable MYLeaveSummaryDT = new DataTable();
        public void CreateDataTable()
        {
            MYLeaveSummaryDT.Columns.Add("EmpID", typeof(int));
            MYLeaveSummaryDT.Columns.Add("EmpNo", typeof(string));
            MYLeaveSummaryDT.Columns.Add("EmpName", typeof(string));

            MYLeaveSummaryDT.Columns.Add("TotalAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("BalAL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("TotalCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("BalCL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("TotalSL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("BalSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("JanAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JanCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JanSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("FebAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("FebCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("FebSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("MarchAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("MarchCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("MarchSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("AprilAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("AprilCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("AprilSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("MayAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("MayCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("MaySL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("JunAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JunCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JunSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("JulyAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JulyCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("JulySL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("AugAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("AugCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("AugSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("SepAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("SepCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("SepSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("OctAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("OctCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("OctSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("NovAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("NovCL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("NovSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("DecAL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("DecL", typeof(float));
            MYLeaveSummaryDT.Columns.Add("DecSL", typeof(float));

            MYLeaveSummaryDT.Columns.Add("Remarks", typeof(string));
            MYLeaveSummaryDT.Columns.Add("DeptName", typeof(string));
            MYLeaveSummaryDT.Columns.Add("DeptID", typeof(short));
            MYLeaveSummaryDT.Columns.Add("SecName", typeof(string));
            MYLeaveSummaryDT.Columns.Add("SecID", typeof(short));
            MYLeaveSummaryDT.Columns.Add("DesgName", typeof(string));
            MYLeaveSummaryDT.Columns.Add("DesgID", typeof(short));
            MYLeaveSummaryDT.Columns.Add("CrewName", typeof(string));
            MYLeaveSummaryDT.Columns.Add("CrewID", typeof(short));
            MYLeaveSummaryDT.Columns.Add("LocationName", typeof(string));
            MYLeaveSummaryDT.Columns.Add("LocationID", typeof(short));
        }
        #endregion
    }
}
