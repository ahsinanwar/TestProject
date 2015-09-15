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
    public partial class RFShifts : Window
    {
        public ObservableCollection<Shift> listOfShifts;
        public List<Shift> _selectedShifts;
        public List<Shift> selectedShifts
        {
            get
            {
                return _selectedShifts;
            }
            set
            {
                _selectedShifts = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFShifts(List<Shift> _selectedShifts)
        {
            InitializeComponent();
            selectedShifts = new List<Shift>();
            listOfShifts = new ObservableCollection<Shift>(context.Shifts.ToList());
            lstView_shifts.ItemsSource = listOfShifts;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfShifts)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfShifts.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}

            this.lstView_shifts.SelectAll();


        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_shifts.SelectedItems)
            {
                Shift _selectedEmp = item as Shift;
                selectedShifts.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
