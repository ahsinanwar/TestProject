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
    class SaveCommandLvApp : ICommand
    {
        #region Fields
        VMLvApplication _vmlvapp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandLvApp(VMLvApplication vm)
        { _vmlvapp = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmlvapp.selectedLvApp != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvApplication vmd = (VMLvApplication)parameter;
            if (vmd.isAdding)
            {   

                LvController lvctrl = new LvController();
                // IF Leave is half Leave
                if (vmd.selectedEmpAndLvApp.LvApp.IsHalf == true)
                {

                }
                else
                {
                    bool chkdup = lvctrl.CheckDuplicateLeave(vmd.selectedEmpAndLvApp.LvApp);
                    if (chkdup == false)
                    {
                        bool chkbal = lvctrl.CheckLeaveBalance(vmd.selectedEmpAndLvApp.LvApp);
                        if (chkbal == true)
                        {
                            vmd.selectedEmpAndLvApp.LvApp.NoOfDays = (vmd.selectedEmpAndLvApp.LvApp.ToDate - vmd.selectedEmpAndLvApp.LvApp.FromDate).Days+1;
                            context.LvApplications.Add(vmd.selectedEmpAndLvApp.LvApp);
                            context.SaveChanges();
                            lvctrl.AddLeaveToLeaveAttData(vmd.selectedEmpAndLvApp.LvApp);
                            lvctrl.AddLeaveToLeaveData(vmd.selectedEmpAndLvApp.LvApp);
                            lvctrl.BalanceLeaves(vmd.selectedEmpAndLvApp.LvApp);
                            vmd.listOfEmpsAndLvApps.Add(vmd.selectedEmpAndLvApp);
                        }
                    }
                }
                    
                

            }
            else
            {
                LvApplication lvapp = context.LvApplications.FirstOrDefault(aa => aa.LvType == vmd.selectedEmpAndLvApp.LvApp.LvType);
                lvapp.LvType = vmd.selectedEmpAndLvApp.LvType;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
