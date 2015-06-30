﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvApplication.Commands
{
    class DeleteCommandLvApp : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        CombinedEmpAndLvApps _vm = new CombinedEmpAndLvApps();
        #endregion
        public DeleteCommandLvApp(CombinedEmpAndLvApps vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvApplication vmd = (VMLvApplication)parameter;
            LvApplication selectedLvApp = context.LvApplications.FirstOrDefault(aa => aa.LvID == vmd.selectedEmpAndLvApp.LvApp.LvID);
            context.LvApplications.Remove(selectedLvApp);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                 //   vmd.listOfLvApps.Remove(vmd.selectedLvApp);
                 //   vmd.selectedLvApp = vmd.listOfLvApps[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
