﻿using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class SaveCommandCrew : ICommand
    {
        #region Fields
        VMCrew _vmcrew;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandCrew(VMCrew vm)
        { _vmcrew = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmcrew.selectedCrew != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedCrew.CrewName == "" || vmd.selectedCrew.CrewName == null)
                {
                    PopUp.popUp("Crew", "Please write Crew name before saving", NotificationType.Warning);
                }

                else
                {
                    if (context.Crews.Where(aa => aa.CrewName == vmd.selectedCrew.CrewName).Count() > 0)
                    {
                        PopUp.popUp("Crew", "Crew already been created", NotificationType.Warning);
                    }
                    else
                    {
                        context.Crews.Add(vmd.selectedCrew);
                        context.SaveChanges();
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Crew, (byte)MyEnums.Operation.Add, DateTime.Now);
                        vmd.listOfCrews.Add(vmd.selectedCrew);
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        PopUp.popUp("Crew", "Crew is Created", NotificationType.Information);
                  
                    }
                }

            }
            else
            {
                Crew crew = context.Crews.First(aa => aa.CrewID == vmd.selectedCrew.CrewID);
                crew.CrewName = vmd.selectedCrew.CrewName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Crew, (byte)MyEnums.Operation.Edit, DateTime.Now);
                PopUp.popUp("Crew", "Crew Updated", NotificationType.Information);
                  
            }

        }
        #endregion
    }
}
