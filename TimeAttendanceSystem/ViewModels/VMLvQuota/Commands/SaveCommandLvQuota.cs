using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvQuota.Commands
{
    class SaveCommandLvQuota : ICommand
    {
         #region Fields
        VMLvQuota _vmlvquota;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandLvQuota(VMLvQuota vm)
        { _vmlvquota = vm; }
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


            //LvConsumed lvquota = new LvConsumed();
            //lvquota = context.LvQuotas.FirstOrDefault(aa => aa.LvID == vmd.selectedEmpAndLvApp.LvApp.LvID);
            //lvquota = vmd.selectedEmpAndLvApp.LvApp;
            //    vmd.isEnabled = false;
            //    vmd.isAdding = false;
            //    context.SaveChanges();
            //    PopUp.popUp("Application", "Application has been successfully edited for " + vmd.selectedEmpAndLvApp.Employee.EmpName, NotificationType.Warning);
                            
            
        }
        #endregion
    }
}
