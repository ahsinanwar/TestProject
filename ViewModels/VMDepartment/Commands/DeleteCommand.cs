using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDepartment.Commands
{
    class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Department _vm = new Department();
        #endregion
        public DeleteCommand(Department vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDepartments vmd = (VMDepartments)parameter;
            Department selectedDept = context.Departments.FirstOrDefault(aa => aa.DeptID == vmd.selectedDept.DeptID);
            context.Departments.Remove(selectedDept);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfDepts.Remove(vmd.selectedDept);
                    vmd.selectedDept = vmd.listOfDepts[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
