﻿using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDivision.Commands
{
    class DeleteCommandDiv : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Division _vm = new Division();
        #endregion
        public DeleteCommandDiv(Division vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDivision vmd = (VMDivision)parameter;
            Division selectedDiv = context.Divisions.FirstOrDefault(aa => aa.DivisionID == vmd.selectedDiv.DivisionID);
            if (context.Emps.Where(aa => aa.Section.Department.DivID == selectedDiv.DivisionID).Count() > 0)
            {
                PopUp.popUp("Division", "Division " + vmd.selectedDiv.DivisionName + " has employees in it. Please delete them first.", NotificationType.Warning);
            }
            else
            {
                context.Divisions.Remove(selectedDiv);
                //vmd.isAdding = true;
                //vmd.isEnabled = true;
                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Division, (byte)MyEnums.Operation.Delete, DateTime.Now);
                        PopUp.popUp("Division","Division "+ vmd.selectedDiv.DivisionName + " has been removed", NotificationType.Information);
                        vmd.listOfDivs.Remove(vmd.selectedDiv);
                        vmd.selectedDiv = vmd.listOfDivs[0];
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception While Deleting...");
                }
            }
     
        }
    }
}
