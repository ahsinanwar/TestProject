using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMSection.Commands
{
    class SaveCommand : ICommand
    {
        #region Fields
        VMSection _vmsection;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommand(VMSection vm)
        { _vmsection = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmsection.selectedSec != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMSection vmd = (VMSection)parameter;
            if (vmd.isAdding)
            {
                context.Sections.Add(vmd.selectedSec);
                context.SaveChanges();
                vmd.listOfSecs.Add(vmd.selectedSec);

            }
            else
            {
                Section sec = context.Sections.First(aa => aa.SectionID == vmd.selectedSec.SectionID);
                sec.SectionName = vmd.selectedSec.SectionName;
                vmd.isEnabled = false;
                vmd.isAdding = false;
                context.SaveChanges();
            }

        }
        #endregion
    }
}
