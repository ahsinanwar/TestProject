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
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMShortLv;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for ShortLvView.xaml
    /// </summary>
    public partial class ShortLvView : Page
    {
        SelectEmpWindow window;
        VMShortLeave vmlvshorts;
        TAS2013Entities context;
        public ShortLvView()
        {
            try
            {
                InitializeComponent();
                vmlvshorts = new VMShortLeave();
                this.DataContext = vmlvshorts;
            }
            catch (Exception ex)
            {
                
             MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
        private void btn_empView_Click(object sender, RoutedEventArgs e)
        {
            window = new SelectEmpWindow();
            if ((bool)window.ShowDialog())
            {
                vmlvshorts.selectedEmpAndShortLv.Employee = window._selectedEmp;
                txtEmpID.Text = window._selectedEmp.EmpNo;
                txtEmpName.Text = window._selectedEmp.EmpName;
            }
        }

       
    }
}
