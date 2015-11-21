using ReadersCommLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeAttendanceSystem.Model;

namespace TimeAttendanceSystem.Views.AccessControl
{
    /// <summary>
    /// Interaction logic for DeviceOperation.xaml
    /// </summary>
    public partial class DeviceOperation : Page
    {
        public DeviceOperation()
        {
            InitializeComponent();
            PopolateTreeView();
        }

        TAS2013Entities db = new TAS2013Entities();
        private void PopolateTreeView()
        {
            TreeViewItem treeItem = null;

            // North America
            
            List<Location> locs = new List<Location>();
            List<Reader> readers = new List<Reader>();
            locs = db.Locations.ToList();
            readers = db.Readers.Where(aa => aa.Status == true).ToList();
            foreach(var loc in locs)
            {
                treeItem = new TreeViewItem();
                treeItem.Header = loc.LocName;

                foreach (var rdr in readers.Where(aa => aa.LocID == loc.LocID).ToList())
                {
                    treeItem.Items.Add(new TreeViewItem() { Header = rdr.RdrName });
                }
                TVReaders.Items.Add(treeItem);
            }

        }

        private void TreeViewControl_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            string RdrName = ((TreeViewItem)e.NewValue).Header.ToString();
            var rdr = db.Readers.Where(aa => aa.RdrName == RdrName);
            List<string> fpIDs = new List<string>();
            if (rdr.Count() > 0)
            {
                if(IsConnectedToInternet(rdr.First().IpAdd))
                {
                    ZKReader zk = new ZKReader();
                    if (zk.Connect(rdr.First().IpAdd, rdr.First().IpPort))
                    {
                        fpIDs = zk.GetUsersFromDevice();
                        
                    }
                    zk.Disconnect();
                    LoadEmployeeInfo(fpIDs);
                }
            }

        }

        private void LoadEmployeeInfo(List<string> fpids)
        {
            List<EmpView> emps = new List<EmpView>();
            emps = db.EmpViews.ToList();
            foreach (var fpid in fpids)
            {
                int id = Convert.ToInt32(fpid);
                List<EmpView> emp = emps.Where(aa => aa.EmpID == id).ToList();
                if (emp.Count > 0)
                {
                    LVEmp.Items.Add(new EmpTemp() { Name = emp.FirstOrDefault().EmpName, ID = fpid, Section = emp.FirstOrDefault().SectionName, Designation = emp.FirstOrDefault().DesignationName, EmpNo = emp.FirstOrDefault().EmpNo });
                }
                else
                    LVEmp.Items.Add(new EmpTemp() { Name = "Undefined", ID = fpid, Section = "", Designation="", EmpNo=""});
            }
	

        }
        
        public bool IsConnectedToInternet(string IPAddress)
        {
            string host = IPAddress;
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
    public class EmpTemp
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Designation { get; set; }
        public string Section { get; set; }
        public string EmpNo { get; set; }
    }
}
