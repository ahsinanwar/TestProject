using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class CRLFtoBlankConvertor : IValueConverter
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public CRLFtoBlankConvertor() { }
        #endregion

       
        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Desgination = (string)value;
            Desgination = Desgination.Replace("\r\n", string.Empty);
            return Desgination;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
            
        }
        #endregion
    }
}
