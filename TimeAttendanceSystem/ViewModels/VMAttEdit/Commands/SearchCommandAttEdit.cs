using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            return (_vmcategory.AttDataShow != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMAttEdit vmattedit = (VMAttEdit)parameter;
            AttData attData = new AttData();
            attData = (AttData)context.AttDatas.FirstOrDefault(aa => aa.AttDate == vmattedit.selectedDate && aa.EmpID == vmattedit.selectedEmp.EmpID);
            ObservableCollection<AttData> listAttData = new ObservableCollection<AttData>(context.AttDatas.Where(aa => aa.AttDate == vmattedit.selectedDate && aa.EmpID == vmattedit.selectedEmp.EmpID).ToList());
            vmattedit.AttDataShow = attData;
            vmattedit.listOfAttData = listAttData;
        }
        #endregion
    }
}
