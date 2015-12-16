using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCity.Commands
{
    class SaveCommand : ICommand
    {
        #region Fields
        VMCity _vmcity;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMCity vm)
        { _vmcity = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmcity.selectedCity != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCity vmd = (VMCity)parameter;
            if (vmd.isAdding)
            {
                context.Cities.Add(vmd.selectedCity);
                context.SaveChanges();
                vmd.listOfcities.Add(vmd.selectedCity);

            }
            else
            {
                City city = context.Cities.First(aa => aa.CityID == vmd.selectedCity.CityID);
                city.CityName = vmd.selectedCity.CityName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
