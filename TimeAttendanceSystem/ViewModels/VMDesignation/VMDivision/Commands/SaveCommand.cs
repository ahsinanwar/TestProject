using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDivision.Commands
{
    class SaveCommand :ICommand
    {
        #region Fields
        VMDivision _vmdivision;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMDivision vm)
        { _vmdivision = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmdivision.selectedDiv != null);
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
           VMDivision vmd= (VMDivision)parameter;
           if (vmd.isAdding)
           {
               context.Divisions.Add(vmd.selectedDiv);
               context.SaveChanges();
               vmd.listOfDivs.Add(vmd.selectedDiv);

           }
           else {
               Division div = context.Divisions.First(aa => aa.DivisionID == vmd.selectedDiv.DivisionID);
               div.DivisionName = vmd.selectedDiv.DivisionName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
                }
         
        }
        #endregion
    }
}
