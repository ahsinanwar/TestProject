using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Controllers;
using TimeAttendanceSystem.HelperClasses;
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
        LvController lvController = new LvController();
        public void Execute(object parameter)
        {
            VMShortLeave vmd = (VMShortLeave)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedEmpAndShortLv.Lvshort.SHour == null)
                    vmd.selectedEmpAndShortLv.Lvshort.SHour = new TimeSpan(0, 12, 0, 0);
                if (vmd.selectedEmpAndShortLv.Lvshort.EHour == null)
                    vmd.selectedEmpAndShortLv.Lvshort.EHour = new TimeSpan(0, 12, 0, 0);


                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.EHour - vmd.selectedEmpAndShortLv.Lvshort.SHour;
                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.THour.Value.Duration();
                vmd.selectedEmpAndShortLv.Lvshort.EmpID = vmd.selectedEmpAndShortLv.Employee.EmpID;
                vmd.selectedEmpAndShortLv.Lvshort.EmpDate = vmd.selectedEmpAndShortLv.Employee.EmpID.ToString() + vmd.selectedEmpAndShortLv.Lvshort.DutyDate.Value.ToString("yyMMdd");
                //vmd.selectedEmpAndShortLv.Lvshort.EmpID = vmd.selectedEmpAndShortLv
                vmd.selectedEmpAndShortLv.Lvshort.CreatedDate = DateTime.Today;
                if (!lvController.ValidateShortLeave(vmd.selectedEmpAndShortLv.Lvshort))
                {
                    context.LvShorts.Add(vmd.selectedEmpAndShortLv.Lvshort);
                    if (context.SaveChanges() > 0)
                    {
                        vmd.listOfEmpsAndShortLv.Add(new CombinedEmpAndShortLvcs(vmd.selectedEmpAndShortLv.Employee, vmd.selectedEmpAndShortLv.Lvshort));
                        lvController.AddShortLeaveToAttData(vmd.selectedEmpAndShortLv.Lvshort);
                    }
                }
                else
                    PopUp.popUp("Duplicate", "This Employee already has Short Leave of this date", NotificationType.Warning);
            }
            else
            {
                LvShort lvshort = context.LvShorts.First(aa => aa.EmpID == vmd.selectedEmpAndShortLv.Employee.EmpID);
                lvshort.CreatedBy = vmd.selectedEmpAndShortLv.Lvshort.CreatedBy;
                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.EHour - vmd.selectedEmpAndShortLv.Lvshort.SHour;
                vmd.selectedEmpAndShortLv.Lvshort.THour = vmd.selectedEmpAndShortLv.Lvshort.THour.Value.Duration();
                vmd.selectedEmpAndShortLv.Lvshort.EmpID = vmd.selectedEmpAndShortLv.Employee.EmpID;
                vmd.selectedEmpAndShortLv.Lvshort.EmpDate = vmd.selectedEmpAndShortLv.Employee.EmpID.ToString() + vmd.selectedEmpAndShortLv.Lvshort.DutyDate.Value.ToString("yyMMdd");
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }
        }
        #endregion
    }
}
