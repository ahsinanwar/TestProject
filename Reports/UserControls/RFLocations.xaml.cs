using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Telerik.Windows.Controls.GridView;
using TimeAttendanceSystem.BaseClasses;
using TimeAttendanceSystem.Model;
using Telerik.Windows.Controls;

namespace TimeAttendanceSystem.Reports.UserControls
{
    /// <summary>
    /// Interaction logic for SelectEmpWindow.xaml
    /// </summary>
    public partial class RFLocations : Window
    {
        public ObservableCollection<Location> listOfLocs;
        public List<Location> _selectedLocs;
        public List<Location> selectedLocs
        {
            get
            {
                return _selectedLocs;
            }
            set
            {
                _selectedLocs = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFLocations(List<Location> _selectedLocs)
        {
            InitializeComponent();
            selectedLocs = new List<Location>();
            listOfLocs = new ObservableCollection<Location>(context.Locations.ToList());
            lstView_locs.ItemsSource = listOfLocs;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfLocs)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfLocs.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}

            this.lstView_locs.SelectAll();


        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_locs.SelectedItems)
            {
                Location _selectedEmp = item as Location;
                selectedLocs.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
