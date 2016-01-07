using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using TimeAttendanceSystem.Reports.UserControls;
using TimeAttendanceSystem.ViewModels.VMUser;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
  
    public partial class UserView : Page
    {
        SelectEmpWindow window;
        byte[] binaryImage;
        VMUser vmusers;
       
        public UserView()
        {

            try
            {
                InitializeComponent();

                vmusers = new VMUser();
                ListBoxSec.ItemsSource = vmusers.listOfSections;
                this.DataContext = vmusers;
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
                vmusers.selectedUser.Emp = window._selectedEmp;
                vmusers.selectedUser.EmpID = window._selectedEmp.EmpID;
                //vmlvapps.selectedEmpAndLvApp.Employee = window._selectedEmp;
                //vmlvapps.selectedEmpAndLvApp.LvApp.EmpID = window._selectedEmp.EmpID;
            }
        }
        private void btn_imageSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
            {
                // set image to image box from the path
                //_image.Source = new BitmapImage(new Uri(open.FileName));


                // read image from the path and convert to stream to be stored later
                Stream stream = File.OpenRead(open.FileName);
                binaryImage = new byte[stream.Length];
                stream.Read(binaryImage, 0, (int)stream.Length);
                vmusers._selectedUser.Emp.EmpPhoto = new Model.EmpPhoto();
                vmusers.selectedUser.Emp.EmpPhoto.EmpID = vmusers._selectedUser.Emp.EmpID;
                vmusers.selectedUser.Emp.EmpPhoto.EmpPic = binaryImage;
                vmusers.raiseEmpChange();
            }
        }

       

        #region -- Section filter --
        public List<TimeAttendanceSystem.Model.Section> selectedSecs;
        RFSections windowSec;
        private void btnAddSec_Click(object sender, RoutedEventArgs e)
        {
            selectedSecs = new List<Model.Section>();
            ListBoxSec.ItemsSource = new List<Section>();
            windowSec = new RFSections(selectedSecs);
            if ((bool)windowSec.ShowDialog())
            {
                selectedSecs.Clear();
                selectedSecs = windowSec.selectedSecs;
            }
           vmusers.listOfSections = new ObservableCollection<Model.Section>(selectedSecs);
            
           
                      
        }

        private void btnClearSec_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSec.ItemsSource = new List<Section>();
            vmusers.listOfSections = new ObservableCollection<Model.Section>();
            if(selectedSecs!=null)
            selectedSecs.Clear();
        }
        #endregion
    }
}
