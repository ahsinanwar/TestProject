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
                        else if (vmd.selectedEmp.Gender == null)
                    {
                        PopUp.popUp("Empty Value", "Please Select Gender before saving", NotificationType.Warning);
                    }
                    else if (vmd.selectedEmp.MarStatus == null)
                    {
                        PopUp.popUp("Empty Value", "Please Select MarStatus before saving", NotificationType.Warning);
                    }
                    else if (vmd.selectedEmp.BirthDate== null)
                    {
                        PopUp.popUp("Empty Value", "Please Select BirthDate before saving", NotificationType.Warning);
                    }

                    else
                    {
                        vmd.listOfEmps.Add(vmd.selectedEmp);
                        Emp dummy = new Emp();
                        dummy = vmd._selectedEmp;
                        dummy.JobID = vmd.selectedEmp.JobTitle.JobID;
                        dummy.SecID = vmd.selectedEmp.Section.SectionID;
                        dummy.CrewID = vmd.selectedEmp.Crew.CrewID;
                        dummy.DesigID = vmd.selectedEmp.Designation.DesignationID;
                        Designation des = new Designation();
                        des = vmd.selectedEmp.Designation;
                        JobTitle jt = new JobTitle();
                        jt = vmd.selectedEmp.JobTitle;
                        Section sec = new Section();
                        Crew crew = new Crew();
                        crew = vmd.selectedEmp.Crew;
                        sec = vmd.selectedEmp.Section;
                        Location locheed = new Location();
                        locheed = vmd.selectedEmp.Location;
                        EmpType empty = new EmpType();
                        empty = vmd.selectedEmp.EmpType;
                        Grade grade = new Grade();
                        grade = vmd.selectedEmp.Grade;
                        dummy.Section.Department = null;
                        //vmd.selectedEmp.Section.Department.Division = null;
                        dummy.Section = null;
                       
                        dummy.Crew = null;
                        dummy.JobTitle = null;
                        dummy.Designation = null;
                        dummy.EmpType = null;
                        // dummy.EmpID = null;
                        dummy.Location = null;
                        dummy.Grade = null;


                     
                        //dummy.EmpPhoto = null;
                        // vmd.selectedEmp.Section = null;
                        dummy.Shift = null;
                        context.Emps.Add(dummy);
                        
                        context.SaveChanges();
                        //int _userID = GlobalClasses.Global.user.UserID;
                        //HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Employee, (byte)MyEnums.Operation.Add, DateTime.Now);
                        dummy.Section = sec;
                        dummy.Crew = crew;
                        dummy.JobTitle = jt;
                        dummy.Designation = des;
                        dummy.EmpType = empty;
                        dummy.Location = locheed;
                        dummy.Grade = grade;

                        
                        
                        PopUp.popUp("Employee", vmd.selectedEmp.EmpName+ " is created", NotificationType.Information);
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

                    
                    

                    vmd.selectedEmp.TypeID = vmd.selectedEmp.EmpType.TypeID;
                    vmd.selectedEmp.DesigID = vmd.selectedEmp.Designation.DesignationID;
                    vmd.selectedEmp.GradeID = vmd.selectedEmp.Grade.GradeID;

                    vmd.selectedEmp.ShiftID = vmd.selectedEmp.Shift.ShiftID;
                    vmd.selectedEmp.SecID = vmd.selectedEmp.Section.SectionID;
                    vmd.selectedEmp.LocID = vmd.selectedEmp.Location.LocID;

                    vmd.selectedEmp.CrewID = vmd.selectedEmp.Crew.CrewID;
              
                    

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
                PopUp.popUp("Eror", "Some error while saving", NotificationType.Warning);
            }
        }

        private void Refresh()
        {
            throw new NotImplementedException();
        }

        
        #endregion
    }
}
