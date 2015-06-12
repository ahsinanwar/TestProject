using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class EditCommandLoc : ICommand
    {
         #region Fields
        VMLocation _vmloc;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandLoc(VMLocation vm)
        { _vmloc = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmloc.selectedLoc != null);
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
