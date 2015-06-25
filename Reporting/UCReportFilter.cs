using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reporting.Model;

namespace Reporting
{
    public partial class UCReportFilter : UserControl
    {
        public UCReportFilter()
        {
            InitializeComponent();
             selectedEmps = new List<EmpView>();
        }
        public List<EmpView> selectedEmps;
        private void btnEmpFilter_Click(object sender, EventArgs e)
        {
            MultipleFilterSelect mfs = new MultipleFilterSelect("Employee", selectedEmps);
            if (mfs.ShowDialog() == DialogResult.OK)
            {
                selectedEmps = mfs.selectedEmps;
            }
            listBox1.Items.Clear();
            foreach(var item in selectedEmps)
            {
                listBox1.Items.Add(item.EmpName);
            }
        }
    }
}
