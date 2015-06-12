using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class EditCommandEmp : ICommand
    {
        #region Fields
        VMEmployee _vmemps;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandEmp(VMEmployee vm)
        { _vmemps = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmemps.selectedEmp != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmployee vmd = (VMEmployee)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
