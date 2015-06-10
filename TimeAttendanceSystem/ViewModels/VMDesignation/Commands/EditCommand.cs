using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDesignation.Commands
{
    class EditCommand : ICommand
    {
        #region Fields
        VMDesignation _vmdesignation;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommand(VMDesignation vm)
        { _vmdesignation = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdesignation.selectedDesg != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDesignation vmd = (VMDesignation)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
