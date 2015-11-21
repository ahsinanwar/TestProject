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
using TimeAttendanceSystem.ViewModels.VMCategory;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for CategoryView.xaml
    /// </summary>
    public partial class CategoryView : Page
    {
        VMCategory vmCategory;
        public CategoryView()
        {
            try
            {
                InitializeComponent();
                vmCategory = new VMCategory();
                this.DataContext = vmCategory;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
    }
}
