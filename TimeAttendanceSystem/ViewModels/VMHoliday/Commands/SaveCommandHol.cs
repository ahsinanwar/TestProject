using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMHoliday.Commands
{
    class SaveCommandHol: ICommand
    {
         #region Fields
        VMHoliday _vmemp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandHol(VMHoliday vm)
        { _vmemp = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmemp.selectedHoliday != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            if (vmd.isAdding)
            {
                context.Holidays.Add(vmd.selectedHoliday);
                context.SaveChanges();
                vmd.listOfHolidays.Add(vmd.selectedHoliday);

            }
            else
            {
                Holiday hol = context.Holidays.First(aa => aa.HolID == vmd.selectedHoliday.HolID);
                hol.HolDate = vmd.selectedHoliday.HolDate;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
