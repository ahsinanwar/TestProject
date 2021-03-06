﻿using Mantin.Controls.Wpf.Notification;
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
    class DeleteCommandHol : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Holiday _vm = new Holiday();
        #endregion
        public DeleteCommandHol(Holiday vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            Holiday selectedHoliday = context.Holidays.FirstOrDefault(aa => aa.HolID == vmd.selectedHoliday.HolID);
            context.Holidays.Remove(selectedHoliday);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    int _userID = GlobalClasses.Global.user.UserID;
                    HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Holiday, (byte)MyEnums.Operation.Delete, DateTime.Now);
                    vmd.listOfHolidays.Remove(vmd.selectedHoliday);
                    vmd.selectedHoliday = vmd.listOfHolidays[0];
                    PopUp.popUp("Holiday", "Holiday Deleted", NotificationType.Information);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
