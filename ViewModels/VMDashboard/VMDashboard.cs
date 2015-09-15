using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.BaseClasses;

using TimeAttendanceSystem.Model;
using WPFPieChart;

namespace TimeAttendanceSystem.ViewModels.VMDashboard
{
    class VMDashboard : ObservableObject
    {
        private ObservableCollection<AssetClass> _value;
        private double _registeredUsers;
        private DateTime _currentDate;
        public ObservableCollection<AssetClass> Value
        {
            get
            {
            return _value;
        
        } set { } }

        private ObservableCollection<DailySummary> _presence;
        private ObservableCollection<DailySummary> _dummy;
        TAS2013Entities context;
        public TimeSpan AverageTimeOut
        { get { return (TimeSpan)_presence[_presence.Count - 1].AvgTimeOut; } set { } }

        public TimeSpan AverageTimeIn
        { get { return (TimeSpan)_presence[_presence.Count - 1].AvgTimeIn; } set {} }

        public double OnLeave
        {
            get {return (Double)_presence[_presence.Count - 1].AbsentEmps; }
            set { }
        
        }
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
        public DateTime currentDate
        {
            get { return _currentDate; }
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
            AverageTimeOut = new TimeSpan();
            AverageTimeIn = new TimeSpan();
            
               context = new TAS2013Entities();
              _dummy = new ObservableCollection<DailySummary>(context.DailySummaries.ToList());
              _presence = new ObservableCollection<DailySummary>();
             _dummy.OrderBy(aa => aa.Date);
              if (_dummy.Count > 7)
                for (int i = 0; i < 7; i++)
                      _presence.Add(_dummy[_dummy.Count-7+i]);

              _registeredUsers = context.Emps.Where(aa => aa.Status == true).Count();
              _currentDate = _dummy.FirstOrDefault().Date.Value;
              _value = CreateInTimeValues();
              base.OnPropertyChanged("_registeredUsers");
              base.OnPropertyChanged("_presence");
              base.OnPropertyChanged("_value");

        }
        #endregion

        #region getInTimeValues
        private ObservableCollection<AssetClass> CreateInTimeValues()
        {

            ObservableCollection<AssetClass> data = new ObservableCollection<AssetClass>();
            DateTime date = DateTime.Now;
            List<DailyTimeMonitor> d= context.DailyTimeMonitors.ToList();

            data.Add(new AssetClass() { Class = d[d.Count - 1].InLabelOne, Fund = (Double)d[d.Count - 1].InLabelOneValue, Total = (Double)d[d.Count - 1].InLabelOneValue, Benchmark = 4.82 });
            data.Add(new AssetClass() { Class = d[d.Count - 1].InLabelTwo, Fund = (Double)d[d.Count - 1].InLabelTwoValue, Total = (Double)d[d.Count - 1].InLabelTwoValue, Benchmark = 4.82 });
            data.Add(new AssetClass() { Class = d[d.Count - 1].InLabelThree, Fund = (Double)d[d.Count - 1].InLabelThreeValue, Total = (Double)d[d.Count - 1].InLabelThreeValue, Benchmark = 4.82 });
            data.Add(new AssetClass() { Class = d[d.Count - 1].InLabelFour, Fund = (Double)d[d.Count - 1].InLabeFourValue, Total = (Double)d[d.Count - 1].InLabeFourValue, Benchmark = 4.82 });
         
            
             return data;
        }
        #endregion
    }
}
