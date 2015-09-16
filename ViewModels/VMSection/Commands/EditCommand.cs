using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection.Commands
{
    class EditCommand : ICommand
    {
        #region Fields
        VMSection _vmsection;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommand(VMSection vm)
        { _vmsection = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmsection.selectedSec != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
