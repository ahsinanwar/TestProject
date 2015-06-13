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
using TimeAttendanceSystem.Views;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AttEditView aa = new AttEditView();
            aa.Show();
        }

        private void MenuItemDept_Click(object sender, RoutedEventArgs e)
        {
            DepartmentView aa = new DepartmentView();
            aa.Show();
        }

        private void MenuItemEmp_Click(object sender, RoutedEventArgs e)
        {
            EmployeeView em = new EmployeeView();
            em.Show();
        }

        private void MenuItemShift_Click(object sender, RoutedEventArgs e)
        {
            ShiftView sv = new ShiftView();
            sv.Show();
        }

        private void MenuItemCat_Click(object sender, RoutedEventArgs e)
        {
            CategoryView cv = new CategoryView();
            cv.Show();
        }

        private void MenuItemEmpType_Click(object sender, RoutedEventArgs e)
        {
            EmpTypeView emt = new EmpTypeView();
            emt.Show();
        }

        private void MenuItemSec_Click(object sender, RoutedEventArgs e)
        {
            SectionView scv = new SectionView();
            scv.Show();
        }

        private void MenuItemDiv_Click(object sender, RoutedEventArgs e)
        {
            DivisionView dv = new DivisionView();
            dv.Show();
        }

        private void MenuItemLoc_Click(object sender, RoutedEventArgs e)
        {
            LocationView lv = new LocationView();
            lv.Show();
        }

        private void MenuItemCity_Click(object sender, RoutedEventArgs e)
        {
            CityView cv = new CityView();
            cv.Show();
        }

        private void MenuItemHol_Click(object sender, RoutedEventArgs e)
        {
            HolidayView hv = new HolidayView();
            hv.Show();
        }

        private void MenuItemCrew_Click(object sender, RoutedEventArgs e)
        {
            CrewView cvv = new CrewView();
            cvv.Show();
        }

        private void MenuItemLvApp_Click(object sender, RoutedEventArgs e)
        {
            LvApplicationView lvAppV = new LvApplicationView();
            lvAppV.Show();
        }

        private void MenuItemSLv_Click(object sender, RoutedEventArgs e)
        {
            ShortLvView slvV = new ShortLvView();
            slvV.Show();
        }

        private void MenuItemQuota_Click(object sender, RoutedEventArgs e)
        {
            LvQuotaView lvQV = new LvQuotaView();
            lvQV.Show();
        }

        private void MenuItemRdr_Click(object sender, RoutedEventArgs e)
        {
            ReaderView rv = new ReaderView();
            rv.Show();
        }
    }
}
