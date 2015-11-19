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
using TimeAttendanceSystem.ViewModels.VMLvQuota;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for LvSetting.xaml
    /// </summary>
    public partial class LvQuota : Page
    {
        SelectEmpWindow window;
        VMLvQuota vmlvquota;
        public LvQuota()
        {
            try
            {
                InitializeComponent();
                vmlvquota = new VMLvQuota();
                this.DataContext = vmlvquota;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
        VMLvQuota vmquota;
        private void btn_empView_Click(object sender, RoutedEventArgs e)
        {
            window = new SelectEmpWindow();


            if ((bool)window.ShowDialog())
            {
                Console.WriteLine(window._selectedEmp);
                txtEmpID.Text = window._selectedEmp.EmpID.ToString();
                txtEmpName.Text = window._selectedEmp.EmpName.ToString();
                vmlvquota.selectedLvQuota.Emp = window._selectedEmp;
                vmlvquota.selectedLvQuota.EmpID = vmlvquota.selectedLvQuota.Emp.EmpID;
               // vmlvquota.selectedLvQuota.Emp = window._selectedEmp;
                //vmlvquota.selectedLvQuota.Emp.EmpName = window._selectedEmp.EmpName;
            }
        }  
    }
}
