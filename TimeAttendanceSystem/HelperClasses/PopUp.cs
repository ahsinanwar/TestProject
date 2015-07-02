﻿using System;
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
               .Show();
            }));
        
        }
    }
}
