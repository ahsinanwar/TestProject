using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCategory.Commands
{
    class EditCommandCat : ICommand
    {
        #region Fields
        VMCategory _vmcategory;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandCat(VMCategory vm)
        { _vmcategory = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcategory.selectedCat != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCategory vmd = (VMCategory)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
