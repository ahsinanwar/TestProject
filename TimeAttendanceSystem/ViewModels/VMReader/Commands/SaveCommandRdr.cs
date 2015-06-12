using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class SaveCommandRdr : ICommand
    {
        #region Fields
        VMReader _vmreader;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandRdr(VMReader vm)
        { _vmreader = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmreader.selectedRdr != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            if (vmd.isAdding)
            {
                context.Readers.Add(vmd.selectedRdr);
                context.SaveChanges();
                vmd.listOfRdrs.Add(vmd.selectedRdr);

            }
            else
            {
                Reader rdr = context.Readers.First(aa => aa.RdrID == vmd.selectedRdr.RdrID);
                rdr.RdrName = vmd.selectedRdr.RdrName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
