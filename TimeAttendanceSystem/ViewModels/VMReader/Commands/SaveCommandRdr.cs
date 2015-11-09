using Mantin.Controls.Wpf.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.HelperClasses;
using TimeAttendanceSystem.Model;


namespace TimeAttendanceSystem.ViewModels.VMReader.Commands
{
    class SaveCommandRdr : ICommand
    {
        #region Fields
        VMReader _vmreader;
        TAS2013Entities context = new TAS2013Entities();
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandRdr(VMReader vm)
        { _vmreader = vm; }
        public bool CanExecute(object parameter)
        {
            return (_vmreader.selectedRdr != null);
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMReader vmd = (VMReader)parameter;
            if (vmd.isAdding)
            {
                Reader dummy = vmd.selectedRdr;
                dummy.RdrTypeID = vmd.selectedRdr.ReaderType.RdrTypeID;
                dummy.RdrDutyID = vmd.selectedRdr.RdrDutyCode.RdrDutyID;
                dummy.LocID = vmd.selectedRdr.Location.LocID;
                dummy.RdrDutyCode = null;
                dummy.ReaderType = null;
                dummy.Location = null;







                context.Readers.Add(dummy);

                if (context.SaveChanges() > 0)
                {
                    vmd.selectedRdr.RdrDutyCode = context.RdrDutyCodes.FirstOrDefault(aa => aa.RdrDutyID == vmd.selectedRdr.RdrDutyID);
                    vmd.selectedRdr.ReaderType = context.ReaderTypes.FirstOrDefault(aa => aa.RdrTypeID == vmd.selectedRdr.RdrTypeID);
                    vmd.selectedRdr.Location = context.Locations.FirstOrDefault(aa => aa.LocID == vmd.selectedRdr.LocID);

                    PopUp.popUp("Reader", "Reader " + vmd.selectedRdr.RdrName + " is created Successfully",                        NotificationType.Information);
                    vmd.listOfRdrs.Add(vmd.selectedRdr);
                }
                

            }
            else
            {
                Reader rdr = context.Readers.First(aa => aa.RdrID == vmd.selectedRdr.RdrID);

                rdr.RdrName = vmd.selectedRdr.RdrName;
                rdr.IpAdd = vmd.selectedRdr.IpAdd;
                rdr.IpPort = vmd.selectedRdr.IpPort;
                rdr.Status = vmd.selectedRdr.Status;
                rdr.RdrDutyID = vmd.selectedRdr.RdrDutyCode.RdrDutyID;
                rdr.RdrTypeID = vmd.selectedRdr.ReaderType.RdrTypeID;
                rdr.LocID = vmd.selectedRdr.Location.LocID;

                if (context.SaveChanges() > 0)
                {
                    PopUp.popUp("Reader", "Reader " + vmd.selectedRdr.RdrName + " is edited and saved Successfully", NotificationType.Information);
                }
            }
            vmd.isEnabled = false;
            vmd.isAdding = false;
        }
        #endregion
    }
}
