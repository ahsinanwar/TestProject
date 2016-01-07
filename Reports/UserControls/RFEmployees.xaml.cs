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
    public partial class RFEmployees : Window
    {
        public ObservableCollection<Emp> listOfEmps;
        public List<Emp> _selectedEmps;
        public List<Emp> selectedEmps
        {
            get
            {
                return _selectedEmps;
            }
            set
            {
                _selectedEmps = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFEmployees(List<Emp> _selectedEmps)
        {
            InitializeComponent();
            selectedEmps = new List<Emp>();
            listOfEmps = new ObservableCollection<Emp>(context.Emps.ToList());
            lstView_emps.ItemsSource = listOfEmps;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfEmps)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfEmps.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}
           
            this.lstView_emps.SelectAll();


        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_emps.SelectedItems)
            {
                Emp _selectedEmp = item as Emp;
                selectedEmps.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
