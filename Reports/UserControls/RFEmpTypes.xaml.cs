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
    public partial class RFEmpTypes : Window
    {
        public ObservableCollection<EmpType> listOfEmpTypes;
        public List<EmpType> _selectedEmpTypes;
        public List<EmpType> selectedEmpTypes
        {
            get
            {
                return _selectedEmpTypes;
            }
            set
            {
                _selectedEmpTypes = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFEmpTypes(List<EmpType> _selectedEmpTypes)
        {
            InitializeComponent();
            selectedEmpTypes = new List<EmpType>();
            listOfEmpTypes = new ObservableCollection<EmpType>(context.EmpTypes.ToList());
            lstView_emptypes.ItemsSource = listOfEmpTypes;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfEmpTypes)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfEmpTypes.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}

            this.lstView_emptypes.SelectAll();


        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_emptypes.SelectedItems)
            {
                EmpType _selectedEmp = item as EmpType;
                selectedEmpTypes.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
