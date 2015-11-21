using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class EmpGenderByteToStringConvetor : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
          
             if (value != null)
            {
                byte val = (byte)value;
                if (val == 1)
                    return  "Female";
                else
                    return  "Male";
            }
             return  "Male";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null)
            {
               string val = (string)value;
                if (val == "Female")
                    return 1;
                else
                    return 0;

            }
            return 0;

        }
    }
}
