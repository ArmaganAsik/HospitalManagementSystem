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
    public partial class FrmAsstLogin : Form
    {
        public FrmAsstLogin()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Assistants where AsstIdentityNo=@p1 and AsstPassword=@p2", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", mskIdentityNo.Text);
            sqlCommand.Parameters.AddWithValue("@p2", txtPassword.Text);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read())
            {
                FrmAsstDetail frm = new FrmAsstDetail();
                frm.identityNo=mskIdentityNo.Text;
                frm.Show();
                Hide();
            }
            else MessageBox.Show("Access Denied !\nInvalid Identity No or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            sqlConnection.MyConnection().Close();
        }

        private void btnSee_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }
    }
}
