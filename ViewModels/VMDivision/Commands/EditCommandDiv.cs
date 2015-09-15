using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDivision.Commands
{
    class EditCommandDiv : ICommand
    {
        #region Fields
        VMDivision _vmdivision;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandDiv(VMDivision vm)
        { _vmdivision = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdivision.selectedDiv != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDivision vmd = (VMDivision)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
