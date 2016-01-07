using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMHoliday.Commands
{
    class SaveCommandHol: ICommand
    {
         #region Fields
        VMHoliday _vmemp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandHol(VMHoliday vm)
        { _vmemp = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmemp.selectedHoliday != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            if (vmd.isAdding)
            {
                if (vmd._selectedHoliday.HolDesc == "" || vmd._selectedHoliday.HolDesc == null)
                {
                    PopUp.popUp("Holiday", "Please write Holiday Name before saving", NotificationType.Warning);
                }
                else
                {
                    if (context.Holidays.Where(aa => aa.HolDesc == vmd.selectedHoliday.HolDesc).Count() > 0)
                    {
                        PopUp.popUp("Holiday", "Holiday Name must be Unique", NotificationType.Warning);
                    }
                    else
                    {
                        context.Holidays.Add(vmd.selectedHoliday);
                        context.SaveChanges();
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Holiday, (byte)MyEnums.Operation.Add, DateTime.Now);
                        vmd.listOfHolidays.Add(vmd.selectedHoliday);
                        PopUp.popUp("Holiday", "Holiday Created", NotificationType.Information);
                  
                    }
                }
            }
            //else
            //{
            //    if (vmd.selectedHoliday.HolDate.
            //    {
            //        PopUp.popUp("Empty Value", "Please write a Section Name before saving", NotificationType.Warning);
            //    }
                    else
                    {
                        Holiday hol = context.Holidays.First(aa => aa.HolID == vmd.selectedHoliday.HolID);
                        hol.HolDateFrom = vmd.selectedHoliday.HolDateFrom;
                        hol.HolDateTo = vmd.selectedHoliday.HolDateTo;
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        context.SaveChanges();
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Holiday, (byte)MyEnums.Operation.Edit, DateTime.Now);
                        PopUp.popUp("Holiday", "Holiday Edited", NotificationType.Information);
                    }
                

        }
        #endregion
    }
}
