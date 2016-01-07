using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeAttendanceSystem.Model;

namespace TASDownloadService.AttProcessDaily
{
    public static class ProcessSupportFunc
    {
        #region -- Helper Function--

        public static string ReturnDayOfWeek(DayOfWeek dayOfWeek)
        {
            string _DayName = "";
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    _DayName = "Monday";
                    break;
                case DayOfWeek.Tuesday:
                    _DayName = "Tuesday";
                    break;
                case DayOfWeek.Wednesday:
                    _DayName = "Wednesday";
                    break;
                case DayOfWeek.Thursday:
                    _DayName = "Thursday";
                    break;
                case DayOfWeek.Friday:
                    _DayName = "Friday";
                    break;
                case DayOfWeek.Saturday:
                    _DayName = "Saturday";
                    break;
                case DayOfWeek.Sunday:
                    _DayName = "Sunday";
                    break;
            }
            return _DayName;
        }

        public static TimeSpan CalculateShiftEndTime(Shift shift, DayOfWeek dayOfWeek)
        {
            Int16 workMins = 0;
            try
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        workMins = shift.MonMin;
                        break;
                    case DayOfWeek.Tuesday:
                        workMins = shift.TueMin;
                        break;
                    case DayOfWeek.Wednesday:
                        workMins = shift.WedMin;
                        break;
                    case DayOfWeek.Thursday:
                        workMins = shift.ThuMin;
                        break;
                    case DayOfWeek.Friday:
                        workMins = shift.FriMin;
                        break;
                    case DayOfWeek.Saturday:
                        workMins = shift.SatMin;
                        break;
                    case DayOfWeek.Sunday:
                        workMins = shift.SunMin;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return shift.StartTime + (new TimeSpan(0, workMins, 0));
        }

        public static Int16 CalculateShiftMinutes(Shift shift, DayOfWeek dayOfWeek)
        {
            Int16 workMins = 0;
            try
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        workMins = shift.MonMin;
                        break;
                    case DayOfWeek.Tuesday:
                        workMins = shift.TueMin;
                        break;
                    case DayOfWeek.Wednesday:
                        workMins = shift.WedMin;
                        break;
                    case DayOfWeek.Thursday:
                        workMins = shift.ThuMin;
                        break;
                    case DayOfWeek.Friday:
                        workMins = shift.FriMin;
                        break;
                    case DayOfWeek.Saturday:
                        workMins = shift.SatMin;
                        break;
                    case DayOfWeek.Sunday:
                        workMins = shift.SunMin;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return workMins;
        }

        public static DateTime CalculateShiftEndTime(Shift shift, DateTime _AttDate, TimeSpan _DutyTime)
        {
            Int16 workMins = 0;
            try
            {
                switch (_AttDate.Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        workMins = shift.MonMin;
                        break;
                    case DayOfWeek.Tuesday:
                        workMins = shift.TueMin;
                        break;
                    case DayOfWeek.Wednesday:
                        workMins = shift.WedMin;
                        break;
                    case DayOfWeek.Thursday:
                        workMins = shift.ThuMin;
                        break;
                    case DayOfWeek.Friday:
                        workMins = shift.FriMin;
                        break;
                    case DayOfWeek.Saturday:
                        workMins = shift.SatMin;
                        break;
                    case DayOfWeek.Sunday:
                        workMins = shift.SunMin;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            DateTime _datetime = new DateTime();
            TimeSpan _Time = new TimeSpan(0, workMins, 0);
            _datetime = _AttDate.Date.Add(_DutyTime);
            _datetime = _datetime.Add(_Time);
            return _datetime;
        }

        #endregion

        internal static short? CalculateShiftBreakMinutes(Shift shift, DayOfWeek dayOfWeek)
        {
            Int16 breakMins = 0;
            try
            {
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        if (shift.MonMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Tuesday:
                        if (shift.TueMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Wednesday:
                        if (shift.WedMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Thursday:
                        if (shift.ThuMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Friday:
                        if (shift.FriMin > 0)
                            breakMins = (short)shift.FriBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Saturday:
                        if (shift.SatMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                    case DayOfWeek.Sunday:
                        if (shift.SunMin > 0)
                            breakMins = (short)shift.SatBreakMin;
                        else
                            breakMins = 0;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return breakMins;
        }
    }
}
