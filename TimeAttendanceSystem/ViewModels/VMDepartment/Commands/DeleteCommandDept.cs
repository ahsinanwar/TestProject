using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDepartment.Commands
{
    class DeleteCommandDept : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Department _vm = new Department();
        #endregion
        public DeleteCommandDept(Department vm)
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
            if (context.Emps.Where(aa => aa.Section.DeptID == selectedDept.DeptID).Count() > 0)
            {
                PopUp.popUp("Department", "Department has Employees in it. Please remove them.", NotificationType.Warning);
            }
            else
            {
                context.Departments.Remove(selectedDept);
                vmd.isAdding = true;
                vmd.isEnabled = true;
                try
                {
                    if (context.SaveChanges() > 0)
                    {

                        PopUp.popUp("Department", vmd.selectedDept.DeptName + " has been removed", NotificationType.Warning);
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
}
