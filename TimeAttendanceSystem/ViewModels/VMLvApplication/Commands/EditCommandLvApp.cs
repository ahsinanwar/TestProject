using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvApplication.Commands
{
    class EditCommandLvApp : ICommand
    {
        #region Fields
        VMLvApplication _vmlvapp;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandLvApp(VMLvApplication vm)
        { _vmlvapp = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmlvapp.selectedEmpAndLvApp != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvApplication vmd = (VMLvApplication)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
