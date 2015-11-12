using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Controllers;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMAttEdit.Commands
{
    public class SaveCommandAttEdit: ICommand
    {
         #region Fields
        VMAttEdit _vmAttData;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandAttEdit(VMAttEdit vm)
        { _vmAttData = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmAttData.AttDataShow != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMAttEdit vmd = (VMAttEdit)parameter;
            if (vmd.isAdding)
            {
                
                //context.Categories.Add(vmd.selectedCat);
                //context.SaveChanges();
                //vmd.listOfCats.Add(vmd.selectedCat);

            }
            else
            {

                if (vmd.AttDataShow.TimeIn > vmd.AttDataShow.TimeOut)
                {
                    PopUp.popUp("Attendance Edit", "Time Out cannot be greater than Time In", NotificationType.Warning);
                    return;
                }

                try
                {
                    EditAttController _pma = new EditAttController(vmd.AttDataShow.EmpDate, "", false, (DateTime)vmd.AttDataShow.TimeIn, (DateTime)vmd.AttDataShow.TimeOut, vmd.AttDataShow.DutyCode, 1, (TimeSpan)vmd.AttDataShow.DutyTime, vmd.AttDataShow.Remarks, (short)vmd.AttDataShow.ShifMin);
                    AttData tempdata = context.AttDatas.FirstOrDefault(aa => aa.EmpDate == vmd.AttDataShow.EmpDate);
                    vmd.AttDataShow = tempdata;
                    PopUp.popUp("Attendance Edit", "Attendance edit successful for " + vmd.AttDataShow.Emp.EmpName, NotificationType.Information);
                    return;
                }
                catch (Exception)
                {
                    //throw;
                }
            }
            vmd.isAdding = false;
            vmd.isEnabled = false;
        }
        #endregion
    }
}
