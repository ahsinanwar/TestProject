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
using TimeAttendanceSystem.ViewModels.VMAttEdit;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttEditView.xaml
    /// </summary>
    public partial class AttEditView : Page
    {
        public AttEditView()
        {
            InitializeComponent();
            vmAttEdit = new VMAttEdit();
            this.DataContext = vmAttEdit;
        }
        VMAttEdit vmAttEdit;
    }
}
