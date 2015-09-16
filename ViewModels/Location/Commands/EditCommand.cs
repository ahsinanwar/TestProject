using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class EditCommand : ICommand
    {
        #region Fields
        VMLocation _vmlocation;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommand(VMLocation vm)
        { _vmlocation = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmlocation.selectedLoc != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLocation vmd = (VMLocation)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
