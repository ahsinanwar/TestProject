using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Controllers;
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
            return (_vmAttData.selectedAttData != null);
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
                EditAttController _pma = new EditAttController(vmd.selectedAttData.EmpDate, "", false, (DateTime)vmd.selectedAttData.TimeIn, (DateTime)vmd.selectedAttData.TimeOut, vmd.selectedAttData.DutyCode, 1, (TimeSpan)vmd.selectedAttData.DutyTime, vmd.selectedAttData.Remarks, (short)vmd.selectedAttData.ShifMin);
            }
        }
        #endregion
    }
}
