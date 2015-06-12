using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class SaveCommand : ICommand
    {
        #region Fields
        VMCrew _vmcrew;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMCrew vm)
        { _vmcrew = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcrew.selectedCrew != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            if (vmd.isAdding)
            {
                context.Crews.Add(vmd.selectedCrew);
                context.SaveChanges();
                vmd.listOfcrews.Add(vmd.selectedCrew);

            }
            else
            {
                Crew crew = context.Crews.First(aa => aa.CrewID == vmd.selectedCrew.CrewID);
                crew.CrewName = vmd.selectedCrew.CrewName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
