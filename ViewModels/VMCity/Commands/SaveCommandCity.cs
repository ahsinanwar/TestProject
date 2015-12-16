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
    class SaveCommandCity : ICommand
    {
        #region Fields
        VMCity _vmcity;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandCity(VMCity vm)
        { _vmcity = vm; }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCity vmd = (VMCity)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedCity.CityName == "" || vmd.selectedCity.CityName == null)
                {
                    PopUp.popUp("Empty Value", "Please write a city name before saving", NotificationType.Warning);

                }
                else
                {
                    if (context.Cities.Where(aa => aa.CityName == vmd.selectedCity.CityName).Count() > 0)
                    {
                        PopUp.popUp("Duplicate", "Please write a unique name", NotificationType.Warning);

                    }
                    else
                    {
                        context.Cities.Add(vmd.selectedCity);
                        context.SaveChanges();
                        vmd.listOfCities.Add(vmd.selectedCity);
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                    }
                }
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
