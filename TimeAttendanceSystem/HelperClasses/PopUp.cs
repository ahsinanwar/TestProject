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
        //why wont this update

        public static void popUp(String title,String Message, NotificationType note)
        {
           
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Background,
                       new Action(delegate
            {
                if(note == NotificationType.Information)
                new ToastPopUp(title, Message, note)
                {
                    Background = new SolidColorBrush(Color.FromRgb(75, 121, 132)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(75, 121, 132)),
                    FontColor = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                }
               .Show();

                if (note == NotificationType.Error)
                 new ToastPopUp(title, Message, note)
                {
                    Background = new SolidColorBrush(Color.FromRgb(139, 0, 0)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(139, 0, 0)),
                    FontColor = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                }
               .Show();

                if (note == NotificationType.Warning)
                    new ToastPopUp(title, Message, note)
                    {
                        Background = new SolidColorBrush(Color.FromRgb(173, 255, 47)),
                        BorderBrush = new SolidColorBrush(Color.FromRgb(173, 255, 47)),
                        FontColor = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                    }
                  .Show();
            }));
        
        }
    }
}
