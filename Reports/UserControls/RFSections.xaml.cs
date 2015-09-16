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
    public partial class RFSections : Window
    {
        public ObservableCollection<TimeAttendanceSystem.Model.Section> listOfSecs;
        public List<TimeAttendanceSystem.Model.Section> _selectedSecs;
        public List<TimeAttendanceSystem.Model.Section> selectedSecs
        {
            get
            {
                return _selectedSecs;
            }
            set
            {
                _selectedSecs = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFSections(List<TimeAttendanceSystem.Model.Section> _selectedSecs)
        {
            InitializeComponent();
            selectedSecs = new List<TimeAttendanceSystem.Model.Section>();
            listOfSecs = new ObservableCollection<TimeAttendanceSystem.Model.Section>(context.Sections.ToList());
            lstView_emps.ItemsSource = listOfSecs;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfSecs)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfSecs.FirstOrDefault();
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
                TimeAttendanceSystem.Model.Section _selectedEmp = item as TimeAttendanceSystem.Model.Section;
                selectedSecs.Add(_selectedEmp);
            }

        }

        private void lstView_emps_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
