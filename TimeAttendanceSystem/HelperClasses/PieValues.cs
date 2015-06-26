using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.HelperClasses
{
    class PieValues
    {
        public double value {get;set; }
        public String label { get; set; }
        public PieValues(double v, String l)
        {
            value = v;
            label = l;
        }
    }
}
