using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShortLv.Commands
{
    class SaveCommandLvShort: ICommand
    {
        #region Fields
        VMShortLeave _vmshortlv;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandLvShort(VMShortLeave vm)
        { _vmshortlv = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmshift.selectedShift != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMShortLeave vmd = (VMShortLeave)parameter;
            if (vmd.isAdding)
            {
                context.LvShorts.Add(vmd.selectedShortLv);
                context.SaveChanges();
                vmd.listOfShortLvs.Add(vmd.selectedShortLv);

            }
            else
            {
                LvShort lvshort = context.LvShorts.First(aa => aa.EmpID == vmd.selectedShortLv.EmpID);
                lvshort.CreatedBy = vmd.selectedShortLv.CreatedBy;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
