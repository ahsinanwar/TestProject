using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Section _vm = new Section();
        #endregion
        public DeleteCommand(Section vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            Section selectedSec = context.Sections.FirstOrDefault(aa => aa.SectionID == vmd.selectedSec.SectionID);
            context.Sections.Remove(selectedSec);
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
