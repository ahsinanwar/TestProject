using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Net.NetworkInformation;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        TAS2013Entities context = new TAS2013Entities();
        public RegistrationView()
        {
            
            //add code for sql database checking if its valid jump to the mainWindow
            
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow win2 = new MainWindow();

            String d = this.firsthalf.Text;
            String mac=GetMacAddress();
            var url = "http://localhost:3000/registration/" + d + "/" + GetMacAddress();
            var syncClient = new WebClient();
            String content = syncClient.DownloadString(url);
            Package df = JsonConvert.DeserializeObject<Package>(content);
            if (df.valid && df.key==d && df.mac==GetMacAddress())
            {
                win2.Show();
                this.Close();
            }
            if (df.key!=d)
            {
                Console.WriteLine("Key is not valid");
            }
            if(df.key==d && df.mac!=GetMacAddress())
            {
                Console.WriteLine("This key is not licensed to you");
             }
            
        }
        private string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            
            long maxSpeed = -1;
            string macAddress = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                  
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }
      
    }
    public partial class Package {
        [JsonProperty("_id")]
        public String id { get; set; }
        [JsonProperty("mac")]
        public String mac { get; set; }
        [JsonProperty("valid")]
        public bool valid { get; set; }
        [JsonProperty("type")]
        public String type { get; set; }
        [JsonProperty("uptill")]
        public DateTime uptill { get; set; }
        [JsonProperty("createdby")]
        public String createdby { get; set; }
        [JsonProperty("key")]
        public String key { get; set; }
    
    
    }
}
