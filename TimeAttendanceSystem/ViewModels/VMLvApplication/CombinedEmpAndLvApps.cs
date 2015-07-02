using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.ViewModels.VMLvApplication
{
    class CombinedEmpAndLvApps : ObservableObject
    {
        private Emp _employee;
        private LvApplication _lvapp;
        private LvType _lvtype;
       

        public CombinedEmpAndLvApps(Emp emp, LvApplication value,LvType lvType)
        {
            
            this.Employee = emp;
            this.LvApp = value;
            this.LvType = lvType;
        }

        public CombinedEmpAndLvApps()
        {
            this.Employee = new Emp();
            this.LvApp = new LvApplication();
            this.LvApp.IsHalf = true;
            this.LvApp.FromDate = DateTime.Now;
            this.LvApp.ToDate = DateTime.Now;
            this.LvType = new LvType();
        }
       
        public Emp Employee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
                base.OnPropertyChanged("Employee");

            }

        }
        public LvApplication LvApp
        {
            get
            {
                return _lvapp;
            }
            set
            {
                _lvapp = value;
                base.OnPropertyChanged("Lvapp");

            }

        }
        public LvType LvType
        {
            get
            {
                return _lvtype;
            }
            set
            {
                _lvtype = value;
                base.OnPropertyChanged("LvType");

            }

        }
       
       
    }
}
