using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMAttEdit.Commands
{
    class SearchCommandAttEdit:ICommand
    {
          #region Fields
        VMAttEdit _vmcategory;
        TAS2013Entities context = new TAS2013Entities();
        #endregion

        #region constructors
        public SearchCommandAttEdit(VMAttEdit vm)
        { _vmcategory = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcategory.selectedAttData != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMAttEdit vmattedit = (VMAttEdit)parameter;
            AttData attData = new AttData();
            attData = (AttData)context.AttDatas.FirstOrDefault(aa => aa.AttDate == vmattedit.AttDataShow.AttDate && aa.EmpID == vmattedit.AttDataShow.EmpID);
            vmattedit.AttDataShow = attData;
        }
        #endregion
    }
}
