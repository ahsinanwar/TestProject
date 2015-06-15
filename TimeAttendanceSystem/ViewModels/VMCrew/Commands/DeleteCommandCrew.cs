using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class DeleteCommandCrew : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Crew _vm = new Crew();
        #endregion
        public DeleteCommandCrew(Crew vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            Crew selectedCrew = context.Crews.FirstOrDefault(aa => aa.CrewID == vmd.selectedCrew.CrewID);
            context.Crews.Remove(selectedCrew);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfCrews.Remove(vmd.selectedCrew);
                    vmd.selectedCrew = vmd.listOfCrews[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
