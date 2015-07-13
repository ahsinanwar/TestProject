using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmpType.Commands
{
    class SaveCommandEmpType :ICommand
    {
         #region Fields
        VMEmpType _vmemptype;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandEmpType(VMEmpType vm)
        { _vmemptype = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmemptype.selectedEmpType != null);
            return true;
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
           VMEmpType vmd= (VMEmpType)parameter;
           if (vmd.isAdding)
           {
               if (vmd.selectedEmpType.TypeName == "" || vmd.selectedEmpType.TypeName == null)
               {
                   PopUp.popUp("Empty Value", "Please write Emp Type before saving", NotificationType.Warning);
               }

               else
               {
                   if (context.EmpTypes.Where(aa => aa.TypeName == vmd.selectedEmpType.TypeName).Count() > 0)
                   {
                       PopUp.popUp("Sorry!", "Emptype already been created", NotificationType.Warning);
                   }
                   else
                   {
                       context.EmpTypes.Add(vmd.selectedEmpType);
                       context.SaveChanges();
                       vmd.listOfEmpTypes.Add(vmd.selectedEmpType);
                       vmd.isEnabled = false;
                       vmd.isAdding = false;
                       PopUp.popUp("Congratulations", "Emptype is Created", NotificationType.Warning);
                   }
               }

           }
           else {
               EmpType type= context.EmpTypes.First(aa => aa.TypeID == vmd.selectedEmpType.TypeID);
               type.TypeName = vmd.selectedEmpType.TypeName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
               PopUp.popUp("Congratulations", "Emptype is Created", NotificationType.Warning);
                }
         
        }
        #endregion
    }
}
