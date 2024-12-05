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
    public partial class FrmPatRegister : Form
    {
        public FrmPatRegister()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length < 3 || txtSurname.Text.Trim().Length < 2)
            {
                MessageBox.Show("Please enter your name and/or surname correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (mskIdentityNo.Text.Trim().Length < 11)
            {
                MessageBox.Show("Please enter your identity number correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtPassword.Text.Trim().Length < 4)
            {
                MessageBox.Show("Password must be between 4 and 8 characters !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Patients (PatName, PatSurname, PatIdentityNo, PatNumber, PatPassword, PatGender) values (@p1, @p2, @p3, @p4, @p5, @p6)", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@p2", txtSurname.Text);
                sqlCommand.Parameters.AddWithValue("@p3", mskIdentityNo.Text);
                sqlCommand.Parameters.AddWithValue("@p4", mskPhone.Text);
                sqlCommand.Parameters.AddWithValue("@p5", txtPassword.Text);
                sqlCommand.Parameters.AddWithValue("@p6", cmbGender.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Registered Successfully !\nPassword : " + txtPassword.Text, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
