using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class DateTimeToTimeSpan : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
           
            if (value != null)
            {
                DateTime dt = new DateTime(2012, 01, 01);
                dt = dt + (TimeSpan)value;

                return dt;
            }
            DateTime g= new DateTime();
            return g;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            DateTime g = (DateTime)value;
            TimeSpan gg=g.TimeOfDay;
            return gg;
        }
    }
}
