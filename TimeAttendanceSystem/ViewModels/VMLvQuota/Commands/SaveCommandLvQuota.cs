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
            return true;
            
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
                    PopUp.popUp("Leave Quota", "Please select Employee before saving", NotificationType.Warning);
                }
                else
                {
                    if (context.LvQuotas.Where(aa => aa.EmpID == vmd.selectedLvQuota.EmpID).Count() > 0)
                    {
                        PopUp.popUp("Leave Quota", vmd.selectedLvQuota.Emp.EmpName+ " already has a quota set please Edit instead of Adding", NotificationType.Warning);
                    }
                    else
                    {
                        LvQuota transition = new LvQuota();
                        
                       

                        transition = vmd.selectedLvQuota;
                        Emp temp = new Emp();
                        temp = transition.Emp;
                        PopulateLvConsumed(transition);
                        transition.Emp = null;
                        context.LvQuotas.Add(transition);
                        context.SaveChanges();
                        transition.Emp = temp;
                        vmd.listOfLvQuotas.Add(vmd.selectedLvQuota);
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        PopUp.popUp("Leave Quota", "Leave Quota created", NotificationType.Information);
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

        private void PopulateLvConsumed(LvQuota transition)
        {
           
            for (int i = 0; i <= 2 ; i++)
            {
                int caseSwitch = i;
                LvConsumed annualleave = new LvConsumed();
                annualleave.JanConsumed = 0;
                annualleave.FebConsumed = 0;
                annualleave.MarchConsumed = 0;
                annualleave.AprConsumed = 0;
                annualleave.MayConsumed = 0;
                annualleave.JuneConsumed = 0;
                annualleave.JulyConsumed = 0;
                annualleave.AugustConsumed = 0;
                annualleave.SepConsumed = 0;
                annualleave.OctConsumed = 0;
                annualleave.NovConsumed = 0;
                annualleave.DecConsumed = 0;
                switch (caseSwitch)
                {
                    case 0:
                        annualleave.LeaveType = "A";
                        annualleave.EmpLvType = transition.EmpID + "A";
                        annualleave.EmpID = transition.EmpID;
                        annualleave.GrandTotal = transition.A;
                        annualleave.GrandTotalRemaining = transition.A;
                        annualleave.YearRemaining = transition.A;
                        annualleave.TotalForYear = transition.A;
                        
                        break;
                    case 1:
                        annualleave.LeaveType = "B";
                        annualleave.EmpLvType = transition.EmpID + "B";
                        annualleave.EmpID = transition.EmpID;
                        annualleave.GrandTotal = transition.B;
                        annualleave.GrandTotalRemaining = transition.B;
                        annualleave.YearRemaining = transition.B;
                        annualleave.TotalForYear = transition.B;
                        
                        break;
                    case 2:
                        annualleave.LeaveType = "C";
                        annualleave.EmpLvType = transition.EmpID + "C";
                        annualleave.EmpID = transition.EmpID;
                        annualleave.GrandTotal = transition.C;
                        annualleave.GrandTotalRemaining = transition.C;
                        annualleave.YearRemaining = transition.C;
                        annualleave.TotalForYear = transition.C;
                        
                        break;
                }
                context.LvConsumeds.Add(annualleave);
                context.SaveChanges();
            }

        }
        #endregion
    }
}
