using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMUser.Commands
{
    class SaveCommandUser: ICommand
    {
        #region Fields
        VMUser _vmuser;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandUser(VMUser vm)
        { _vmuser = vm; }
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
            if (vmd.isAdding)
            {
                context.Users.Add(vmd.selectedUser);
                context.SaveChanges();
                vmd.listOfUsers.Add(vmd.selectedUser);

            }
            else
            {
                User user = context.Users.First(aa => aa.UserID == vmd.selectedUser.UserID);
                user.UserName = vmd.selectedUser.UserName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
