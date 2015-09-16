using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Emp _vm = new Emp();
        #endregion
        public DeleteCommand(Emp vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmployee vmd = (VMEmployee)parameter;
            Emp selectedEmp = context.Emps.FirstOrDefault(aa => aa.EmpID == vmd.selectedEmp.EmpID);
            context.Emps.Remove(selectedEmp);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfEmps.Remove(vmd.selectedEmp);
                    vmd.selectedEmp = vmd.listOfEmps[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
