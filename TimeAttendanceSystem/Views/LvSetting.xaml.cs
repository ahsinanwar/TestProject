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
using TimeAttendanceSystem.ViewModels.VMLvApplication;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for LvSetting.xaml
    /// </summary>
    public partial class LvSetting : Page
    {
        SelectEmpWindow window;
        VMLvApplication vmlvapps;
        public LvSetting()
        {
            try
            {
                InitializeComponent();
                vmlvapps = new VMLvApplication();
                this.DataContext = vmlvapps;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
        }
        private void btn_empView_Click(object sender, RoutedEventArgs e)
        {
            window = new SelectEmpWindow();


            if ((bool)window.ShowDialog())
            {
                Console.WriteLine(window._selectedEmp);
                txtEmpID.Text = window._selectedEmp.EmpID.ToString();
                vmlvapps.selectedEmpAndLvApp.Employee = window._selectedEmp;
                vmlvapps.selectedEmpAndLvApp.LvApp.EmpID = window._selectedEmp.EmpID;
            }
        }  
    }
}
