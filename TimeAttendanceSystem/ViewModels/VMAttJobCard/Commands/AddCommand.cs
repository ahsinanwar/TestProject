using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMAttJobCard.Commands
{
    class AddCommand :ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
       // VMAttJobCard _vm = new VMAttJobCard();
        #endregion

        //#region constructors
        //public AddCommand()
        //{ _vm = vm; }
        public bool CanExecute(object parameter)
        {
            return true;
        }
       // #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            VMAttJobCard vmd = (VMAttJobCard)parameter;
            vmd._selectedJobCardApp = new JobCardApp();
            vmd.selectedAttData = new AttData();
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
