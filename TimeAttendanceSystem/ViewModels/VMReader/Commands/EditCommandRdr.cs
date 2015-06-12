using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class EditCommandRdr : ICommand
    {
         #region Fields
        VMReader _vmrdr;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandRdr(VMReader vm)
        { _vmrdr = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmrdr.selectedRdr != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
