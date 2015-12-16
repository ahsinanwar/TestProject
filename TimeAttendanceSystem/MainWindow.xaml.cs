using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Telerik.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeAttendanceSystem.Reports.ReportForms;
using TimeAttendanceSystem.Views;
using TimeAttendanceSystem.Model;
using WPFPieChart;
using TimeAttendanceSystem.BaseClasses;
using Telerik.Windows;
using Telerik.Windows.Controls;
using System.ComponentModel;
using TimeAttendanceSystem.HelperClasses;
using Newtonsoft.Json;
using System.Net;
using TimeAttendanceSystem.Views.AccessControl;
using TASDownloadService.Helper;
using Mantin.Controls.Wpf.Notification;


namespace TimeAttendanceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
       
           
        TAS2013Entities ctx = new TAS2013Entities(); 
        public MainWindow()
        {
            InitializeComponent();
            User userp = new User();
            userp = GlobalClasses.Global.user;
           
            
            WindowState = WindowState.Maximized;
            CheckForRegistered(new BackgroundWorker());
           // _mainFrame.Navigate(new DashView());
            _mainFrame.Navigate(new EmployeeView());
            this.DataContext = userp;
            CommanVariables.CompanyName = "CNS TECHNOLOGIES";
            // this.DataContext = GlobalClasses.Global;
            //_mainFrame.Navigate(new EmployeeView());
            //CommanVariables.CompanyName = "INVEN TECHNOLOGIES";
            
        }

       
           
        public void CommenceTripleChecking()
        {
             BackgroundWorker bw = new BackgroundWorker();
             CheckForRegistered(bw);
        }
      
        private void radContextMenu_ItemClick(object sender, RadRoutedEventArgs e)
        {
            RadMenu menu = (RadMenu)sender;
            RadMenuItem clickedItem = e.OriginalSource as RadMenuItem;

            if (clickedItem != null)
            {
                string header = Convert.ToString(clickedItem.Header);

                switch (header)
                {
                    case "Sections":
                        _mainFrame.Navigate(new SectionView());
                        break;
                    case "Departments":
                        _mainFrame.Navigate(new DepartmentView());
                        break;
                    //case "Division":
                    //    _mainFrame.Navigate(new DivisionView());
                    //    break;
                    case "Designation":
                        _mainFrame.Navigate(new DesignationView());
                        break;
                    case "Crew":
                        _mainFrame.Navigate(new CrewView());
                        break;
                    case "City":
                        _mainFrame.Navigate(new CityView());
                        break;
                    case "Location":
                        _mainFrame.Navigate(new LocationView());
                        break;
                    case "Category":
                        _mainFrame.Navigate(new CategoryView());
                        break;
                    case "Emp Types":
                        _mainFrame.Navigate(new EmpTypeView());
                        break;
                    case "Holidays":
                        _mainFrame.Navigate(new HolidayView());
                        break;
                    case "Users":
                        _mainFrame.Navigate(new UserView());
                        break;
                    case "Application":
                        _mainFrame.Navigate(new LvApplicationView());
                        break;
                    case "Short Leave":
                        _mainFrame.Navigate(new ShortLvView());
                        break;
                    case "Leave Quota App":
                        _mainFrame.Navigate(new TimeAttendanceSystem.Views.LvQuota());
                        break;
                    case "Employee":
                        _mainFrame.Navigate(new Employee());
                        break;
                    case "Employee(Excel)":
                        _mainFrame.Navigate(new EmpDetail());
                        break;

                    case "Short Leaves":
                        _mainFrame.Navigate(new DFShortLeave());
                        break;
                    case "Quota":
                        _mainFrame.Navigate(new DFLeaveQuota());
                        break;
                    case "Daily":
                        _mainFrame.Navigate(new DFDaily());
                        break;
                    case "Monthly":
                        _mainFrame.Navigate(new DFDaily());
                        break;
                    case "Present":
                        _mainFrame.Navigate(new DFPresent());
                        break;
                    case "Leave":
                        _mainFrame.Navigate(new DFLeave());
                        break;
                    case "Absent":
                        _mainFrame.Navigate(new DFAbsent());
                        break;
                    case "Late In":
                        _mainFrame.Navigate(new DFLateIn());
                        break;
                    case "Late Out":
                        _mainFrame.Navigate(new DFLateOut());
                        break;
                    case "Early In":
                        _mainFrame.Navigate(new DFEarlyIn());
                        break;
                    case "Early Out":
                        _mainFrame.Navigate(new DFEarlyOut());
                        break;
                    case "Overtime":
                        _mainFrame.Navigate(new DFOverTime());
                        break;
                    case "Missing":
                        _mainFrame.Navigate(new DFMissingAtt());
                        break;
                    case "Multiple In/Out":
                        _mainFrame.Navigate(new DFMultipleInOut());
                        break;
                    case "Yearly Leaves":
                        _mainFrame.Navigate(new YLSummary());
                        break;
                    case "Leave Quota":
                        _mainFrame.Navigate(new DFLeaveQuota());
                        break;
                    case "Attendance Sheet":
                        _mainFrame.Navigate(new MFAttSheetxaml());
                        break;
                    case "Attendance Summary":
                        _mainFrame.Navigate(new MFAttSummary());
                        break;
                    case "By Company":
                        _mainFrame.Navigate(new SDailyCompany());
                        break;
                    case "By Department":
                        _mainFrame.Navigate(new SDailyDept());
                        break;
                    case "By Type":
                        _mainFrame.Navigate(new SDailyEmpType());
                        break;
                    case "By Section":
                        _mainFrame.Navigate(new SDailySection());
                        break;
                    case "By Shift":
                        _mainFrame.Navigate(new SDailyShift());
                        break;
                    case "Device Setup":
                        _mainFrame.Navigate(new ReaderView());
                        break;
                    case "Upload Templates":
                        _mainFrame.Navigate(new UploadUsers());
                        break;
                    case "Device Manager":
                        _mainFrame.Navigate(new DeviceOperation());
                        break;
                    case "Troubleshoot":
                        _mainFrame.Navigate(new TroubleshootView());
                        break;
                    case "Database":
                        _mainFrame.Navigate(new DatabaseSettings());
                        break;
                    case "Process Attendance":
                        _mainFrame.Navigate(new AttendanceProcess());
                        break;
                    case "Leave Balance":
                        _mainFrame.Navigate(new MRLeaveBalance());
                        break;
                }
            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttEditView());
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            //_mainFrame.Navigate(new DashView());
        }
       
        private void btn_employee_Click(object sender, RoutedEventArgs e)
        {
            
            _mainFrame.Navigate(new EmployeeView());
        }

        private void btn_shift_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new ShiftView());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttEditView());
        }

        //private void btn_leave_Click(object sender, RoutedEventArgs e)
        //{
        //    _mainFrame.Navigate(new LvSetting());
        //}
        private void btn_JobCard_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttEditJobCard());
        }
        private void btn_User_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new UserView());
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttendanceProcess());
        }

        private void CheckForRegistered(BackgroundWorker bw)
        {
            ClientInfo checkForRegistered = ctx.ClientInfoes.FirstOrDefault();
            checkForRegistered = ReviseLicenseFile(bw);

        }
        private ClientInfo ReviseLicenseFile(BackgroundWorker bw)
        {
            ClientInfo ci = ctx.ClientInfoes.FirstOrDefault();
            bw.DoWork += new DoWorkEventHandler(
            delegate(object o, DoWorkEventArgs args)
            {
                BackgroundWorker b = o as BackgroundWorker;


                CheckGodsWraith();
               
            });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                string json = EncDec.GetString(ctx.Options.FirstOrDefault().WelcomeNote);
                Package df = JsonConvert.DeserializeObject<Package>(json);
               
                ci.isActive = df.IsActive;
                ClientLicense cl = ctx.ClientLicenses.Where(aa => aa.LicenseName == df.Licensetype.LicenseName).First();
                cl.NoOfDevices = (short)df.Licensetype.NoOfDevices;
                cl.NoOfEmp = (short)df.Licensetype.NoOfEmployees;
                cl.NoOfUsers = (short)df.Licensetype.NoOfUsers;
                ci.LiscenceTypeID = cl.LicenseID;
                ctx.SaveChanges();
                if (df.Licensetype.TypeId == -1)
                    this.Close();
                
            });
        
 
            bw.RunWorkerAsync();


          
            return ci;

        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void CheckGodsWraith()
        {

            ClientInfo ci = ctx.ClientInfoes.FirstOrDefault();

            String responsebody = null;

            //if (CheckForInternetConnection() == true)
            //    using (WebClient client = new WebClient())
            //    {
            //        System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            //        reqparm.Add("clientinfo", ci.ClientName);
            //        try
            //        {
            //            byte[] responsebytes = client.UploadValues(" https://powerful-lowlands-4417.herokuapp.com/Editpackage", "POST", reqparm);
            //            responsebody = Encoding.UTF8.GetString(responsebytes);

            //            if (responsebody != null)
            //            {
                            
            //                    Package df = JsonConvert.DeserializeObject<Package>(responsebody);
            //                    df = JsonConvert.DeserializeObject<Package>(responsebody);
            //                    Option opt = ctx.Options.FirstOrDefault();
            //                    opt.WelcomeNote = EncDec.GetBytes(responsebody);
            //                    string json = EncDec.GetString(opt.WelcomeNote);
            //                    ci.LiscenceTypeID = df.Licensetype.TypeId;
            //                    if (df.Licensetype.TypeId == -1)
            //                    {
            //                        Application.Current.Shutdown();

            //                    }
            //                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //                    string jsonString = javaScriptSerializer.Serialize(df);
            //                    opt.WelcomeNote = EncDec.GetBytes(jsonString);
            //                    string MacAdd = EncDec.GetMacAddress();
            //                    ClientMAC cm = ctx.ClientMACs.Where(aa => aa.MACAddress == MacAdd).First();
            //                    cm.IsUsing = true;
            //                    ctx.SaveChanges();

                            


            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);

            //        }


            //    }



        }

        private void btn_Database_Click(object sender, RadRoutedEventArgs e)
        {
            _mainFrame.Navigate(new DatabaseSettings());
        }

        private void btn_LogOut_Click(object sender, RadRoutedEventArgs e)
        {
           
        }

       

      
    }
}
