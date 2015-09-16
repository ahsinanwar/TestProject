using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;
namespace TimeAttendanceSystem.ViewModels.VMDepartment.Commands
{
    class SaveCommand :ICommand
    {
        #region Fields
        VMDepartments _vmdepartment;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMDepartments vm)
        { _vmdepartment = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdepartment.selectedDept != null);
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
           VMDepartments vmd= (VMDepartments)parameter;
           if (vmd.isAdding)
           {
               context.Departments.Add(vmd.selectedDept);
               context.SaveChanges();
               vmd.listOfDepts.Add(vmd.selectedDept);

           }
           else {
               Department dept = context.Departments.First(aa => aa.DeptID == vmd.selectedDept.DeptID);
               dept.DeptName = vmd.selectedDept.DeptName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
                }
         
        }
        #endregion
    }
}
