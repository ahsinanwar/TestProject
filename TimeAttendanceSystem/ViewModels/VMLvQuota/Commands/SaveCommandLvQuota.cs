using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
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
            return (_vmlvquota.selectedLvQuota != null);
            
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {


            VMLvQuota vmd = (VMLvQuota)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedLvQuota.EmpID == null)
                {
                    PopUp.popUp("Empty Value", "Please select Emp before saving", NotificationType.Warning);
                }
                else
                {
                    if (context.LvQuotas.Where(aa => aa.EmpID == vmd.selectedLvQuota.EmpID).Count() > 0)
                    {
                        PopUp.popUp("Duplicate", "This Employee have already been a quota", NotificationType.Warning);
                    }
                    else
                    {
                        context.LvQuotas.Add(vmd.selectedLvQuota);
                        context.SaveChanges();
                        vmd.listOfLvQuotas.Add(vmd.selectedLvQuota);
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        PopUp.popUp("Congratulations", "Lv Quota  is Created", NotificationType.Warning);
                    }
                }
            }
            else
            {
                LvQuota lvquota = context.LvQuotas.First(aa => aa.EmpID == vmd.selectedLvQuota.EmpID);
               
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                PopUp.popUp("Created Successfully", "Lv Quota  is Created congrats g", NotificationType.Warning);
            }
           
            
        }
        #endregion
    }
}
