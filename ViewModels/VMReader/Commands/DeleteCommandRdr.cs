using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class DeleteCommandRdr : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Reader _vm = new Reader();
        #endregion
        public DeleteCommandRdr(Reader vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            Reader selectedRdr = context.Readers.FirstOrDefault(aa => aa.RdrID == vmd.selectedRdr.RdrID);
            context.Readers.Remove(selectedRdr);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfRdrs.Remove(vmd.selectedRdr);
                    vmd.selectedRdr = vmd.listOfRdrs[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
