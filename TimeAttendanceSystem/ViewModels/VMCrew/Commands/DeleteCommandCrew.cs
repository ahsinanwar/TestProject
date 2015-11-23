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
    class DeleteCommandCrew : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Crew _vm = new Crew();
        #endregion
        public DeleteCommandCrew(Crew vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMCrew vmd = (VMCrew)parameter;
            Crew selectedCrew = context.Crews.FirstOrDefault(aa => aa.CrewID == vmd.selectedCrew.CrewID);
             int no = context.Emps.Where(aa => aa.CrewID == vmd.selectedCrew.CrewID).Count();
             if (no > 0)
             {
                 PopUp.popUp("Crew", "There are Employees in this Crew. Please remove them first", Mantin.Controls.Wpf.Notification.NotificationType.Error);
             }
             else
             {
                 context.Crews.Remove(selectedCrew);
                 //vmd.isAdding = true;
                 //vmd.isEnabled = true;
                 try
                 {
                     if (context.SaveChanges() > 0)
                     {
                         vmd.listOfCrews.Remove(vmd.selectedCrew);
                         vmd.selectedCrew = vmd.listOfCrews[0];
                         PopUp.popUp("Crew", "Crew Deleted", Mantin.Controls.Wpf.Notification.NotificationType.Information);
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
