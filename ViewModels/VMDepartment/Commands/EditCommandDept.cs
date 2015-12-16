using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDepartment.Commands
{
    class EditCommandDept : ICommand
    {
        #region Fields
        VMDepartments _vmdepartment;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandDept(VMDepartments vm)
        { _vmdepartment = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdepartment.selectedDept != null);
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {  
           VMDepartments vmd= (VMDepartments)parameter;
           vmd.isEnabled = true;
           vmd.isAdding = false;
          
           }
        #endregion
    }
}
