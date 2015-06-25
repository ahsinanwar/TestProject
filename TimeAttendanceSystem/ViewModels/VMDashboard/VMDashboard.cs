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
        private double _registeredUsers;
        private ObservableCollection<DailySummary> _presence;
        private ObservableCollection<DailySummary> _dummy;
        TAS2013Entities context;
        public double Absent
        { get { return (Double)_presence[_presence.Count - 1].AbsentEmps; } }
        public double inOffice
        {
            get {
                
                return (Double)_presence[_presence.Count - 1].PresentEmps; }
        }

        public double registeredUsers {
            get { return _registeredUsers; }
            set { }
        
        }
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
              _dummy = new ObservableCollection<DailySummary>(context.DailySummaries.ToList());
              _presence = new ObservableCollection<DailySummary>();
             _dummy.OrderBy(aa => aa.Date);
              if (_dummy.Count > 7)
                for (int i = 0; i < 7; i++)
                      _presence.Add(_dummy[_dummy.Count-7+i]);

              _registeredUsers = context.Emps.Where(aa => aa.Status == true).Count();


              base.OnPropertyChanged("_registeredUsers");
              base.OnPropertyChanged("_presence");
           
        }
        #endregion
    }
}
