﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeAttendanceSystem.ViewModels.VMDatabase;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for DatabaseSettings.xaml
    /// </summary>
    public partial class DatabaseSettings : Page
    {
        public DatabaseSettings()
        {
            try
            {
                InitializeComponent();
                this.DataContext = new VMDataBase();
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
    }
}
