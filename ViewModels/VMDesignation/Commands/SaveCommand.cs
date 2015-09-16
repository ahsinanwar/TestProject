using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDesignation.Commands
{
    class SaveCommand :ICommand
    {
        #region Fields
        VMDesignation _vmdesignation;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMDesignation vm)
        { _vmdesignation = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdesignation.selectedDesg != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDesignation vmd = (VMDesignation)parameter;
            if (vmd.isAdding)
            {
                context.Designations.Add(vmd.selectedDesg);
                context.SaveChanges();
                vmd.listOfDesgs.Add(vmd.selectedDesg);

            }
            else
            {
                Designation desg = context.Designations.First(aa => aa.DesignationID == vmd.selectedDesg.DesignationID);
                desg.DesignationName = vmd.selectedDesg.DesignationName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
