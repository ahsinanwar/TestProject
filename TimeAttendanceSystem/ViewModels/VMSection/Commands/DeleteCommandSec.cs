using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection.Commands
{
    class DeleteCommandSec : ICommand
    {
        #region Fields
        TAS2013Entities context = new TAS2013Entities();
        Section _vm = new Section();
        #endregion
        public DeleteCommandSec(Section vm)
        { _vm = vm; }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            Section selectedSec = context.Sections.FirstOrDefault(aa => aa.SectionID == vmd.selectedSec.SectionID);
            if (selectedSec != null)
            {
                List<Emp> emp = new List<Emp>();
                emp = context.Emps.Where(aa => aa.SecID == selectedSec.SectionID).ToList();

                if (emp.Count > 0)
                {
                    PopUp.popUp("Not Deleted", "Please delete Employee before  Section Name Deletion", NotificationType.Warning);
                }
                else
                {
                    context.Sections.Remove(selectedSec);
                   vmd.isAdding = true;
                    vmd.isEnabled = true;
                    try
                    {
                        if (context.SaveChanges() > 0)
                        {
                            vmd.listOfSecs.Remove(vmd.selectedSec);
                            PopUp.popUp("Section", "Section " + vmd.selectedSec.SectionName + " deleted", NotificationType.Warning);
                            vmd.selectedSec = vmd.listOfSecs[0];

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
}
