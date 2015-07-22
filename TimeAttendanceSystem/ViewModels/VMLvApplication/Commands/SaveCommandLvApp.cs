using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Controllers;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.HelperClasses;
using Mantin.Controls.Wpf.Notification;

namespace TimeAttendanceSystem.ViewModels.VMLvApplication.Commands
{
    class SaveCommandLvApp : ICommand
    {
        #region Fields
        VMLvApplication _vmlvapp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandLvApp(VMLvApplication vm)
        { _vmlvapp = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmlvapp.selectedLvApp != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvApplication vmd = (VMLvApplication)parameter;
            if (vmd.isAdding)
            {   

                LvController lvctrl = new LvController();
                bool chkIsFromToDateValid = lvctrl.IsDateFromToValid(vmd.selectedEmpAndLvApp.LvApp);
                if (!chkIsFromToDateValid)
                {
                    PopUp.popUp("From/To Date Invalid", "From Date should be less than To Date", NotificationType.Warning);
                }
                else if (vmd.selectedEmpAndLvApp.LvApp.EmpID == null)
                {
                    PopUp.popUp("Empty Value", "Please select an employee first", NotificationType.Warning);

                }
                else if (vmd.selectedEmpAndLvApp.LvApp.TypeID == null)
                {
                    PopUp.popUp("Empty Value", "Please select leave type", NotificationType.Warning);

                }
                else
                {

                    // IF Leave is half Leave
                    if (vmd.selectedEmpAndLvApp.LvApp.IsHalf == true)
                    {

                        if (vmd.selectedEmpAndLvApp.LvApp.FirstHalf == null)
                        {
                            PopUp.popUp("Half Day", "Please select First Half or Second Half", NotificationType.Warning);

                        }
                        else
                            if ((vmd.selectedEmpAndLvApp.LvApp.ToDate - vmd.selectedEmpAndLvApp.LvApp.FromDate).Days == 0)
                            {

                                bool chkdup = lvctrl.CheckDuplicateLeave(vmd.selectedEmpAndLvApp.LvApp);
                                if (chkdup == false)
                                {
                                    vmd.selectedEmpAndLvApp.LvApp.NoOfDays = (float)0.5;
                                    context.LvApplications.Add(vmd.selectedEmpAndLvApp.LvApp);
                                    context.SaveChanges();
                                    lvctrl.AddHalfLeaveToLeaveData(vmd.selectedEmpAndLvApp.LvApp);
                                    lvctrl.AddHalfLeaveToAttData(vmd.selectedEmpAndLvApp.LvApp);
                                    lvctrl.BalanceLeaves(vmd.selectedEmpAndLvApp.LvApp);
                                    vmd.listOfEmpsAndLvApps.Add(vmd.selectedEmpAndLvApp);
                                    PopUp.popUp("Application", "Application has been successfully registered for " + vmd.selectedEmpAndLvApp.Employee.EmpName, NotificationType.Warning);
                                    vmd.selectedEmpAndLvApp = new CombinedEmpAndLvApps();
                                }
                            }
                            else
                            {
                                PopUp.popUp("Half Day", "Please select same Dates", NotificationType.Warning);
                            }
                    }
                    else
                    {
                        bool chkdup = lvctrl.CheckDuplicateLeave(vmd.selectedEmpAndLvApp.LvApp);
                        if (chkdup == false)
                        {
                            bool chkbal = lvctrl.CheckLeaveBalance(vmd.selectedEmpAndLvApp.LvApp);
                            if (chkbal == true)
                            {
                                vmd.selectedEmpAndLvApp.LvApp.NoOfDays = (vmd.selectedEmpAndLvApp.LvApp.ToDate - vmd.selectedEmpAndLvApp.LvApp.FromDate).Days + 1;
                                context.LvApplications.Add(vmd.selectedEmpAndLvApp.LvApp);
                                context.SaveChanges();
                                vmd.selectedEmpAndLvApp.LvApp.LvDate = DateTime.Now;
                                lvctrl.AddLeaveToLeaveAttData(vmd.selectedEmpAndLvApp.LvApp);
                                lvctrl.AddLeaveToLeaveData(vmd.selectedEmpAndLvApp.LvApp);
                                lvctrl.BalanceLeaves(vmd.selectedEmpAndLvApp.LvApp);
                                vmd.listOfEmpsAndLvApps.Add(vmd.selectedEmpAndLvApp);
                                PopUp.popUp("Application", "Application has been successfully registered for " + vmd.selectedEmpAndLvApp.Employee.EmpName, NotificationType.Warning);
                                vmd.selectedEmpAndLvApp = new CombinedEmpAndLvApps();

                            }
                            else
                            {
                                PopUp.popUp("Exceeds", "Leave balance exceeds", NotificationType.Warning);
                            }
                        }
                        else
                        {
                            PopUp.popUp("Duplicate", "Leave is duplicate, Please select unqiue days", NotificationType.Warning);
                        }
                    }
                }
                    
                

            }
            else
            {
                LvApplication lvapp = new LvApplication();
                lvapp = context.LvApplications.FirstOrDefault(aa => aa.LvID == vmd.selectedEmpAndLvApp.LvApp.LvID);
                lvapp = vmd.selectedEmpAndLvApp.LvApp;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                PopUp.popUp("Application", "Application has been successfully edited for " + vmd.selectedEmpAndLvApp.Employee.EmpName, NotificationType.Warning);
                            
            }

        }
        #endregion
    }
}
