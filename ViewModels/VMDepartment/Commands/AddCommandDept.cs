using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;
namespace TimeAttendanceSystem.ViewModels.VMDepartment.Commands
{
    class AddCommandDept : ICommand
    {
        #region Fields
        TAS2013Entities context=new TAS2013Entities();
        Department _vm = new Department();
        #endregion

        #region constructors
        public AddCommandDept(Department vm)
        { _vm = vm; }
        public bool CanExecute(object parameter)
        {
                   return true;
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
           VMDepartments vmd= (VMDepartments)parameter;
           vmd._selectedDept = new Department();
           vmd._selectedDept.Division = vmd.listOfDivs.FirstOrDefault();
           vmd._selectedDept.DivID = vmd.selectedDept.Division.DivisionID;
           vmd.selectedDept = vmd._selectedDept;
           vmd.isAdding = true;
           vmd.isEnabled = true;
        //   context.SaveChanges();
           //Console.WriteLine(vmd.DeptName);
          // Console.WriteLine(vmd.DivID);
          // Console.WriteLine(vmd.DeptID);
          // Console.WriteLine(vmd.CompanyID);
        }
        #endregion
    }
}
