using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class DeleteCommandRdr : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Reader _vm = new Reader();
        #endregion
        public DeleteCommandRdr(Reader vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            Reader selectedRdr = context.Readers.FirstOrDefault(aa => aa.RdrID == vmd.selectedRdr.RdrID);
            if(selectedRdr != null)
            context.Readers.Remove(selectedRdr);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            List<Emp> emp = new List<Emp>();
            emp = context.Emps.Where(aa => aa.ReaderID == selectedRdr.RdrID).ToList();
            List<PollData> polls = new List<PollData>();
            polls = context.PollDatas.Where(aa => aa.RdrID == selectedRdr.RdrID).ToList();

            try
            {
                if (emp.Count > 0)
                {
                    PopUp.popUp("Reader", "Please move Employees to another reader before Deletion", NotificationType.Warning);
                }
                else if (polls.Count > 0)
                {
                    PopUp.popUp("Reader", "Cannot delete reader as it has some attendance associated with it. It will affect all reports", NotificationType.Warning);
                }
                else
                {
                    if (context.SaveChanges() > 0)
                    {
                        int _userID = GlobalClasses.Global.user.UserID;
                        HelperClasses.MyHelper.SaveAuditLog(_userID, (byte)MyEnums.FormName.Reader, (byte)MyEnums.Operation.Delete, DateTime.Now);
                        PopUp.popUp("Reader", "Reader Deleted Successfully", NotificationType.Warning);
                        vmd.listOfRdrs.Remove(vmd.selectedRdr);
                        vmd.selectedRdr = vmd.listOfRdrs[0];
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
