using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class DeleteCommandLoc : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Location _vm = new Location();
        #endregion
        public DeleteCommandLoc(Location vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLocation vmd = (VMLocation)parameter;
            Location selectedLoc = context.Locations.FirstOrDefault(aa => aa.LocID == vmd.selectedLoc.LocID);
            List<Emp> emp = new List<Emp>();
            emp = context.Emps.Where(aa => aa.LocID == selectedLoc.LocID).ToList();
            //vmd.isAdding = true;
            //vmd.isEnabled = true;

            try
            {
                if (emp.Count > 0)
                {
                    PopUp.popUp("Location", "Please delete Employee before Location Deletion", NotificationType.Information);
                }
                else
                {
                    vmd.listOfLocs.Remove(vmd.selectedLoc);
                    int _userID = GlobalClasses.Global.user.UserID;
                    HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Location, (byte)MyEnums.Operation.Delete, DateTime.Now);
                    vmd.selectedLoc = vmd.listOfLocs[0];
                    PopUp.popUp("Location", "Location is Deleted", NotificationType.Information);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
            
        }
    }
}
