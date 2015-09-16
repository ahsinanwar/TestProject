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
    class ExitCommand : ICommand
    {
          #region Fields
        TAS2013Entities context = new TAS2013Entities();
     
        #endregion

        #region constructors
        public ExitCommand()
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

            Application.Current.Shutdown();

            
        }
        #endregion
    }
}
