using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.BaseClasses
{
    public static class CommanVariables
    {
        private static string _companyName;

        public static string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
    }
}
