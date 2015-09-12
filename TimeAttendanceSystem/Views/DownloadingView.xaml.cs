using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TimeAttendanceSystem.HelperClasses;
using System.Web.Script.Serialization;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for DownloadingView.xaml
    /// </summary>
    public partial class DownloadingView : Window
    {
        private Model.ClientInfo clientinfo2;
        
        BackgroundWorker bw = new BackgroundWorker();

        private Package cm;
        public bool IsAvailable { get; set; }
        public DownloadingView()
        {
            InitializeComponent();


        }


        public DownloadingView(Model.ClientInfo clientinfo2, Package cm)
        {
            InitializeComponent();
            this.clientinfo2 = clientinfo2;
            this.cm = cm;
            bw.DoWork += new DoWorkEventHandler(
        delegate(object o, DoWorkEventArgs args)
        {
            BackgroundWorker b = o as BackgroundWorker;

            SendDataToServer();

        });

            // what to do when worker completes its task (notify the user)
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
            delegate(object o, RunWorkerCompletedEventArgs args)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            });

            bw.RunWorkerAsync();
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

        private void SendDataToServer()
        {
            String responsebody = null;
            do
            {
                if (CheckForInternetConnection() == true)
                    using (WebClient client = new WebClient())
                    {

                        System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                        reqparm.Add("ClientName", clientinfo2.ClientName);
                        reqparm.Add("LicenseType", clientinfo2.LiscenceTypeID + "");
                        reqparm.Add("createdby", clientinfo2.CreatedBy);
                        reqparm.Add("MAC", EncDec.GetMacAddress());
                        reqparm.Add("isActive", "true");
                        if (clientinfo2.LiscenceTypeID > 9)
                        {
                            reqparm.Add("NoOfDevices", cm.Licensetype.NoOfDevices + "");
                            reqparm.Add("NoOfUsers", cm.Licensetype.NoOfUsers + "");
                            reqparm.Add("NoOfEmployees", cm.Licensetype.NoOfEmployees + "");

                        
                        }
                       

                        try
                        {
                            byte[] responsebytes = client.UploadValues("https://powerful-lowlands-4417.herokuapp.com/registration", "POST", reqparm);
                            responsebody = Encoding.UTF8.GetString(responsebytes);

                            if (responsebody == "\"Success\"")
                            {
                                using (TAS2013Entities context = new TAS2013Entities())
                                {

                                    ClientInfo ci = context.ClientInfoes.First();
                                    ci.isActive = true;
                                    Option opt = context.Options.FirstOrDefault();
                                    string json = EncDec.GetString(opt.WelcomeNote);
                                    Package df = JsonConvert.DeserializeObject<Package>(json);
                                    df.IsActive = true;
                                    var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                                    string jsonString = javaScriptSerializer.Serialize(df);
                                    opt.WelcomeNote = EncDec.GetBytes(jsonString);
                                    string MacAdd = EncDec.GetMacAddress();
                                    ClientMAC cm = context.ClientMACs.Where(aa => aa.MACAddress == MacAdd).First();
                                    cm.IsUsing = true;
                                    context.SaveChanges();

                                }


                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);

                        }

                    }
            }
            while (responsebody != "\"Success\"");

        }
   
      
    }
}
