using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class EmpMarriedStringToStringConvertor:IValueConverter
    {
        public object Convert(object value, Type targetType,
      object parameter, CultureInfo culture)
        {

            if (value != null)
            {
                string val = (string)value;
                if (val == "0")
                    return "Single";
                if (val == "1")
                    return "Married";
                if (val == "2")
                    return "Engaged";
                   
            }
            return "Single";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string val = (string)value;
                if (val == "Single")
                    return "0";
                if(val=="Married")
                    return "1";
                if (val == "Engaged")
                    return "2";

            }
            return "0";

        }
    }
}
