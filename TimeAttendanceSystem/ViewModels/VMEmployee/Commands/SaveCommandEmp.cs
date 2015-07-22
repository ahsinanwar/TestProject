using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class SaveCommandEmp : ICommand
    {
        #region Fields
        VMEmployee _vmemp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandEmp(VMEmployee vm)
        { _vmemp = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmemp.selectedEmp != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {

            VMEmployee vmd = (VMEmployee)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedEmp.EmpName == "" || vmd.selectedEmp.EmpName == null)
                       {
                           PopUp.popUp("Empty Value", "Please write Emp Name before saving", NotificationType.Warning);
                       }
                else if (vmd.selectedEmp.EmpNo == "" || vmd.selectedEmp.EmpNo == null)
                    {
                        PopUp.popUp("Empty Value", "Please write Emp No before saving", NotificationType.Warning);
                    }
                  
                else if (context.Emps.Where(aa => aa.EmpNo == vmd.selectedEmp.EmpNo).Count() > 0)
  
                    {
                        PopUp.popUp("Duplication", "Emp no already exit", NotificationType.Warning);
                    }
                else
                    {
                
                        context.Emps.Add(vmd.selectedEmp);
                        context.SaveChanges();
                        vmd.listOfEmps.Add(vmd.selectedEmp);
                        PopUp.popUp("Congratulations", "Emp is Created", NotificationType.Warning);
                    }
              }
       
            else
            {
                Emp emp = context.Emps.First(aa => aa.EmpID == vmd.dummyEmp.EmpID);
                emp.EmpName = vmd.dummyEmp.EmpName;


                emp.EmpPhoto = context.EmpPhotoes.FirstOrDefault(aa => aa.EmpID == emp.EmpID);

                if (emp.EmpPhoto == null)
                {
                    EmpPhoto ep = new EmpPhoto()
                    {
                        EmpID = emp.EmpID,
                        EmpPic = vmd.dummyEmp.EmpPhoto.EmpPic
                    };
                    context.EmpPhotoes.Add(ep);
                    emp.EmpImageID = ep.PhotoID;
                }
                else
                {
                    
                }                       
                context.SaveChanges();
                emp.EmpImageID = emp.EmpPhoto.PhotoID;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }

        
        #endregion
    }
}
