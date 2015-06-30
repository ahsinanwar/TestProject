using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMShortLv
{
    class CombinedEmpAndShortLvcs: ObservableObject
    {
        public Emp _employee{get; set;}
        public LvShort _lvShort { get; set;}
        public  Emp Employee{
            get {
                return _employee;
            }
            set {
                _employee = value;
                base.OnPropertyChanged("Employee");
            
            }
    
    }
    public LvShort Lvshort{
        get {
            return _lvShort;
        }
        set {
            _lvShort = value;
            base.OnPropertyChanged("Lvshort");
          
        }
    
    }
        public CombinedEmpAndShortLvcs(Emp employee, LvShort lvshort)
        {
            _employee = employee;
            _lvShort = lvshort;
        
        }
        public CombinedEmpAndShortLvcs()
        {
            this.Employee = new Emp();
            this.Lvshort = new LvShort();
        
        }
    }
}
