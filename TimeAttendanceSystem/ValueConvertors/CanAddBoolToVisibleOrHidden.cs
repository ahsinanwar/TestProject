using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TimeAttendanceSystem.ValueConvertors
{
    class CanAddBoolToVisibleOrHidden : IValueConverter
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public CanAddBoolToVisibleOrHidden() { }
        #endregion

        #region Properties
        public bool Collapse { get; set; }
        public string Permission { get; set; }
        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Permission = (string)parameter;
            bool bValue = false;
            switch (Permission)
            {
                case "Add": bValue = (bool)GlobalClasses.Global.user.CanAdd;
                    break;
                case "Edit": bValue = (bool)GlobalClasses.Global.user.CanEdit;
                    break;
                case "Delete": bValue = (bool)GlobalClasses.Global.user.CanDelete;
                    break;
                case "MEditAtt": bValue = (bool)GlobalClasses.Global.user.MEditAtt;
                    break;
                case "MHR": bValue = (bool)GlobalClasses.Global.user.MHR;
                    break;
                case "MLeave": bValue = (bool)GlobalClasses.Global.user.MLeave;
                    break;
                
            
            }
            //bool bValue = (bool)GlobalClasses.Global.user.CanAdd;
            if (bValue)
            {
                return Visibility.Visible;
            }
            else
            {
                if (Collapse)
                    return Visibility.Hidden;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
        #endregion
    }
}
