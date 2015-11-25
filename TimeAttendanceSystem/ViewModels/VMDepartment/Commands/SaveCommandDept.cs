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
    class SaveCommandDept :ICommand
    {
        #region Fields
        VMDepartments _vmdepartment;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandDept(VMDepartments vm)
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
            VMDepartments vmd = (VMDepartments)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedDept.DeptName == "" || vmd.selectedDept.DeptName == null)
                {
                    PopUp.popUp("Department", "Please write Department Name before saving", NotificationType.Warning);
                }

                else
                {
                    if (context.Departments.Where(aa => aa.DeptName == vmd.selectedDept.DeptName).Count() > 0)
                    {
                        PopUp.popUp("Department", "Department Name must be Unique", NotificationType.Warning);
                    }
                    else
                    {
                        Department dummy = vmd.selectedDept;
                        dummy.Division = context.Divisions.Where(aa => aa.DivisionID == dummy.DivID).FirstOrDefault();
                        vmd.listOfDepts.Add(dummy);
                        dummy.Division = null;
                        context.Departments.Add(vmd.selectedDept);
                       context.SaveChanges();
                       int _userID = GlobalClasses.Global.user.UserID;
                       HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Department, (byte)MyEnums.Operation.Add, DateTime.Now);

                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                      
                        PopUp.popUp("Department", dummy.DeptName+" is created Successfully", NotificationType.Information);
                  
                    }
                }
            }
            else
            {
                if (vmd.selectedDept.DeptName == "" || vmd.selectedDept.DeptName == null)
                {
                    PopUp.popUp("Department", "Please write a Section Name before saving", NotificationType.Warning);
                }
                else
                {
                    Department dept = context.Departments.First(aa => aa.DivID == vmd.selectedDept.Division.DivisionID);
                    dept.Division.DivisionName = vmd.selectedDept.Division.DivisionName;
                    dept.Division.DivisionID = vmd.selectedDept.Division.DivisionID;
                    vmd.isEnabled = false;
                    vmd.isAdding = false;
                    context.SaveChanges();
                    int _userID = GlobalClasses.Global.user.UserID;
                    HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Department, (byte)MyEnums.Operation.Edit, DateTime.Now);

                    PopUp.popUp("Department", dept.DeptName+" Edited", NotificationType.Information);
              
                }
            }

            //    context.Departments.Add(vmd.selectedDept);
            //    context.SaveChanges();
            //    vmd.selectedDept.Division = context.Divisions.FirstOrDefault(aa => aa.DivisionID == vmd.selectedDept.DivID);
            //    vmd.listOfDepts.Add(vmd.selectedDept);
            //    vmd.isEnabled = false;
            //    vmd.isAdding = false;

            //}
            //else {
            //    Department dept = context.Departments.First(aa => aa.DeptID == vmd.selectedDept.DeptID);
            //    dept.DeptName = vmd.selectedDept.DeptName;
            //    dept.DivID = vmd.selectedDept.DivID;
            //    vmd.isEnabled = false;
            //    vmd.isAdding = false;
            //    context.SaveChanges();
            //     }

        }
        #endregion
    
    }
}
