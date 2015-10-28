using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Xml;
using TimeAttendanceSystem.Model;
using TimeAttendanceSystem.ViewModels.VMDatabase;

namespace TimeAttendanceSystem.Views
{
    /// <summary>
    /// Interaction logic for FirstPageDatabase.xaml
    /// </summary>
    public partial class FirstPageDatabase : Window
    {
        public FirstPageDatabase()
        {
            try
            {
                InitializeComponent();
                VMDataBase vdb = new VMDataBase();
                this.DataContext = vdb;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Error Occured");
            }
           
        }

        private void RadButton_Click_1(object sender, RoutedEventArgs e)
        {

            StringBuilder Con1 = new StringBuilder("Data Source=");
            Con1.Append(serverName.Text);
            Con1.Append(";Initial Catalog=");
            Con1.Append(databaseName.Text);
            Con1.Append(";Persist Security Info=True;User ID=");
            Con1.Append(userName.Text);
            Con1.Append(";Password=");
            Con1.Append(password.Password);
            Con1.Append(";MultipleActiveResultSets=True;Application Name=EntityFramework");
            StringBuilder Con = new StringBuilder("metadata=res://*/Model.Model1.csdl|res://*/Model.Model1.ssdl|res://*/Model.Model1.msl;provider=System.Data.SqlClient;provider connection string=\"data source=");
            // StringBuilder Con = new StringBuilder("Data Source=");
            Con.Append(serverName.Text);
            Con.Append(";initial catalog=");
            Con.Append(databaseName.Text);
            Con.Append(";persist security info=True;");
            Con.Append("user id=");
            Con.Append(userName.Text);
            Con.Append(";password=");
            Con.Append(password.Password);
            Con.Append(";MultipleActiveResultSets=True;App=EntityFramework\"");
            string strCon = Con.ToString();
            string strCon1 = Con.ToString();
            updateConfigFile(strCon,strCon1);
            ConfigurationManager.RefreshSection("connectionStrings");
            TAS2013Entities ctx = new TAS2013Entities();
            List<Emp> dsf = new List<Emp>();
            dsf = ctx.Emps.ToList();
            RegistrationView rv = new RegistrationView();
            rv.Show();
            this.Close();
            

        }
        public void updateConfigFile(string con,string con2)
        {
            //updating config file
            XmlDocument XmlDoc = new XmlDocument();
            //Loading the Config file
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    String g = xElement.InnerXml.ToString();
                    xElement.LastChild.Attributes[1].Value = con;
                    xElement.LastChild.Attributes[2].Value = "System.Data.EntityClient";
                    //setting the coonection string
                      xElement.FirstChild.Attributes[1].Value = con2;
                     xElement.FirstChild.Attributes[2].Value = "System.Data.EntityClient";
                }
            }
            //writing the connection string in config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
    }
}
