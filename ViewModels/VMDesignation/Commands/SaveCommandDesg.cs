using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDesignation.Commands
{
    class SaveCommandDesg : ICommand
    {
        #region Fields
        VMDesignation _vmdesignation;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandDesg(VMDesignation vm)
        { _vmdesignation = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmdesignation.selectedDesg != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDesignation vmd = (VMDesignation)parameter;
            if (vmd.isAdding)
            {
                    if (vmd.selectedDesg.DesignationName == "" || vmd.selectedDesg.DesignationName == null)
                    {
                        PopUp.popUp("Empty Value", "Please write Designation Name before saving", NotificationType.Warning);
                    }
                     else
                    {
                    if (context.Designations.Where(aa => aa.DesignationName == vmd.selectedDesg.DesignationName).Count() > 0)
                    {
                        PopUp.popUp("Duplicate", "Designation name already been done", NotificationType.Warning);
                    }
                    else
                    {
                context.Designations.Add(vmd.selectedDesg);
                context.SaveChanges();
                vmd.listOfDesgs.Add(vmd.selectedDesg);
                vmd.isEnabled = false;
                vmd.isAdding = false;
                PopUp.popUp("Congratulations", "Designation Name is Created", NotificationType.Warning);
                    }
             }
          }
            else
            {
                Designation desg = context.Designations.First(aa => aa.DesignationID == vmd.selectedDesg.DesignationID);
                desg.DesignationName = vmd.selectedDesg.DesignationName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                PopUp.popUp("Created Successfully", "Designation Name is Created", NotificationType.Warning);
            }

        }
        #endregion
    }
 }
