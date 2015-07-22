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

namespace TimeAttendanceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            CheckForRegistered();
            _mainFrame.Navigate(new DashView());
            CommanVariables.CompanyName = "INVEN TECHNOLOGIES"; 
        }

        private void CheckForRegistered()
        {
            

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
                    case "Division":
                        _mainFrame.Navigate(new DivisionView());
                        break;
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
                    case "User":
                        _mainFrame.Navigate(new SectionView());
                        break;
                    case "Application":
                        _mainFrame.Navigate(new LvApplicationView());
                        break;

                    case "Employee":
                        _mainFrame.Navigate(new Employee());
                        break;
                    case "Employee(Excel)":
                        _mainFrame.Navigate(new EmpDetail());
                        break;

                    case "Short Leave":
                        _mainFrame.Navigate(new DFShortLeave());
                        break;
                    case "Quota":
                        _mainFrame.Navigate(new DFDaily());
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
                    case "Over Time":
                        _mainFrame.Navigate(new DFOverTime());
                        break;
                    case "Yearly Leaves":
                        _mainFrame.Navigate(new YLSummary());
                        break;
                    case "Leave Quota":
                        _mainFrame.Navigate(new MFLeaves());
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
                }
            }
        }
        TAS2013Entities ctx = new TAS2013Entities(); 

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttEditView());
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
        private void btn_home_Click(object sender, RoutedEventArgs e)
        {
            
            _mainFrame.Navigate(new DashView());
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

        private void btn_leave_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new LvApplicationView());
        }
        private void btn_JobCard_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new AttEditJobCard());
        }
    }
}
