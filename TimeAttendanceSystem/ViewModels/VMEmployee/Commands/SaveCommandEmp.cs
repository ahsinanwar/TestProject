using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            try
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
                        PopUp.popUp("Empty Value", "Please write Emp ID before saving", NotificationType.Warning);
                    }

                    else if (context.Emps.Where(aa => aa.EmpNo == vmd.selectedEmp.EmpNo).Count() > 0)
                    {
                        PopUp.popUp("Duplication", "Emp no already exit", NotificationType.Warning);
                    }
                    else
                    {
                        Emp dummy = vmd.selectedEmp;
                        dummy.Section.Department = null;
                        dummy.Section = null;
                        dummy.Crew = null;
                        dummy.Designation = null;
                        dummy.EmpType = null;
                        // dummy.EmpID = null;
                        dummy.Location = null;
                        dummy.Grade = null;
                        dummy.Gender = dummy.Gender1.GenderID;
                        dummy.Gender1 = null;
                        dummy.MarStatus = dummy.Married.MarriedID;
                        dummy.Married = null;
                         //dummy.EmpPhoto = null;
                        // vmd.selectedEmp.Section = null;
                        dummy.Shift = null;
                        context.Emps.Add(dummy);
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

                    if (emp.EmpPhoto == null && vmd.dummyEmp.EmpPhoto != null)
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
                        PopUp.popUp("No Photo", "Emloyee Saved Without a Photo", NotificationType.Warning);
                    //A bad approach but way to handle this when u change the section we need to change its ID too
                    vmd.selectedEmp.SecID = vmd.selectedEmp.Section.SectionID;
                    vmd.selectedEmp.Gender = vmd.selectedEmp.Gender1.GenderID;
                    vmd.selectedEmp.MarStatus = vmd.selectedEmp.Married.MarriedID;
                    context.Entry(emp).CurrentValues.SetValues(vmd.selectedEmp);
                   // emp = vmd.selectedEmp;
                   //context.Entry(emp).State = EntityState.Modified;
                   
                    context.SaveChanges();
                    vmd.isEnabled = false;
                    vmd.isAdding = false;
                   
                }
            }
            catch (Exception ex)
            {
                PopUp.popUp("Eror", ex.InnerException.ToString(), NotificationType.Warning);
            }

        }

        
        #endregion
    }
}
