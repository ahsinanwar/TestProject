using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCity.Commands
{
     class DeleteCommandCity : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        City _vm = new City();
        #endregion
        public DeleteCommandCity(City vm)
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

            List<Emp> emp = new List<Emp>();
            emp = context.Emps.Where(aa => aa.Location.CityID == selectedCity.CityID).ToList();

            if (emp.Count > 0)
            {
                PopUp.popUp("Not Deleted", "Please move Employees of this city to another city before making an attempt to delete.", NotificationType.Warning);
            }
            else
            {
                context.Cities.Remove(selectedCity);
                //vmd.isAdding = true;
                //vmd.isEnabled = true;
                try
                {
                    if (context.SaveChanges() > 0)
                    {
                        vmd.listOfCities.Remove(vmd.selectedCity);
                        vmd.selectedCity = vmd.listOfCities[0];
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception While Deleting...");
                }
            }
        }
    }
}
