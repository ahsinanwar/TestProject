﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShift.Commands
{
    class EditCommandShift : ICommand
    {
        #region Fields
        VMShift _vmshift;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandShift(VMShift vm)
        { _vmshift = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmshift.selectedShift != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMShift vmd = (VMShift)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
