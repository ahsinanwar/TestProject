using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeAttendanceSystem.Model;
using System.Configuration;
using System.Xml;

namespace TimeAttendanceSystem.ViewModels.VMDatabase.Commands
{
    class SaveCommandDatabase : ICommand
    {
        #region Fields
        VMDataBase _vmdatabase;
        
        //Department _vm = new Department();
        #endregion

        #region constructors
        public SaveCommandDatabase(VMDataBase vm)
        { _vmdatabase = vm; }
        public bool CanExecute(object parameter)
        {
            //return (_vmcrew.selectedCrew != null);
            return true;
        }
        #endregion

        #region ICommand Members
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            VMDataBase vm = (VMDataBase)parameter;
            StringBuilder Con = new StringBuilder("metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=\"data source=");
            // StringBuilder Con = new StringBuilder("Data Source=");
            Con.Append(vm.serverName);
            Con.Append(";initial catalog=");
            Con.Append(vm.databaseName);
            Con.Append(";persist security info=True;");
            Con.Append("user id=");
            Con.Append(vm.userName);
            Con.Append(";password=");
            Con.Append(vm.password);
            Con.Append(";MultipleActiveResultSets=True;App=EntityFramework\"");
            string strCon = Con.ToString();
            updateConfigFile(strCon);
            ConfigurationManager.RefreshSection("connectionStrings");
            TAS2013Entities ctx = new TAS2013Entities();
            List<Emp> dsf = new List<Emp>();
            dsf = ctx.Emps.ToList();
        }
        public void updateConfigFile(string con)
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
                 //   xElement.LastChild.Attributes[1].Value = con;
                 //   xElement.LastChild.Attributes[2].Value = "System.Data.EntityClient";
                    //setting the coonection string
                  //  xElement.FirstChild.Attributes[1].Value = con;
                   // xElement.FirstChild.Attributes[2].Value = "System.Data.EntityClient";
                }
            }
            //writing the connection string in config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
        }
        #endregion
    }
}
