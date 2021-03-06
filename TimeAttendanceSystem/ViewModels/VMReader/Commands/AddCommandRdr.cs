﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class AddCommandRdr : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Reader _vm = new Reader();
        #endregion

        #region constructors
        public AddCommandRdr(Reader vm)
        { _vm = vm; }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            vmd.selectedRdr = new Reader() { 
                IpPort = (short)4370,
                ReaderType = context.ReaderTypes.FirstOrDefault(),
                Location = context.Locations.FirstOrDefault(),
                RdrDutyCode = context.RdrDutyCodes.FirstOrDefault(),
                Status = true
            };
            vmd.isAdding = true;
            vmd.isEnabled = true;
        }
        #endregion
    }
}
