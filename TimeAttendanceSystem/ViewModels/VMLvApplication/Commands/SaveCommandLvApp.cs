using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

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
            return (_vmlvapp.selectedLvApp != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMLvApplication vmd = (VMLvApplication)parameter;
            if (vmd.isAdding)
            {
                context.LvApplications.Add(vmd.selectedLvApp);
                context.SaveChanges();
                vmd.listOfLvApps.Add(vmd.selectedLvApp);

            }
            else
            {
                LvApplication lvapp = context.LvApplications.First(aa => aa.LvID == vmd.selectedLvApp.LvID);
                lvapp.LvType = vmd.selectedLvApp.LvType;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
