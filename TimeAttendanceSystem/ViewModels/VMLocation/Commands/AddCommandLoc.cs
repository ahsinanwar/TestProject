using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class AddCommandLoc : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Location _vm = new Location();
        #endregion

        #region constructors
        public AddCommandLoc(Location vm)
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
            VMLocation vmd = (VMLocation)parameter;
            vmd._selectedLoc = new Location();
            vmd._selectedLoc.City = vmd.listOfCities.FirstOrDefault();
            vmd._selectedLoc.City.CityID = vmd.selectedLoc.City.CityID;
            vmd.selectedLoc = vmd._selectedLoc;

            //VMEmpType vmd = (VMEmpType)parameter;
            //vmd._selectedEmpType = new EmpType();
            //vmd._selectedEmpType.Category = vmd.listOfCats.FirstOrDefault();
            //vmd._selectedEmpType.CatID = vmd.selectedEmpType.Category.CatID;
            //vmd.selectedEmpType = vmd._selectedEmpType;


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
