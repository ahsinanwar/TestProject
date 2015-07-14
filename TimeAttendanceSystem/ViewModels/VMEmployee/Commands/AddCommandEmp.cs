using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class AddCommandEmp : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Emp _vm = new Emp();
        #endregion

        #region constructors
        public AddCommandEmp(Emp vm)
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
            VMEmployee vmd = (VMEmployee)parameter;
            vmd._selectedEmp = new Emp();
            vmd._selectedEmp.EmpType = vmd.listOfEmpTypes.FirstOrDefault();
            vmd._selectedEmp.TypeID = vmd.selectedEmp.EmpType.TypeID;
            vmd._selectedEmp.Designation = vmd.listOfDesgs.FirstOrDefault();
            vmd._selectedEmp.DesigID = vmd.selectedEmp.Designation.DesignationID;
            vmd._selectedEmp.Grade = vmd.listOfGrades.FirstOrDefault();
            vmd._selectedEmp.GradeID = vmd.selectedEmp.Grade.GradeID;
            vmd._selectedEmp.Shift = vmd.listOfShifts.FirstOrDefault();
            vmd._selectedEmp.ShiftID = vmd.selectedEmp.Shift.ShiftID;
            vmd._selectedEmp.Location = vmd.listOfLocs.FirstOrDefault();
            vmd._selectedEmp.LocID = vmd.selectedEmp.Location.LocID;
            vmd._selectedEmp.Crew = vmd.listOfCrews.FirstOrDefault();
            vmd._selectedEmp.CrewID = vmd.selectedEmp.Crew.CrewID;
            vmd._selectedEmp.JoinDate = DateTime.Today;
            //vmd._selectedEmp.Status = vmd.selectedEmp.Status.Value;
            
            vmd._selectedEmp.ValidDate = DateTime.Today.AddYears(10);
           
            vmd.selectedEmp = vmd._selectedEmp;

            //vmd._selectedDept.Division = vmd.listOfDivs.FirstOrDefault();
            //vmd._selectedDept.DivID = vmd.selectedDept.Division.DivisionID;
            //vmd.selectedDept = vmd._selectedDept;

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
