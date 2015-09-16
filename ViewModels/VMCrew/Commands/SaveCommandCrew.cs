using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMCrew.Commands
{
    class SaveCommandCrew : ICommand
    {
        #region Fields
        VMCrew _vmcrew;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandCrew(VMCrew vm)
        { _vmcrew = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmcrew.selectedCrew != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedCrew.CrewName == "" || vmd.selectedCrew.CrewName == null)
                {
                    PopUp.popUp("Empty Value", "Please write Crew name before saving", NotificationType.Warning);
                }

                else
                {
                    if (context.Crews.Where(aa => aa.CrewName == vmd.selectedCrew.CrewName).Count() > 0)
                    {
                        PopUp.popUp("Sorry!", "Crew already been created", NotificationType.Warning);
                    }
                    else
                    {
                        context.Crews.Add(vmd.selectedCrew);
                        context.SaveChanges();
                        vmd.listOfCrews.Add(vmd.selectedCrew);
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        PopUp.popUp("Congratulations", "A Crew is Created", NotificationType.Warning);
                  
                    }
                }

            }
            else
            {
                Crew crew = context.Crews.First(aa => aa.CrewID == vmd.selectedCrew.CrewID);
                crew.CrewName = vmd.selectedCrew.CrewName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
                PopUp.popUp("Congratulations", "Emptype is Created", NotificationType.Warning);
                  
            }

        }
        #endregion
    }
}
