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
using TimeAttendanceSystem.Controllers;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;
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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            TAS2013Entities context = new TAS2013Entities();
            string username = userNameBox.Text;
            string password = PasswordBox.Password;
            User g = context.Users.Where(aa => aa.UserName == username && aa.Password == password).FirstOrDefault();
            if (g != null)
            {
                GlobalClasses.Global.user = g;
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.LogIn, (byte)MyEnums.Operation.LogIn, DateTime.Now);

                MainWindow mw = new MainWindow();
                //   mw.CommenceTripleChecking();

                mw.Show();
                context.Dispose();
                this.Close();

                //


            }
            else
            {
                //Application.Current.MainWindow.Close();

            }
            Console.WriteLine(username);
            Console.WriteLine(password);
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
