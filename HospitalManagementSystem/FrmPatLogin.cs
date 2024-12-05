using HospitalManagementSystem.SqlConnections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class FrmPatLogin : Form
    {
        public FrmPatLogin()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatRegister frm = new FrmPatRegister();
            frm.Show();
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Patients where PatIdentityNo=@p1 and PatPassword=@p2", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", mskIdentityNo.Text);
            sqlCommand.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                FrmPatDetail frm = new FrmPatDetail();
                frm.identityNo = mskIdentityNo.Text;
                frm.Show();
                Hide();
            }
            else MessageBox.Show("Access Denied !\nInvalid Identity No or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            sqlConnection.MyConnection().Close();
        }
    }
}
