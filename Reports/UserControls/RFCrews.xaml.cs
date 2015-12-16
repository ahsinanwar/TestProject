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
    public partial class RFCrews : Window
    {
        public ObservableCollection<Crew> listOfCrews;
        public List<Crew> _selectedCrews;
        public List<Crew> selectedCrews
        {
            get
            {
                return _selectedCrews;
            }
            set
            {
                _selectedCrews = value;
            }
        }
        TAS2013Entities context = new TAS2013Entities();
        public RFCrews(List<Crew> _selectedCrews)
        {
            InitializeComponent();
            selectedCrews = new List<Crew>();
            listOfCrews = new ObservableCollection<Crew>(context.Crews.ToList());
            lstView_crews.ItemsSource = listOfCrews;

            //lstView_emps.Items.Clear();


            //foreach (var item in listOfCrews)
            //{
            //    //lstView_emps.Items.Add(item);
            //    Emp emp = listOfCrews.FirstOrDefault();
            //    this.lstView_emps.SelectedItem = emp;
            //}
            

        }

        private void selectbutton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            //lstView_emps
            foreach (var item in lstView_crews.SelectedItems)
            {
                Crew _selectedCrews = item as Crew;
                selectedCrews.Add(_selectedCrews);
            }

        }

        private void lstView_crews_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            //Emp employee = lstView_emps.SelectedItem as Emp;
            //selectedEmp = employee;
        }

    }
}
