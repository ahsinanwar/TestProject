using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reporting
{
    public partial class UCReportFilter : UserControl
    {
        public UCReportFilter()
        {
            InitializeComponent();
        }

        private void btnEmpFilter_Click(object sender, EventArgs e)
        {
            MultipleFilterSelect mfs = new MultipleFilterSelect("Employee");
            mfs.Show();
        }
    }
}
