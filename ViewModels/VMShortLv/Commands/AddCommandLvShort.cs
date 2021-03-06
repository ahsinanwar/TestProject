﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShortLv.Commands
{
    class AddCommandLvShort : ICommand
    {
         #region Fields
        TAS2013Entities context = new TAS2013Entities();
        CombinedEmpAndShortLvcs _vm = new CombinedEmpAndShortLvcs();
        #endregion

        #region constructors
        public AddCommandLvShort(CombinedEmpAndShortLvcs vm)
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
            VMShortLeave vmd = (VMShortLeave)parameter;
            vmd.selectedEmpAndShortLv = new CombinedEmpAndShortLvcs();
            vmd.isAdding = true;
            vmd.isEnabled = true;
            //   context.SaveChanges();
            //Console.WriteLine(vmd.DeptName);
            // Console.WriteLine(vmd.DivID);
            // Console.WriteLine(vmd.DeptID);
            // Console.WriteLine(vmd.CompanyID);
        }
        #endregion
    }
}
