using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using TimeAttendanceSystem.ViewModels.VMEmployee;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Page
    {
        private BackgroundWorker worker = new BackgroundWorker();
        VMEmployee vmemps;
        byte[] binaryImage;
        
        public EmployeeView()
        { 
            InitializeComponent();

            vmemps = new VMEmployee();
            this.DataContext = vmemps;
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

                vmemps.dummyEmp.EmpPhoto = new Model.EmpPhoto();

                vmemps.dummyEmp.EmpPhoto.EmpID = vmemps.dummyEmp.EmpID;
                vmemps.dummyEmp.EmpPhoto.EmpPic = binaryImage;

                vmemps.raiseEmpChange();
            }
        }

        

       
      
        
    }
}
