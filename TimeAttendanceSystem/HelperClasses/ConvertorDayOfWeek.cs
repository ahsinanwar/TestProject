using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAttendanceSystem.HelperClasses
{
    public static class ConvertorDayOfWeek

    {

       


        public static DayOfWeek ReturnDayOfWeek(string dayOfWeek)
        {
            DayOfWeek _DayName = new DayOfWeek();
            switch (dayOfWeek)
            {
                case "Monday":
                    _DayName =DayOfWeek.Monday ;
                    break;
                case "Tuesday":
                    _DayName = DayOfWeek.Tuesday;
                    break;
                case "Wednesday":
                    _DayName = DayOfWeek.Wednesday;
                    break;
                case  "Thursday":
                    _DayName = DayOfWeek.Thursday;
                    break;
                case "Friday":
                    _DayName = DayOfWeek.Friday;
                    break;
                case "Saturday":
                    _DayName = DayOfWeek.Saturday;
                    break;
                case "Sunday":
                    _DayName = DayOfWeek.Sunday;
                    break;
            }
            return _DayName;
        }
    }
}
