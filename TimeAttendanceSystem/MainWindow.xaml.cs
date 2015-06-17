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
using TimeAttendanceSystem.Views;

namespace TimeAttendanceSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            WindowState = WindowState.Maximized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AttEditView aa = new AttEditView();
            aa.Show();
        }

        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string selectedMenuText = ((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString();
            switch (selectedMenuText)
            {
                //case "Sections":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                case "Departments":
                    _mainFrame.Navigate(new DepartmentView());
                    break;
                //case "Division":
                //    _mainFrame.Navigate(new DivisionView());
                //    break;
                //case "Designation":
                //    _mainFrame.Navigate(new DesignationView());
                //    break;
                //case "Crew":
                //    _mainFrame.Navigate(new CrewView());
                //    break;
                case "City":
                    _mainFrame.Navigate(new CityView());
                    break;
                //case "Location":
                //    _mainFrame.Navigate(new LocationView());
                //    break;
                //case "Category":
                //    _mainFrame.Navigate(new CategoryView());
                //    break;
                //case "Emp Types":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Holidays":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "User":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Application":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Short Leave":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Quota":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Daily":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
                //case "Monthly":
                //    _mainFrame.Navigate(new SectionView());
                //    break;
            }
        }
    }
}
