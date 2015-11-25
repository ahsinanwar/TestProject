using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMShift.Commands;

namespace TimeAttendanceSystem.ViewModels.VMShift
{
    class SaveCommandShift : ICommand
    {
        #region Fields
        VMShift _vmshift;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandShift(VMShift vm)
        { _vmshift = vm; }
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
            VMShift vmd = (VMShift)parameter;
            if (vmd.isAdding)
            {
                vmd.selectedShift.RosterType = vmd.selectedShift.RosterType1.ID;
                vmd.selectedShift.RosterType1 = null;
                context.Shifts.Add(vmd.selectedShift);
                context.SaveChanges();
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Shift, (byte)MyEnums.Operation.Add, DateTime.Now);
                vmd.listOfShifts.Add(vmd.selectedShift);
                PopUp.popUp("Shift", vmd.selectedShift.ShiftName + " is Created", NotificationType.Information);

            }
            else
            {
                Shift shift = context.Shifts.First(aa => aa.ShiftID == vmd.selectedShift.ShiftID);
                context.Entry(shift).CurrentValues.SetValues(vmd.selectedShift);
                PopUp.popUp("Shift", vmd.selectedShift.ShiftName+" is Modified", NotificationType.Information);
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Shift, (byte)MyEnums.Operation.Edit, DateTime.Now);
            }

        }
        #endregion
    }
}
