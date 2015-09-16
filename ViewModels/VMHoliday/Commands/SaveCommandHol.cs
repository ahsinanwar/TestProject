using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMHoliday.Commands
{
    class SaveCommandHol: ICommand
    {
         #region Fields
        VMHoliday _vmemp;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandHol(VMHoliday vm)
        { _vmemp = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmemp.selectedHoliday != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMHoliday vmd = (VMHoliday)parameter;
            if (vmd.isAdding)
            {
                if (vmd._selectedHoliday.HolDesc == "" || vmd._selectedHoliday.HolDesc == null)
                {
                    PopUp.popUp("Empty Value", "Please write Holiday Name before saving", NotificationType.Warning);
                }
                else
                {
                    if (context.Holidays.Where(aa => aa.HolDesc == vmd.selectedHoliday.HolDesc).Count() > 0)
                    {
                        PopUp.popUp("Duplicate", "Holiday Name must be Unique", NotificationType.Warning);
                    }
                    else
                    {
                        context.Holidays.Add(vmd.selectedHoliday);
                        context.SaveChanges();
                        vmd.listOfHolidays.Add(vmd.selectedHoliday);
                        PopUp.popUp("Save", "Holiday is created Successfully", NotificationType.Warning);
                  
                    }
                }
            }
            //else
            //{
            //    if (vmd.selectedHoliday.HolDate.
            //    {
            //        PopUp.popUp("Empty Value", "Please write a Section Name before saving", NotificationType.Warning);
            //    }
                    else
                    {
                        Holiday hol = context.Holidays.First(aa => aa.HolID == vmd.selectedHoliday.HolID);
                        hol.HolDate = vmd.selectedHoliday.HolDate;
                        vmd.isEnabled = false;
                        vmd.isAdding = false;
                        context.SaveChanges();
                    }
                

        }
        #endregion
    }
}
