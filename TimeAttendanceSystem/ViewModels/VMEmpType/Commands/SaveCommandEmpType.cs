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
                   PopUp.popUp("Employee Type", "Please write Employee type before saving", NotificationType.Warning);
               }

               else
               {
                   if (context.EmpTypes.Where(aa => aa.TypeName == vmd.selectedEmpType.TypeName).Count() > 0)
                   {
                       PopUp.popUp("Employee Type", "Employee type already been created", NotificationType.Warning);
                   }
                   else
                   {
                       EmpType dummy = vmd.selectedEmpType;
                       Category cat = new Category();
                       cat = vmd.selectedEmpType.Category;
                       dummy.Category = null;
                       context.EmpTypes.Add(dummy);
                       context.SaveChanges();
                       dummy.Category = cat;
                       vmd.listOfEmpTypes.Add(dummy);
                       vmd.isEnabled = false;
                       vmd.isAdding = false;
                       PopUp.popUp("Employee Type", "Employee type Created", NotificationType.Information);
                   }
               }

           }
           else {
               EmpType type= context.EmpTypes.First(aa => aa.TypeID == vmd.selectedEmpType.TypeID);
               type.TypeName = vmd.selectedEmpType.TypeName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
               PopUp.popUp("Employee Type", "Employee type Edited", NotificationType.Information);
                }
         
        }
        #endregion
    }
}
