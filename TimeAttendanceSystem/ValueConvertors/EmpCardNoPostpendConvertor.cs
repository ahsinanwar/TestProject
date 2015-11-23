using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class EmpCardNoPostpendConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {

            if (value == "00000000")
            {
                return "00000000";
            }
            else
            {
                return (string)value;
            }
              
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            string cardnovalue = (string)value;
            if (cardnovalue.Count() > 8)
            {
                cardnovalue = cardnovalue.Substring(1, cardnovalue.Count() - 1);
                return cardnovalue;
            }
            else 
            
            {
                cardnovalue = "0" + cardnovalue;
                return cardnovalue;
            }
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
