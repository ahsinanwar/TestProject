using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDashboard;

namespace TimeAttendanceSystem.HelperClasses
{
    public class StackedBarObject
    {
        private DateTime _XValue;
        private double _Value1;
        private double _Value2;
        private double _Value3;
        private double _Value4;
        public DateTime XValue
        {
            get
            {
                return _XValue;
            }
            set
            {
                _XValue = value;
            }
        }

        public double Value1
        {
            get
            {
                return _Value1;
            }
            set
            {
                _Value1 = value;
            }
        }

        public double Value2
        {
            get
            {
                return _Value2;
            }
            set
            {
                _Value2 = value;
            }
        }
        public double Value3
        {
            get
            {
                return _Value3;
            }
            set
            {
                _Value3 = value;
            }
        }
        public double Value4
        {
            get
            {
                return _Value4;
            }
            set
            {
                _Value4 = value;
            }
        }
        public StackedBarObject(DateTime _XValue, double _Value1, double _Value2,double _Value3,double _Value4)
        {
            this.XValue = _XValue;
            this.Value1 = _Value1;
            this.Value2 = _Value2;
            this.Value3 = _Value3;
            this.Value4 = _Value4;
        }

        
    }
}
