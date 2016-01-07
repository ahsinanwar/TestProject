using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.HelperClasses
{
    public static class MyEnums
    {
        public enum FormName
        {
            Employee = 1,
            Shift,
            Reader,
            Leave,
            ShortLeave,
            LeaveQuota,
            EditAttendance,
            Designation,
            Department,
            User,
            LogIn,
            LogOut,Category,
            City,
            Division,
            LeaveApplication,
            Section,
            EmpType,
            Holiday,Location

        }
        public enum Operation
        {
            Add = 1,
            Edit,
            View,
            Delete,
            LogIn,
            LogOut
        }
    }

}
