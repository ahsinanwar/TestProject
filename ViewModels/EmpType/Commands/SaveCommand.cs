using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmpType.Commands
{
    class SaveCommand :ICommand
    {
         #region Fields
        VMEmpType _vmemptype;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMEmpType vm)
        { _vmemptype = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmemptype.selectedEmpType != null);
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
           VMEmpType vmd= (VMEmpType)parameter;
           if (vmd.isAdding)
           {
               context.EmpTypes.Add(vmd.selectedEmpType);
               context.SaveChanges();
               vmd.listOfEmpTypes.Add(vmd.selectedEmpType);

           }
           else {
               EmpType type= context.EmpTypes.First(aa => aa.TypeID == vmd.selectedEmpType.TypeID);
               type.TypeName = vmd.selectedEmpType.TypeName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
                }
         
        }
        #endregion
    }
}
