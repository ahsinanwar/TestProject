using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mantin.Controls.Wpf.Notification;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Media;
namespace TimeAttendanceSystem.HelperClasses
{
    public static class PopUp
    {


        public static void popUp(String title,String Message, NotificationType note)
        {
           
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                       new Action(delegate
            {

                new ToastPopUp(title, Message,note)
                {
                    Background = new LinearGradientBrush(Color.FromArgb(255, 4, 253, 82), Color.FromArgb(255, 10, 13, 248), 90),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    FontColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
                }.Show();
            }));
        
        }
    }
}
