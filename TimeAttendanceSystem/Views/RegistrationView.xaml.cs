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
using System.ComponentModel;



namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        ClientInfo clientinfo = new ClientInfo();
        TAS2013Entities context = new TAS2013Entities();
       
        private Model.ClientInfo clientinfo2;                   
        public RegistrationView()
        {
            

            int activeMACs = context.ClientMACs.Where(aa => aa.IsUsing == true).ToList().Count;
            ClientLicense cl;
            ClientInfo checkForRegistered = context.ClientInfoes.FirstOrDefault();
           if (checkForRegistered!=null)
           {
              
               if (checkForRegistered.isActive==true)
               {

                   string json = EncDec.GetString(context.Options.FirstOrDefault().WelcomeNote);
                   Package df = JsonConvert.DeserializeObject<Package>(json);
                   cl = context.ClientLicenses.First(aa => aa.LicenseName == df.Licensetype.LicenseName);
                   ClientMAC mymac= new ClientMAC();
                   List<ClientMAC> cm = context.ClientMACs.ToList();
                   for (int i = 0; i < cm.Count; i++)
                       if (cm[i].MACAddress == EncDec.GetMacAddress())
                          mymac = cm[i];


                   if (mymac.ClientInfo == null)
                   {

                       
                       if (activeMACs < cl.NoOfUsers)
                       {
                        
                           mymac.ClientTabID = checkForRegistered.ClientID;
                           mymac.MACAddress = EncDec.GetMacAddress();
                           mymac.IsUsing = false;
                           context.ClientMACs.Add(mymac);
                           context.SaveChanges();
                           df.MAC.Clear();
                           df.MAC.Add(EncDec.GetMacAddress());
                           DownloadingView dv = new DownloadingView(checkForRegistered, df);
                           dv.Show();
                           context.Dispose();
                           this.Close();
                          

                       }
                       else if (activeMACs >= cl.NoOfUsers)
                       {
                           MessageBox.Show("You have exceeded your User limit");
                           this.Close();
                       }
                          
                       
                      
                       
                   }
                   else
                   {
                       if (activeMACs > cl.NoOfUsers)
                       {
                           MessageBox.Show("You have exceeded your User limit");
                           this.Close();

                       }
                       else if (activeMACs <= cl.NoOfUsers && mymac.IsUsing == true)
                       {
                           LoginPage mw = new LoginPage();

                           //mw.CommenceTripleChecking();
                           mw.Show();
                           context.Dispose();
                           this.Close();


                       }
                       else if (activeMACs <= cl.NoOfUsers && mymac.IsUsing == false)
                       {
                           DownloadingView dv = new DownloadingView(checkForRegistered, df);
                           dv.Show();
                           context.Dispose();
                           this.Close();
                       }


                   }
                   
                   
                
               
               
               
               }
               else 
                {
                    string json = EncDec.GetString(context.Options.FirstOrDefault().WelcomeNote);
                    Package df = JsonConvert.DeserializeObject<Package>(json); 
                    DownloadingView dv = new DownloadingView(checkForRegistered, df);
                    dv.Show();
                    context.Dispose();
                    this.Close();
               
               }

           
           }
           else
          
                InitializeComponent();
        }

       
     
       
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            Boolean MyMACPresentInTheLic = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Inv Files (.inv)|*.inv";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == true)
            {
              //OPens a save dialog
               String nameOfFile = openFileDialog1.FileName;
               EncDec.Decrypt(nameOfFile, "C:/Users/Public/license.inv", "weareinvenbitches");
               string json = File.ReadAllText("C:/Users/Public/license.inv");
               string fileContent = File.ReadAllText("C:/Users/Public/license.inv");
               Package df = JsonConvert.DeserializeObject<Package>(json);
               if (File.Exists("C:/Users/Public/license.inv"))
                   File.Delete("C:/Users/Public/license.inv");
               ClientInfo clientinfo2 = new ClientInfo();
               clientinfo2.isActive = df.IsActive;
               clientinfo2.CreatedBy = df.CreatedBy;
               clientinfo2.ClientName = df.Name;
               clientinfo2.LiscenceTypeID = df.Licensetype.TypeId;
               clientinfo2.CreatedDate = df.Date;
               
               if (df.Licensetype.TypeId > 9)
               {
                   ClientLicense cl2 = new ClientLicense();
                   cl2.LicenseID = df.Licensetype.TypeId;
                   cl2.LicenseName = df.Licensetype.LicenseName;
                   cl2.NoOfDevices = (short)df.Licensetype.NoOfDevices;
                   cl2.NoOfEmp = (short)df.Licensetype.NoOfEmployees;
                   cl2.NoOfUsers = (short)df.Licensetype.NoOfUsers;
                  
                   context.ClientLicenses.Add(cl2);
                   
                   context.SaveChanges();
               
               }
               context.ClientInfoes.Add(clientinfo2);
                   if (context.Options.FirstOrDefault() != null)
                       context.Options.FirstOrDefault().WelcomeNote = EncDec.GetBytes(fileContent);
                   else
                   {
                       Option opt = new Option();
                       opt.WelcomeNote = EncDec.GetBytes(fileContent);
                       context.Options.Add(opt);
                   
                   }
                   context.SaveChanges();
                   clientinfo2 = context.ClientInfoes.FirstOrDefault(aa => aa.isActive == clientinfo2.isActive && aa.ClientName == clientinfo2.ClientName);
                   for (int i = 0; i < df.MAC.Count; i++)
                   { 
                   ClientMAC cm = new ClientMAC();
                   if (df.MAC[i] == EncDec.GetMacAddress())
                      MyMACPresentInTheLic = true;
                      cm.MACAddress = df.MAC[i];
                      cm.IsUsing = false;
                      cm.ClientTabID = clientinfo2.ClientID;
                      context.ClientMACs.Add(cm);
                  
                   }
                   if (!MyMACPresentInTheLic)
                   {
                       ClientMAC cm = new ClientMAC();
                       cm.MACAddress = EncDec.GetMacAddress();
                       cm.ClientTabID = clientinfo2.ClientID;
                       cm.IsUsing = false;
                       context.ClientMACs.Add(cm);
                   }
                   context.SaveChanges();
                   DownloadingView dv = new DownloadingView(clientinfo2, df);
                   dv.Show();
                   context.Dispose();
                   this.Close();
                  
               }
        }
              
                     
      
    }
    }
