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
    class SaveCommandSec : ICommand
    {
        #region Fields
        VMSection _vmsection;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandSec(VMSection vm)
        { _vmsection = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmsection.selectedSec != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            if (vmd.isAdding)
            {
                if (vmd.selectedSec.SectionName == "" || vmd.selectedSec.SectionName == null)
                {
                    PopUp.popUp("Empty Value", "Please write a Section Name before saving", NotificationType.Warning);
                }
                else
                {
                    if (context.Sections.Where(aa => aa.SectionName == vmd.selectedSec.SectionName).Count() > 0)
                    {
                        PopUp.popUp("Duplicate Value", "Section Name must be Unique", NotificationType.Warning);
                    }
                    else
                    {
                        context.Sections.Add(vmd.selectedSec);
                        context.SaveChanges();
                        vmd.listOfSecs.Add(vmd.selectedSec);
                        PopUp.popUp("Save", "Section Name is created Successfully", NotificationType.Warning);
                    }
                }
            }
            else
            {
                if (vmd.selectedSec.SectionName == "" || vmd.selectedSec.SectionName == null)
                {
                    PopUp.popUp("Empty Value", "Please write a Section Name before saving", NotificationType.Warning);
                }
                else
                {
                    Section sec = context.Sections.First(aa => aa.SectionID == vmd.selectedSec.SectionID);
                    sec.SectionName = vmd.selectedSec.SectionName;
                    sec.DeptID = vmd.selectedSec.Department.DeptID;
                    vmd.isEnabled = false;
                    vmd.isAdding = false;
                    context.SaveChanges();
                }
            }

        }
        #endregion
    }
}
