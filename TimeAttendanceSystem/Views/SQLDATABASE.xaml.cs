using System;
using System.Collections.Generic;
using System.Data.Sql;
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

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for SQLDATABASE.xaml
    /// </summary>
    public partial class SQLDATABASE : Window
    {
        public SQLDATABASE()
        {
            try
            {
                InitializeComponent();
                SqlDataSourceEnumerator instance =
                  SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();
                Console.WriteLine(table.Rows[0].ItemArray[0]);
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.ToString(), "Error Occured");
            }

        }
    }
}
