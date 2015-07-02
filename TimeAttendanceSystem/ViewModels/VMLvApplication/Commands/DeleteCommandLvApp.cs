using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Controllers;
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
            LvController lvctrl = new LvController();
            if ((bool)selectedLvApp.IsHalf)
            {
                lvctrl.DeleteHLFromAttData(selectedLvApp);
                lvctrl.DeleteHLFromLVData(selectedLvApp);
                lvctrl.UpdateHLeaveBalance(selectedLvApp);
            }
            else {
            
                lvctrl.DeleteFromLVData(selectedLvApp);
                lvctrl.DeleteHLFromAttData(selectedLvApp);
                lvctrl.UpdateLeaveBalance(selectedLvApp);
            
            
            }
            
           context.LvApplications.Remove(selectedLvApp);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                   vmd.listOfEmpsAndLvApps.Remove(vmd.selectedEmpAndLvApp);
                    vmd.selectedEmpAndLvApp = vmd.listOfEmpsAndLvApps[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
