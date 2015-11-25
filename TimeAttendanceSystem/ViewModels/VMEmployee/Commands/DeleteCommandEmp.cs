using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.HelperClasses;
namespace TimeAttendanceSystem.ViewModels.VMEmployee.Commands
{
    class DeleteCommandEmp : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Emp _vm = new Emp();
        #endregion
        public DeleteCommandEmp(Emp vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMEmployee vmd = (VMEmployee)parameter;
            Emp selectedEmp = context.Emps.FirstOrDefault(aa => aa.EmpID == vmd.selectedEmp.EmpID);
            List<AttData> empdatas = context.AttDatas.Where(aa => aa.EmpID == selectedEmp.EmpID).ToList();
            foreach (AttData data in empdatas)
            {
                context.AttDatas.Remove(data);
            }
          //  context.AttDatas.DeleteObjects(empdatas);
            context.Emps.Remove(selectedEmp);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {

                context.SaveChanges();
                int _userID = GlobalClasses.Global.user.UserID;
                HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Employee, (byte)MyEnums.Operation.Delete, DateTime.Now);
                    vmd.listOfEmps.Remove(vmd.selectedEmp);
                    vmd.selectedEmp = vmd.listOfEmps[0];
                
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
