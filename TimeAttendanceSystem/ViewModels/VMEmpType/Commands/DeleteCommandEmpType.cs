using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMEmpType.Commands
{
    class DeleteCommandEmpType : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        EmpType _vm = new EmpType();
        #endregion
        public DeleteCommandEmpType(EmpType vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmpType vmd = (VMEmpType)parameter;
            EmpType selectedEmpType = context.EmpTypes.FirstOrDefault(aa => aa.TypeID== vmd.selectedEmpType.TypeID);
            int emptypeno = context.Emps.Where(aa => aa.TypeID == vmd.selectedEmpType.TypeID).Count();
            if (emptypeno > 0)

            { PopUp.popUp("Employee Type", "Remove the Employees with this type first", Mantin.Controls.Wpf.Notification.NotificationType.Information); }
            else
            {
                context.EmpTypes.Remove(selectedEmpType);
                //vmd.isAdding = true;
                //vmd.isEnabled = true;
                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.EmpType, (byte)MyEnums.Operation.Delete, DateTime.Now);
                        vmd.listOfEmpTypes.Remove(vmd.selectedEmpType);
                        vmd.selectedEmpType = vmd.listOfEmpTypes[0];
                        PopUp.popUp("Employee Type", "Employee Type Deleted", Mantin.Controls.Wpf.Notification.NotificationType.Information);

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
