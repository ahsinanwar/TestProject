using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLogin.Commands
{
    class LoginCommand : ICommand
    {
          #region Fields
        TAS2013Entities context = new TAS2013Entities();
     
        #endregion

        #region constructors
        public LoginCommand()
        {  }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            UserCredentials uc = (UserCredentials)parameter;
            User g = context.Users.Where(aa => aa.UserName == uc.Username && aa.Password == uc.Password).FirstOrDefault();
            if (g != null)
            {
                GlobalClasses.Global.user = g;
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.LogIn, (byte)MyEnums.Operation.LogIn, DateTime.Now);
               
                MainWindow mw = new MainWindow();
             //   mw.CommenceTripleChecking();
                
                mw.Show();
               
                Application.Current.Shutdown();
               
                
            }
            else 
            {
                //Application.Current.MainWindow.Close();

            }
        }
        #endregion
    }
}
