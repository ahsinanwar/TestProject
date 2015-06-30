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
        //SelectEmpWindow window;
        VMAttEdit vmAttEdit;
        public AttEditView()
        {
            InitializeComponent();
            vmAttEdit = new VMAttEdit();
            this.DataContext = vmAttEdit;
           // this.radGridView.BeginningEdit += new EventHandler<Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs>(radGridView_BeginningEdit);
        }
        private void EditGrid_BeginningEdit(object sender, Telerik.Windows.Controls.GridViewBeginningEditRoutedEventArgs e)
        {
            e.Cancel = true;
        }
       
       
        //private void btn_empView_Click(object sender, RoutedEventArgs e)
        //{
        //    window = new SelectEmpWindow();


        //    if ((bool)window.ShowDialog())
        //    {
        //        Console.WriteLine(window._selectedEmp);
        //        txtEmpID.Text = window._selectedEmp.EmpID.ToString();
        //    }
        //}  
    }
}
