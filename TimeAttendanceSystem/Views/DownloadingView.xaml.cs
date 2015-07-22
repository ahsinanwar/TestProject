using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
    /// Interaction logic for DownloadingView.xaml
    /// </summary>
    public partial class DownloadingView : Window
    {
        private Model.ClientInfo clientinfo2;
        private Model.ClientMAC cm;
        public bool IsAvailable { get; set; }
        public DownloadingView()
        {
            InitializeComponent();
           
    
        }
     

        public DownloadingView(Model.ClientInfo clientinfo2, Model.ClientMAC cm)
        {
            InitializeComponent();
            this.clientinfo2 = clientinfo2;
            this.cm = cm;
        }

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private void SendDataToServer(ClientInfo clientinfo2, ClientMAC cm)
        {

            using (WebClient client = new WebClient())
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("ClientName", clientinfo2.ClientName);
                reqparm.Add("LicenseType", clientinfo2.LiscenceTypeID + "");
                reqparm.Add("MAC", cm.MACAddress + "");
                reqparm.Add("isActive", "" + clientinfo2.isActive);
                reqparm.Add("createdby", clientinfo2.CreatedBy);

                byte[] responsebytes = client.UploadValues("http://localhost:3000/registration", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(responsebytes);
            }
        }
    }
}
