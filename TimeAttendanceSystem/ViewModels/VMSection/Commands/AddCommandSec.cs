using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection.Commands
{
    class AddCommandSec: ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Section _vm = new Section();
        #endregion

        #region constructors
        public AddCommandSec(Section vm)
        { _vm = vm; }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            vmd._selectedSec = new Section();
            vmd._selectedSec.Department=vmd.listOfDepts.FirstOrDefault();
            vmd._selectedSec.DeptID = vmd.selectedSec.Department.DeptID;
            vmd.selectedSec = vmd._selectedSec;
            vmd.isAdding = true;
            vmd.isEnabled = true;
            //   context.SaveChanges();
            //Console.WriteLine(vmd.DeptName);
            // Console.WriteLine(vmd.DivID);
            // Console.WriteLine(vmd.DeptID);
            // Console.WriteLine(vmd.CompanyID);
        }
        #endregion
    }
}
