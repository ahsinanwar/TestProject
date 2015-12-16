using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class minuteToHourMinuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {      
            if (value != null)
            {
                int duration = (Int16)value;
                if (duration < 0)
                    return "None";
                int hours = duration / 60;
                int minutes = duration - (hours * 60);
                // Console.WriteLine(hours+":"+minutes);
                String min = minutes + "";
                if (minutes < 10)
                    min = "0" + minutes;

                return hours + ":" + min;
            }
            return "None";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            
            return "FUCEDK";
        }
    }
}
