using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShift.Commands
{
    class AddCommandShift : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Shift _vm = new Shift();
        #endregion

        #region constructors
        public AddCommandShift(Shift vm)
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
            VMShift vmd = (VMShift)parameter;
            vmd.selectedShift = new Shift();
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
