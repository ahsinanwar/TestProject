﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class SaveCommandLoc :ICommand
    {
         #region Fields
        VMLocation _vmloc;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandLoc(VMLocation vm)
        { _vmloc = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmloc.selectedLoc != null);
            return true;
                }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;
      
        public void Execute(object parameter)
        {
            VMLocation vmd = (VMLocation)parameter;
           if (vmd.isAdding)
           {
               context.Locations.Add(vmd.selectedLoc);
               context.SaveChanges();
               vmd.listOfLocs.Add(vmd.selectedLoc);

           }
           else {
               Location loc = context.Locations.First(aa => aa.LocID == vmd.selectedLoc.LocID);
               loc.LocName = vmd.selectedLoc.LocName;
               vmd.isEnabled = false;
               vmd.isAdding = false;
               context.SaveChanges();
                }
         
        }
        #endregion
    }
}
