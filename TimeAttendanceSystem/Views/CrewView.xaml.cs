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
using TimeAttendanceSystem.ViewModels.VMCrew;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for CrewView.xaml
    /// </summary>
    public partial class CrewView : Page
    {
        public CrewView()
        {
            InitializeComponent();
            vmcrew = new VMCrew();
            this.DataContext = vmcrew;
        }
        VMCrew vmcrew; 
    }
}
