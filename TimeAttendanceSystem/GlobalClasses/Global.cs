using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.GlobalClasses
{
    public static class Global
{
        private static User _user;
       
        public static User user
    {
        get
        {
            return ( _user );
        }
        set
        {
            _user = value;
        }
    }
       
   
}
}
