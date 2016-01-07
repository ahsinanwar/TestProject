using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShift
{
    class SaveCommandShift : ICommand
    {
        #region Fields
        VMShift _vmshift;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandShift(VMShift vm)
        { _vmshift = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmshift.selectedShift != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMShift vmd = (VMShift)parameter;
            if (vmd.isAdding)
            {
                context.Shifts.Add(vmd.selectedShift);
                context.SaveChanges();
                vmd.listOfShifts.Add(vmd.selectedShift);

            }
            else
            {
                Shift shift = context.Shifts.First(aa => aa.ShiftID == vmd.selectedShift.ShiftID);
                shift.ShiftName = vmd.selectedShift.ShiftName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
