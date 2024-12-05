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
    public partial class FrmAsstDetail : Form
    {
        public FrmAsstDetail()
        {
            InitializeComponent();
        }

        internal string identityNo;

        MySqlConnection sqlConnection = new MySqlConnection();

        void GetBranches()
        {
            //DataGridView
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Branches", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvBranch.DataSource = dataTable;

            //ComboBox
            SqlCommand sqlCommand = new SqlCommand("select BranchName from Tbl_Branches", sqlConnection.MyConnection());
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cmbBranch.Items.Add(sqlDataReader[0].ToString());
            }

            sqlConnection.MyConnection().Close();
        }

        void GetDoctors()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select (DocName + ' ' + DocSurname) as Doctor, DocBranch as Branch from Tbl_Doctors", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvDoc.DataSource = dataTable;
        }

        private void FrmAsstDetail_Load(object sender, EventArgs e)
        {
            lblIdentityNo.Text = identityNo;

            #region Name-surname

            SqlCommand sqlCommand = new SqlCommand("select AsstNameSurname from Tbl_Assistants where AsstIdentityNo=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", identityNo);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                lblNameSurname.Text = sqlDataReader[0].ToString();
            }

            sqlConnection.MyConnection().Close();

            #endregion

            GetBranches();

            GetDoctors();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (mskDate.MaskCompleted && mskTime.MaskCompleted)
            {
                if (cmbBranch.Text != "" && cmbDoc.Text != "")
                {
                    SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Appointments (ApptDate, ApptHour, ApptBranch, ApptDoc) values (@p1, @p2, @p3, @p4)", sqlConnection.MyConnection());
                    sqlCommand.Parameters.AddWithValue("@p1", mskDate.Text);
                    sqlCommand.Parameters.AddWithValue("@p2", mskTime.Text);
                    sqlCommand.Parameters.AddWithValue("@p3", cmbBranch.Text);
                    sqlCommand.Parameters.AddWithValue("@p4", cmbDoc.Text);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.MyConnection().Close();
                    MessageBox.Show("Appointment added successfully !", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    mskDate.Text = mskTime.Text = "";
                    cmbBranch.SelectedIndex = cmbDoc.SelectedIndex = -1;
                }
                else MessageBox.Show("Please choose branch and doctor !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Please enter date and time correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoc.Items.Clear();

            SqlCommand sqlCommand3 = new SqlCommand("select (DocName + ' ' + DocSurname) from Tbl_Doctors where DocBranch=@p1", sqlConnection.MyConnection());
            sqlCommand3.Parameters.AddWithValue("@p1", cmbBranch.Text);
            SqlDataReader sqlDataReader3 = sqlCommand3.ExecuteReader();
            while (sqlDataReader3.Read())
            {
                cmbDoc.Items.Add(sqlDataReader3[0].ToString());
            }

            sqlConnection.MyConnection().Close();
        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            if (rtxtNotice.Text.Trim().Length >= 3)
            {
                SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Notices (Notice) values (@p1)", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", rtxtNotice.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Notice published successfully !", "Published", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rtxtNotice.Text = "";
            }
            else MessageBox.Show("Please enter the notice correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDocPanel_Click(object sender, EventArgs e)
        {
            FrmDocPanel frm = new FrmDocPanel();
            frm.Show();
        }

        private void btnBranchPanel_Click(object sender, EventArgs e)
        {
            FrmBranchPanel frm = new FrmBranchPanel();
            frm.Show();
        }

        private void btnApptList_Click(object sender, EventArgs e)
        {
            FrmAppointmentList frm = new FrmAppointmentList();
            frm.Show();
        }

        private void btnNotices_Click(object sender, EventArgs e)
        {
            FrmNotices frm = new FrmNotices();
            frm.Show();
        }

        private void btnBranchRefresh_Click_1(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();

            GetBranches();
        }

        private void btnDocRefresh_Click_1(object sender, EventArgs e)
        {
            GetDoctors();
        }
    }
}
