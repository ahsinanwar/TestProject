using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShortLv.Commands
{
    class DeleteCommandLvShort: ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        CombinedEmpAndShortLvcs _vm = new CombinedEmpAndShortLvcs();
        #endregion
        public DeleteCommandLvShort(CombinedEmpAndShortLvcs vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
          //  VMShortLeave vmd = (VMShortLeave)parameter;
            //LvShort selectedShortLv = context.LvShorts.FirstOrDefault(aa => aa.EmpID == vmd.selectedShortLv.EmpID);
       //     context.LvShorts.Remove(selectedShortLv);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
           // try
           // {
            //    if (context.SaveChanges() > 0)
            //    {
             //       vmd.listOfShortLvs.Remove(vmd.selectedShortLv);
             //       vmd.selectedShortLv = vmd.listOfShortLvs[0];
             //   }
          //  }
          //  catch (Exception)
          //  {
          //      Console.WriteLine("Exception While Deleting...");
           // }
        }
    }
}
