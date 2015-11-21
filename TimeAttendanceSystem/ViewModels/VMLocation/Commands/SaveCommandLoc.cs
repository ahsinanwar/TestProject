using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLocation.Commands
{
    class SaveCommandLoc :ICommand
    {
         #region Fields
        VMLocation _vmloc;
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
            using (var ctx = new TAS2013Entities())
            {
                VMLocation vmd = (VMLocation)parameter;
                if (vmd.isAdding)
                    // if a new object is being added
                {
                    // validate empty
                    if (vmd.selectedLoc.LocName == "" || vmd.selectedLoc.LocName == null)
                    {
                        PopUp.popUp("Location", "Please write Location before saving", NotificationType.Warning);
                    }

                    else
                    {
                        // validate duplicate
                        if (ctx.Locations.Where(aa => aa.LocName == vmd.selectedLoc.LocName).Count() > 0)
                        {
                            PopUp.popUp("Location", "Location name already been created", NotificationType.Warning);
                        }
                        else
                        {
                            // if everything is ok, save new object to database


                            Location dummy = vmd.selectedLoc;
                            dummy.City = null;
                            ctx.Locations.Add(vmd.selectedLoc);
                            if (ctx.SaveChanges() > 0)
                            {
                                PopUp.popUp("Location", "Location is Created", NotificationType.Warning);
                            }
                            else
                            {
                                PopUp.popUp("Location", "An error occured while saving.", NotificationType.Warning);
                            }

                            vmd.selectedLoc.City = ctx.Cities.Where(aa => aa.CityID == vmd.selectedLoc.CityID).FirstOrDefault();

                            vmd.listOfLocs.Add(vmd.selectedLoc);
                        }
                    }
                }
                else
                    // if old object is being edited
                {
                    Location loc = ctx.Locations.First(aa => aa.LocID == vmd.selectedLoc.LocID);
                    loc.LocName = vmd.selectedLoc.LocName;
                    vmd.isEnabled = false;
                    vmd.isAdding = false;
                    ctx.SaveChanges();
                    PopUp.popUp("Location", "Location is Created", NotificationType.Information);
                }
                vmd.isAdding = false;
                vmd.isEnabled = false;
            }
         
        }
        #endregion
    }
}
