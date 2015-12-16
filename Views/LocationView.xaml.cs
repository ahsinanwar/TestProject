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
using TimeAttendanceSystem.ViewModels.VMLocation;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for LocationView.xaml
    /// </summary>
    public partial class LocationView : Page
    {
        public LocationView()
        {
            InitializeComponent();
            vmlocs = new VMLocation();
            this.DataContext = vmlocs;
        }
        VMLocation vmlocs;
    }
}
