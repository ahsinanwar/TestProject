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
using System.Windows.Shapes;
using TimeAttendanceSystem.ViewModels.VMAttJobCard;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for AttEditJobCard.xaml
    /// </summary>
    public partial class AttEditJobCard : Page
    {
        VMAttJobCard vmAttJobCard;
        public AttEditJobCard()
        {
            InitializeComponent();
            rbShift.IsChecked = true;
            vmAttJobCard = new VMAttJobCard();
            this.DataContext = vmAttJobCard;
        }
    }
}
