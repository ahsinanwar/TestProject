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
using TimeAttendanceSystem.ViewModels.VMHoliday;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for HolidayView.xaml
    /// </summary>
    public partial class HolidayView : Page
    {
        public HolidayView()
        {
            InitializeComponent();
            vmhols = new VMHoliday();
            this.DataContext = vmhols;
        }
        VMHoliday vmhols;

        private void MainGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
