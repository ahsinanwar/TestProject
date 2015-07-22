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
using Microsoft.Win32;
using System.IO;
using TimeAttendanceSystem.HelperClasses;



namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        ClientInfo clientinfo = new ClientInfo();
        TAS2013Entities context = new TAS2013Entities();
        public RegistrationView()
        {
            
            //Intitial check whether its registered already or not
            ClientInfo checkForRegistered = context.ClientInfoes.FirstOrDefault();
            if (checkForRegistered != null)
            {   
                MainWindow win2 = new MainWindow();
                win2.Show();
                this.Close();
            }
            else
                InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Inv Files (.inv)|*.inv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == true)
            {
              
               String nameOfFile = openFileDialog1.FileName;
               string json = File.ReadAllText(nameOfFile);
               Package df = JsonConvert.DeserializeObject<Package>(json);
               ClientInfo clientinfo = new ClientInfo();
               ClientMAC cm = new ClientMAC();
               clientinfo = context.ClientInfoes.FirstOrDefault(aa => aa.isActive == df.isActive && aa.ClientName == df.Name);
               if (clientinfo == null)
               {
                   ClientInfo clientinfo2 = new ClientInfo();
                   clientinfo2.isActive = df.isActive;
                   clientinfo2.CreatedBy = df.createdby;
                   clientinfo2.ClientName = df.Name;
                   clientinfo2.LiscenceTypeID = df.license.LiscenceTypeID;
                   context.ClientInfoes.Add(clientinfo2);
                   context.SaveChanges();
                   clientinfo2 = context.ClientInfoes.FirstOrDefault(aa => aa.isActive == clientinfo2.isActive && aa.ClientName == clientinfo2.ClientName);
                   cm.MACAddress = df.mac;
                   cm.ClientTabID = clientinfo2.ClientID;
                   DownloadingView dv = new DownloadingView(clientinfo2, cm);
                  
               }
                   //works if there is already a clientinfo made
               else
               {

                   cm.MACAddress = df.mac;
                   cm.ClientTabID = clientinfo.ClientID;
                   DownloadingView dv = new DownloadingView(clientinfo, cm);
               }
               context.ClientMACs.Add(cm);
               context.SaveChanges();

             
              

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
    }
