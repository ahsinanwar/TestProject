using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDesignation.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Designation _vm = new Designation();
        #endregion
        public DeleteCommand(Designation vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDesignation vmd = (VMDesignation)parameter;
            Designation selectedDesg = context.Designations.FirstOrDefault(aa => aa.DesignationID == vmd.selectedDesg.DesignationID);
            context.Designations.Remove(selectedDesg);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfDesgs.Remove(vmd.selectedDesg);
                    vmd.selectedDesg = vmd.listOfDesgs[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
