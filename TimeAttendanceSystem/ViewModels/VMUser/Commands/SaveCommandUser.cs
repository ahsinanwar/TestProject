using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMUser.Commands
{
    class SaveCommandUser: ICommand
    {
        #region Fields
        
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandUser()
        {  }
        public bool CanExecute(object parameter)
        {
            //return (_vmshift.selectedShift != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMUser vmd = (VMUser)parameter;
           
           if (vmd.selectedUser.RoleID == null)
            { PopUp.popUp("User Role", "Please select a Role", NotificationType.Warning); }
            else
           
            if (vmd.isAdding)
            {
                if ( vmd.selectedUser.EmpID == null)
                {
                    PopUp.popUp("Give a Value", "Please write User ID before saving", NotificationType.Warning);   
                }
                else if (vmd.listOfSections.Count() == 0)
                {
                    PopUp.popUp("User", "Please Select a Section to give access to the User", NotificationType.Information);
                }
                else
                {
                    
                    using (TAS2013Entities ctx = new TAS2013Entities())
                    {
                        vmd.selectedUser.Emp = null;
                        //[user a,user b]
                        int Duplicate = ctx.Users.Where(aa => aa.UserName == vmd.selectedUser.UserName || aa.EmpID == vmd.selectedUser.EmpID).Count();
                        if (Duplicate > 0)
                        {
                            PopUp.popUp("User", "Duplicate Detected " + vmd.selectedUser.UserName, NotificationType.Warning);
                        }
                        else
                        {
                            ctx.Users.Add(vmd.selectedUser);
                            ctx.SaveChanges();
                            foreach (Section sec in vmd.listOfSections)
                            {

                                UserAccess uAccess = new UserAccess();
                                uAccess.UserID = (int)vmd.selectedUser.UserID;
                                uAccess.UserAccessRoleID = 1;
                                uAccess.CriteriaData = sec.SectionID;
                                ctx.UserAccesses.Add(uAccess);
                                ctx.SaveChanges();
                               
                            }
                            //int _userID = GlobalClasses.Global.user.UserID;
                            //HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.User, (byte)MyEnums.Operation.Add, DateTime.Now);
                            PopUp.popUp("User", "Successfully Saved " + vmd.selectedUser.UserName, NotificationType.Information);
                            vmd.listOfUsers.Add(vmd.selectedUser);
                        }
                    }
                   
                  
                }
            }
           
            else
            {
                User user = context.Users.First(aa => aa.UserID == vmd.selectedUser.UserID);
                user.UserName = vmd.selectedUser.UserName;
                user.RoleID = vmd.selectedUser.RoleID;
                user.CanAdd = vmd.selectedUser.CanAdd;
                user.CanView = vmd.selectedUser.CanView;
                user.CanEdit = vmd.selectedUser.CanEdit;
                user.CanDelete = vmd.selectedUser.CanDelete;
                user.ViewPermanentStaff = vmd.selectedUser.ViewPermanentStaff;
                user.ViewPermanentMgm = vmd.selectedUser.ViewPermanentStaff;
                user.ViewContractualStaff = vmd.selectedUser.ViewContractualStaff;
                user.ViewContractualMgm = vmd.selectedUser.ViewContractualMgm;
                user.MLeave = vmd.selectedUser.MLeave;
                user.MRMonthly = vmd.selectedUser.MRMonthly;
                user.MRAudit = vmd.selectedUser.MRAudit;
                user.MREmployee = vmd.selectedUser.MREmployee;
                user.MRDetail = vmd.selectedUser.MRDetail;
                user.MRDailyAtt = vmd.selectedUser.MRDailyAtt;
                user.MRSummary = vmd.selectedUser.MRSummary;
                user.MHR = vmd.selectedUser.MHR;
                user.MEditAtt = vmd.selectedUser.MEditAtt;
                //user.Report = vmd.selectedUser.ViewPermanentStaff;
                user.MUser = vmd.selectedUser.MUser;
                user.MRoster = vmd.selectedUser.MRoster;
                user.MDevice = vmd.selectedUser.MDevice;
                user.MLeave = vmd.selectedUser.MLeave;
                //Code For User Access deletion and addition with new Sections
                List<UserAccess> listOfUserAcccess = new List<UserAccess>();
                listOfUserAcccess = context.UserAccesses.Where(aa => aa.UserID == user.UserID).ToList();
                foreach (UserAccess UAcess in listOfUserAcccess)
                    context.UserAccesses.Remove(UAcess);
                context.SaveChanges();
                foreach (Section sec in vmd.listOfSections)
                {

                    UserAccess uAccess = new UserAccess();
                    uAccess.UserID = (int)vmd.selectedUser.UserID;
                    uAccess.UserAccessRoleID = 1;
                    uAccess.CriteriaData = sec.SectionID;
                    context.UserAccesses.Add(uAccess);
                    context.SaveChanges();

                }
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                //int _userID = GlobalClasses.Global.user.UserID;
                //HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.User, (byte)MyEnums.Operation.Edit, DateTime.Now);
                PopUp.popUp("User", "Successfully Edited " + vmd.selectedUser.UserName, NotificationType.Information);   
            }

        }
        #endregion
    }
}
