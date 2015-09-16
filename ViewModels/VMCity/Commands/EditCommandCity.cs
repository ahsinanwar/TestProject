using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCity.Commands
{
    class EditCommandCity : ICommand
    {
        #region Fields
        VMCity _vmcity;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandCity(VMCity vm)
        { _vmcity = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcity.selectedCity != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCity vmd = (VMCity)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
