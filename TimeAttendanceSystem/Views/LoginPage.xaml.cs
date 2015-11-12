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
using TimeAttendanceSystem.ViewModels.VMLogin;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            try
            {
                InitializeComponent();
                VMLogin login = new VMLogin();
                this.DataContext = login;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            new MainWindow().ShowDialog();
            
        }

        private void RadButton_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
