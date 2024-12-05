using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class FrmWelcomePage : Form
    {
        public FrmWelcomePage()
        {
            InitializeComponent();
        }

        private void btnPat_Click(object sender, EventArgs e)
        {
            FrmPatLogin frm = new FrmPatLogin();
            frm.Show();
            Hide();
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {
            FrmDocLogin frm = new FrmDocLogin();
            frm.Show();
            Hide();
        }

        private void btnAsst_Click(object sender, EventArgs e)
        {
            FrmAsstLogin frm = new FrmAsstLogin();
            frm.Show();
            Hide();
        }
    }
}
