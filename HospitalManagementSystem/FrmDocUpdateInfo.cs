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
    public partial class FrmDocUpdateInfo : Form
    {
        public FrmDocUpdateInfo()
        {
            InitializeComponent();
        }

        internal string docId;

        MySqlConnection sqlConnection = new MySqlConnection();

        private void FrmDocUpdateInfo_Load(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("select * from Tbl_Doctors where DoctorId=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", docId);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                txtName.Text = sqlDataReader[1].ToString();
                txtSurname.Text = sqlDataReader[2].ToString();
                cmbBranch.Text = sqlDataReader[3].ToString();
                mskIdentityNo.Text = sqlDataReader[4].ToString();
                txtPassword.Text = sqlDataReader[5].ToString();
            }

            sqlConnection.MyConnection().Close();
        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3 || txtSurname.Text.Trim().Length < 2)
            {
                MessageBox.Show("Please enter name and surname correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbBranch.Text == "")
            {
                MessageBox.Show("Please choose a branch !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!mskIdentityNo.MaskCompleted)
            {
                MessageBox.Show("Please enter the identity no correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPassword.Text.Trim().Length < 4)
            {
                MessageBox.Show("Password must be between 4 and 8 characters !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("update Tbl_Doctors set DocName=@p1, DocSurname=@p2, DocBranch=@p3, DocIdentityNo=@p4, DocPassword=@p5 where DoctorId=@p6", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@p2", txtSurname.Text);
                sqlCommand.Parameters.AddWithValue("@p3", cmbBranch.Text);
                sqlCommand.Parameters.AddWithValue("@p4", mskIdentityNo.Text);
                sqlCommand.Parameters.AddWithValue("@p5", txtPassword.Text);
                sqlCommand.Parameters.AddWithValue("@p6", docId);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Your info updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
