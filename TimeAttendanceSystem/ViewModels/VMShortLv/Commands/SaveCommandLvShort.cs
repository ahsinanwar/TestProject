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
                if (vmd.selectedEmpAndShortLv.Lvshort.SHour == null)
                    vmd.selectedEmpAndShortLv.Lvshort.SHour = new TimeSpan(0, 12, 0, 0);

                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.EHour - vmd.selectedEmpAndShortLv.Lvshort.SHour;
                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.THour.Value.Duration();
                context.LvShorts.Add(vmd.selectedEmpAndShortLv.Lvshort);
            context.SaveChanges();
            vmd.listOfEmpsAndShortLv.Add(new CombinedEmpAndShortLvcs(vmd.selectedEmpAndShortLv.Employee, vmd.selectedEmpAndShortLv.Lvshort));
            
            }
            else
           {
               LvShort lvshort = context.LvShorts.First(aa => aa.EmpID == vmd.selectedEmpAndShortLv.Employee.EmpID);
              lvshort.CreatedBy = vmd.selectedEmpAndShortLv.Lvshort.CreatedBy;
             vmd.isEnabled = false;
            vmd.isAdding = false;
            context.SaveChanges();
            }

        }
        #endregion
    }
}
