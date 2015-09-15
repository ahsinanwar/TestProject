using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMUser.Commands
{
    class EditCommandUser: ICommand
    {
         #region Fields
        VMUser _vmuser;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandUser(VMUser vm)
        { _vmuser = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmshift.selectedShift != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMUser vmd = (VMUser)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}

