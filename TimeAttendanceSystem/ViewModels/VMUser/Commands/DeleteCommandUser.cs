﻿using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.HelperClasses;

namespace TimeAttendanceSystem.ViewModels.VMUser.Commands
{
    class DeleteCommandUser: ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        User _vm = new User();
        #endregion
        public DeleteCommandUser(User vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMUser vmd = (VMUser)parameter;
            User selectedUser = context.Users.FirstOrDefault(aa => aa.UserID == vmd.selectedUser.UserID);
            PopUp.popUp("User", "Successfully Deleted " + selectedUser.UserName, NotificationType.Information);  
            context.Users.Remove(selectedUser);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    int _userID = GlobalClasses.Global.user.UserID;
                    HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.User, (byte)MyEnums.Operation.Delete, DateTime.Now);
                    vmd.listOfUsers.Remove(vmd.selectedUser);
                    
                    vmd.selectedUser = vmd.listOfUsers[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
