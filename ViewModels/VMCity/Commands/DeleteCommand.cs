using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCity.Commands
{
     class DeleteCommand : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        City _vm = new City();
        #endregion
        public DeleteCommand(City vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCity vmd = (VMCity)parameter;
            City selectedCity = context.Cities.FirstOrDefault(aa => aa.CityID == vmd.selectedCity.CityID);
            context.Cities.Remove(selectedCity);
            //vmd.isAdding = true;
            //vmd.isEnabled = true;
            try
            {
                if (context.SaveChanges() > 0)
                {
                    vmd.listOfcities.Remove(vmd.selectedCity);
                    vmd.selectedCity = vmd.listOfcities[0];
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception While Deleting...");
            }
        }
    }
}
