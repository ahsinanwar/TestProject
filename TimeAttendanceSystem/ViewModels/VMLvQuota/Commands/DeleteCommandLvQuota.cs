using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvQuota.Commands
{
    class DeleteCommandLvQuota : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        LvQuota _vm = new LvQuota();
        #endregion
        public DeleteCommandLvQuota(LvQuota vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvQuota vmd = (VMLvQuota)parameter;
            LvQuota selectedlvQuota = context.LvQuotas.FirstOrDefault(aa => aa.EmpID == vmd.selectedLvQuota.EmpID);
            context.LvQuotas.Remove(selectedlvQuota);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfLvQuotas.Remove(vmd.selectedLvQuota);
                    vmd.selectedLvQuota = vmd.listOfLvQuotas[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
