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

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttendanceProcess.xaml
    /// </summary>
    public partial class AttendanceProcess : Page
    {
        public AttendanceProcess()
        {
            InitializeComponent();
            DateTime DateFrom = new DateTime();
            DateTime DateTo = new DateTime();
            bool isActive = false;
        }
    }
}
