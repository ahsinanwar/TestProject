using Mantin.Controls.Wpf.Notification;
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
    class SaveCommandDiv : ICommand
    {
        #region Fields
        VMDivision _vmdivision;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandDiv(VMDivision vm)
        { _vmdivision = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmdivision.selectedDiv != null);
            return true;
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
           VMDivision vmd= (VMDivision)parameter;
           if (vmd.isAdding)
           {
               if (vmd.selectedDiv.DivisionName == "" || vmd.selectedDiv.DivisionName == null)
               {
                   PopUp.popUp("Empty Value", "Please write Division Name before saving", NotificationType.Warning);
               }
               else
               {
                   if (context.Divisions.Where(aa => aa.DivisionName == vmd.selectedDiv.DivisionName).Count() > 0)
                   {
                       PopUp.popUp("Sorry!", "Division Name already been done", NotificationType.Warning);
                   }
                   else
                   {
                       context.Divisions.Add(vmd.selectedDiv);
                       context.SaveChanges();
                       vmd.listOfDivs.Add(vmd.selectedDiv);
                       vmd.isEnabled = false;
                       vmd.isAdding = false;
                       PopUp.popUp("Congratulations", "Division Name is Created", NotificationType.Warning);
                   }
               }
           }
           else
           {
               Division div = context.Divisions.First(aa => aa.DivisionID == vmd.selectedDiv.DivisionID);
               div.DivisionName = vmd.selectedDiv.DivisionName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
               PopUp.popUp("Congratulations", "Division Name is Created", NotificationType.Warning);
           }
         
        }
        #endregion
    }
}
