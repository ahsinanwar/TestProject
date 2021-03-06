﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmpType.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        EmpType _vm = new EmpType();
        #endregion
        public DeleteCommand(EmpType vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmpType vmd = (VMEmpType)parameter;
            EmpType selectedEmpType = context.EmpTypes.FirstOrDefault(aa => aa.TypeID== vmd.selectedEmpType.TypeID);
            context.EmpTypes.Remove(selectedEmpType);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfDepts.Remove(vmd.selectedDept);
                    vmd.selectedDept = vmd.listOfDepts[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
