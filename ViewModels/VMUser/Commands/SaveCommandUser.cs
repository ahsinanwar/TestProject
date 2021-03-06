﻿using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMUser.Commands
{
    class SaveCommandUser: ICommand
    {
        #region Fields
        
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandUser()
        {  }
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
            VMUser vmd = (VMUser)parameter;
           int getUserRoleID=-2;
            String b = vmd.selectedUserRole;
            if (b == null)
            { PopUp.popUp("User Role", "Please select a Role", NotificationType.Warning); }
            else
           getUserRoleID = context.UserRoles.Where(aa => aa.RoleName == b).FirstOrDefault().RoleID;
            if (vmd.isAdding)
            {
                if ( vmd.selectedUser.EmpID == null)
                {
                    PopUp.popUp("Give a Value", "Please write User ID before saving", NotificationType.Warning);   
                }
                else
                {
                    vmd.selectedUser.RoleID = (Byte)getUserRoleID;
                    using (TAS2013Entities ctx = new TAS2013Entities())
                    {
                        vmd.selectedUser.Emp = null;
                        ctx.Users.Add(vmd.selectedUser);
                        ctx.SaveChanges();
                    }
                   
                    vmd.listOfUsers.Add(vmd.selectedUser);
                }
            }
            else
            {
                User user = context.Users.First(aa => aa.UserID == vmd.selectedUser.UserID);
                user.UserName = vmd.selectedUser.UserName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
