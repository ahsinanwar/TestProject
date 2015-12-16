﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMHoliday.Commands
{
    class EditCommandHol : ICommand
    {
        #region Fields
        VMHoliday _vmholiday;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public EditCommandHol(VMHoliday vm)
        { _vmholiday = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmholiday.selectedHoliday != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            vmd.isEnabled = true;
            vmd.isAdding = false;

        }
        #endregion
    }
}
