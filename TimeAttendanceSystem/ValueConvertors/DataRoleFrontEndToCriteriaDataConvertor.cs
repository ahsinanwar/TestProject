using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class DataRoleFrontEndToCriteriaDataConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType,
           object parameter, CultureInfo culture)
        {
            string FrontEndValue = (string)value;
            if (value == null)
            {
                DateTime dt = DateTime.Now;
                return dt.ToShortDateString();
            }
            DateTime dt1 = (DateTime)value;
            if (dt1.Year == 1)
                dt1 = DateTime.Now;
            return dt1;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            DateTime g = (DateTime)value;

            return g;
        }
    }
}
