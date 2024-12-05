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
    public partial class FrmPatDetail : Form
    {
        public FrmPatDetail()
        {
            InitializeComponent();
        }

        internal string identityNo;

        MySqlConnection sqlConnection = new MySqlConnection();

        void GetPatName()
        {
            SqlCommand sqlCommand = new SqlCommand("select PatName, PatSurname from Tbl_Patients where PatIdentityNo=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", identityNo);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                lblNameSurname.Text = sqlDataReader[0] + " " + sqlDataReader[1];
            }

            sqlConnection.MyConnection().Close();
        }

        void GetPreviousAppt()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Appointments where PAtIdentityNo=" + identityNo, sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvPrevious.DataSource = dataTable;
        }

        void GetAvailableAppt()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Appointments where ApptBranch='" + cmbBranch.Text + "' and ApptDoc='" + cmbDoc.Text + "' and ApptStatus=0", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvAvailable.DataSource = dataTable;
        }

        private void FrmPatDetail_Load(object sender, EventArgs e)
        {
            lblIdentityNo.Text = identityNo;

            GetPatName();

            GetPreviousAppt();

            #region Branches

            SqlCommand sqlCommand = new SqlCommand("select BranchName from Tbl_Branches", sqlConnection.MyConnection());
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cmbBranch.Items.Add(sqlDataReader[0]);
            }

            sqlConnection.MyConnection().Close();

            #endregion
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoc.Items.Clear();
            dgvAvailable.DataSource = null;
            txtId.Text = "";

            SqlCommand sqlCommand = new SqlCommand("select DocName, DocSurname from Tbl_Doctors where DocBranch=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", cmbBranch.Text);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cmbDoc.Items.Add(sqlDataReader[0] + " " + sqlDataReader[1]);
            }

            sqlConnection.MyConnection().Close();
        }

        private void cmbDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtId.Text = "";

            GetAvailableAppt();
        }

        private void lnkUpdateInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmPatUpdateInfo frm = new FrmPatUpdateInfo();
            frm.identityNo = lblIdentityNo.Text;
            frm.Show();
        }

        private void dgvAvailable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dgvAvailable.SelectedCells[0].RowIndex;
            var value = dgvAvailable.Rows[selected].Cells[0].Value;
            txtId.Text = value != null ? value.ToString() : string.Empty;
        }

        private void btnMakeAppt_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Please choose a appointment first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rtxtComplaint.Text.Trim().Length < 3)
            {
                MessageBox.Show("Please enter your complaints correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("update Tbl_Appointments set PatComplaint=@p1, PatIdentityNo=@p2, ApptStatus=1 where AppointmentId=@p3", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", rtxtComplaint.Text);
                sqlCommand.Parameters.AddWithValue("@p2", lblIdentityNo.Text);
                sqlCommand.Parameters.AddWithValue("@p3", txtId.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Appointment added successfully !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtId.Text = rtxtComplaint.Text = "";

                GetPreviousAppt();

                GetAvailableAppt();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetPatName();
        }
    }
}
