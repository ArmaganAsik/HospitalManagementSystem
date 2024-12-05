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
    public partial class FrmPatUpdateInfo : Form
    {
        public FrmPatUpdateInfo()
        {
            InitializeComponent();
        }

        internal string identityNo;

        MySqlConnection sqlConnection = new MySqlConnection();

        private void FrmPatUpdateInfo_Load(object sender, EventArgs e)
        {
            mskIdentityNo.Text = identityNo;

            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Patients where PatIdentityNo=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", identityNo);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                txtName.Text = sqlDataReader[1].ToString();
                txtSurname.Text = sqlDataReader[2].ToString();
                mskPhone.Text = sqlDataReader[4].ToString();
                txtPassword.Text = sqlDataReader[5].ToString();
                cmbGender.Text = sqlDataReader[6].ToString();
            }

            sqlConnection.MyConnection().Close();
        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3 || txtSurname.Text.Trim().Length < 2)
            {
                MessageBox.Show("Please enter your name and/or surname correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPassword.Text.Trim().Length < 4)
            {
                MessageBox.Show("Password must be between 4 and 8 characters !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("update Tbl_Patients set PatName=@p1, PatSurname=@p2, PatNumber=@p3, PatPassword=@p4, PatGender=@p5 where PatIdentityNo=@p6", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@p2", txtSurname.Text);
                sqlCommand.Parameters.AddWithValue("@p3", mskPhone.Text);
                sqlCommand.Parameters.AddWithValue("@p4", txtPassword.Text);
                sqlCommand.Parameters.AddWithValue("@p5", cmbGender.Text);
                sqlCommand.Parameters.AddWithValue("@p6", identityNo);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Your Info updated successfully !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } 
        }
    }
}
