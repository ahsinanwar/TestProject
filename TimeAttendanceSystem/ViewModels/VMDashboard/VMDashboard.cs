using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMDashboard
{
    class VMDashboard : ObservableObject
    {
        private ObservableCollection<DailySummary> _presence;
        TAS2013Entities context;
        public ObservableCollection<DailySummary> presence { get{
            return _presence;
        
        } set{
            _presence = value;
            OnPropertyChanged("presence");
        } }
        #region constructor
        public VMDashboard()
        {
            context = new TAS2013Entities();
            _presence = new ObservableCollection<DailySummary>(context.DailySummaries);
            OnPropertyChanged("_presence");
        }
        #endregion
    }
}
