using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShortLv.Commands
{
    class EditCommandLvShort: ICommand
    {
         #region Fields
        VMShortLeave _vmshortlv;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandLvShort(VMShortLeave vm)
        { _vmshortlv = vm; }
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
            VMShortLeave vmd = (VMShortLeave)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}

