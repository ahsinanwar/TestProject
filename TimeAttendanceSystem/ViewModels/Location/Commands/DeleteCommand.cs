using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Location _vm = new Location();
        #endregion
        public DeleteCommand(Location vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLoction vmd = (VMLoction)parameter;
            Location selectedLoc = context.Locations.FirstOrDefault(aa => aa.LocID == vmd.selectedLoc.LocID);
            context.Locations.Remove(selectedLoc);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfDepts.Remove(vmd.selectedDept);
                    vmd.selectedDept = vmd.listOfDepts[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
