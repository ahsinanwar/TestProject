using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDivision.Commands
{
    class DeleteCommandDiv : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Division _vm = new Division();
        #endregion
        public DeleteCommandDiv(Division vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDivision vmd = (VMDivision)parameter;
            Division selectedDiv = context.Divisions.FirstOrDefault(aa => aa.DivisionID == vmd.selectedDiv.DivisionID);
            context.Divisions.Remove(selectedDiv);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfDivs.Remove(vmd.selectedDiv);
                    vmd.selectedDiv = vmd.listOfDivs[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
