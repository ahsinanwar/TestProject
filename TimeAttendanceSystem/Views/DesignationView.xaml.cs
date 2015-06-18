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
using TimeAttendanceSystem.ViewModels.VMDesignation;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for Designation.xaml
    /// </summary>
    public partial class DesignationView : Page
    {
        public DesignationView()
        {
            InitializeComponent();
            vmdesgs = new VMDesignation();
            this.DataContext = vmdesgs;
        }
        VMDesignation vmdesgs;
    }
}
