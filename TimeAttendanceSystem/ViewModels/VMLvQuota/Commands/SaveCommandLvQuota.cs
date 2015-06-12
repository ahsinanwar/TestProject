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
                context.LvQuotas.Add(vmd.selectedLvQuota);
                context.SaveChanges();
                vmd.listOfLvQuotas.Add(vmd.selectedLvQuota);

            }
            else
            {
                LvConsumed lvconsumed = context.LvConsumeds.First(aa => aa.EmpID == vmd.selectedLvQuota.EmpID);
                lvconsumed.Emp.EmpName = vmd.selectedLvQuota.Emp.EmpName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
