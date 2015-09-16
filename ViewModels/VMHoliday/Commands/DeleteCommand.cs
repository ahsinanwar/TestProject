using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMHoliday.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Holiday _vm = new Holiday();
        #endregion
        public DeleteCommand(Holiday vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            Holiday selectedHoliday = context.Holidays.FirstOrDefault(aa => aa.HolID == vmd.selectedHoliday.HolID);
            context.Holidays.Remove(selectedHoliday);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfHolidays.Remove(vmd.selectedHoliday);
                    vmd.selectedHoliday = vmd.listOfHolidays[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
