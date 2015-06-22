using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reporting.Model;

namespace Reporting
{
    public partial class MultipleFilterSelect : Form
    {
        public MultipleFilterSelect(string Criteria)
        {
            InitializeComponent();
            _emp = context.EmpViews.ToList();

            ClearGrid();
            if (_emp.Count > 0)
            {
                foreach (EmpView obj in _emp)
                    AddObjectToGrid(obj);
            }
        }
        List<EmpView> _emp;
        TASReportingEntities context = new TASReportingEntities();

        public void ClearGrid()
        {
            // Define columns in grid
            lvwList.Columns.Clear();
            lvwList.Columns.Add("EmpID", 0, HorizontalAlignment.Left);
            lvwList.Columns.Add("Employee No", 90, HorizontalAlignment.Left);
            lvwList.Columns.Add("Employee Name", 160, HorizontalAlignment.Left);
            lvwList.Columns.Add("Card No", 90, HorizontalAlignment.Left);
            lvwList.Columns.Add("Department", 210, HorizontalAlignment.Left);
            lvwList.Columns.Add("City", 80, HorizontalAlignment.Left);
            lvwList.Columns.Add("NIC No", 120, HorizontalAlignment.Left);

        }
        //DataView view = new DataView(person);
        public void AddObjectToGrid(EmpView employee)
        {
            ListViewItem parent;
            parent = lvwList.Items.Add(employee.EmpID.ToString());
            parent.SubItems.Add(employee.EmpNo);
            parent.SubItems.Add(employee.EmpName);
            parent.SubItems.Add(employee.CardNo);
            parent.SubItems.Add(employee.DeptName);
            parent.SubItems.Add(employee.CityName);
            parent.SubItems.Add(employee.NicNo);
        }

        public int EmpID { get { return Convert.ToInt32(this.lvwList.SelectedItems[0].Text); } }
        private void lvwList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //frmSearch frm = new frmSearch(ref lvwList, "Find Employee");
            //frm.Show(this as IWin32Window);
        }

        private void lvwList_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
