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
    public partial class FrmDocDetail : Form
    {
        public FrmDocDetail()
        {
            InitializeComponent();
        }

        internal string identityNo;
        internal string docId;

        MySqlConnection sqlConnection = new MySqlConnection();

        private void FrmDocDetail_Load(object sender, EventArgs e)
        {
            #region Name-Surname

            lblIdentityNo.Text = identityNo;

            SqlCommand sqlCommand = new SqlCommand("select DocName + ' ' + DocSurname, DoctorId from Tbl_Doctors where DocIdentityNo=@p1", sqlConnection.MyConnection());
            sqlCommand.Parameters.AddWithValue("@p1", identityNo);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                lblNameSurname.Text = sqlDataReader[0].ToString();
                docId = sqlDataReader[1].ToString();
            }

            sqlConnection.MyConnection().Close();

            #endregion

            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select AppointmentId, ApptDate, ApptHour, ApptBranch, ApptDoc, ApptStatus, Tbl_Appointments.PatIdentityNo, PatName + ' ' + PatSurname as 'Patient Name', PatComplaint from Tbl_Appointments left join Tbl_Patients on Tbl_Appointments.PatIdentityNo = Tbl_Patients.PatIdentityNo where ApptDoc='" + lblNameSurname.Text + "'", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvAppt.DataSource = dataTable;
        }

        private void dgvAppt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rtxtComplaint.Text = "";
            int selected = dgvAppt.SelectedCells[0].RowIndex;
            string patient = dgvAppt.Rows[selected].Cells[7].Value?.ToString() ?? string.Empty;
            string complaint = dgvAppt.Rows[selected].Cells[8].Value?.ToString() ?? string.Empty;
            rtxtComplaint.Text = "Patient Name : " + patient + "\n" + "Complaint : " + complaint;
        }

        private void btnUpdateInfo_Click(object sender, EventArgs e)
        {
            FrmDocUpdateInfo frm = new FrmDocUpdateInfo();
            frm.docId = docId;
            frm.Show();
        }

        private void btnNotices_Click(object sender, EventArgs e)
        {
            FrmNotices frm = new FrmNotices();
            frm.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
