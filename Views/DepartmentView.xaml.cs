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
using System.Windows.Shapes;
using TimeAttendanceSystem.ViewModels.VMDepartment;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for DepartmentView.xaml
    /// </summary>
    public partial class DepartmentView : Page
    {
        public DepartmentView()
        {
            InitializeComponent();
            vmdepts = new VMDepartments();
            this.DataContext = vmdepts;
        }
        VMDepartments vmdepts;
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Windows loaded");
        }
    }
}
