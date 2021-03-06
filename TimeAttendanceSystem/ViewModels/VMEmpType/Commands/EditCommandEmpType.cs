﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmpType.Commands
{
    class EditCommandEmpType : ICommand
    {
         #region Fields
        VMEmpType _vm;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandEmpType(VMEmpType vm)
        { _vm = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vm.selectedEmpType != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmpType vmd = (VMEmpType)parameter;
            

            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
