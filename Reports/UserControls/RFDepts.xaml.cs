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
    public partial class RFDepts : Window
    {
        public ObservableCollection<Department> listOfDepts;
        public List<Department> _selectedDepts;
        public List<Department> selectedDepts
        {
            get
            {
                return _selectedDepts;
            }
            set
            {
                _selectedDepts = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFDepts(List<Department> _selectedDepts)
        {
            InitializeComponent();
            selectedDepts = new List<Department>();
            listOfDepts = new ObservableCollection<Department>(context.Departments.ToList());
            lstView_depts.ItemsSource = listOfDepts;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfDepts)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfDepts.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}

            this.lstView_depts.SelectAll();


        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_depts.SelectedItems)
            {
                Department _selectedEmp = item as Department;
                selectedDepts.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
