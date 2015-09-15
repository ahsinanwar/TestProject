using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class AddCommandCrew : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Crew _vm = new Crew();
        #endregion

        #region constructors
        public AddCommandCrew(Crew vm)
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
            VMCrew vmd = (VMCrew)parameter;
            vmd.selectedCrew = new Crew();
            //vmd._selectedCrew = vmd.listOfCrews.FirstOrDefault();
            //vmd._selectedCrew.CrewID = vmd.selectedCrew.CrewID;
            //vmd.selectedCrew = vmd._selectedCrew;

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
