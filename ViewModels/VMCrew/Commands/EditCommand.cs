using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class EditCommand : ICommand
    {
        #region Fields
        VMCrew _vmcrew;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommand(VMCrew vm)
        { _vmcrew = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcrew.selectedCrew != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
