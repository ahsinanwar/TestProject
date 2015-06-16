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
using TimeAttendanceSystem.Model;


namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for SelectEmpWindow.xaml
    /// </summary>
    public partial class SelectEmpWindow : Window
    {
        public ObservableCollection<Emp> listOfEmps;
        TAS2013Entities context = new TAS2013Entities();
        public SelectEmpWindow()
        {
            InitializeComponent();
            listOfEmps = new ObservableCollection<Emp>(context.Emps.ToList());
            lstView_emps.ItemsSource = listOfEmps;
        }
    }
}
