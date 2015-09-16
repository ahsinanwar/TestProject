using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShift.Commands
{
    class DeleteCommandShift : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Shift _vm = new Shift();
        #endregion
        public DeleteCommandShift(Shift vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMShift vmd = (VMShift)parameter;
            Shift selectedShift = context.Shifts.FirstOrDefault(aa => aa.ShiftID == vmd.selectedShift.ShiftID);
            context.Shifts.Remove(selectedShift);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfShifts.Remove(vmd.selectedShift);
                    vmd.selectedShift = vmd.listOfShifts[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
