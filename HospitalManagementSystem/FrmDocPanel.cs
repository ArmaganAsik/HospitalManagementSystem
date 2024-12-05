using HospitalManagementSystem.SqlConnections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class FrmDocPanel : Form
    {
        public FrmDocPanel()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        void ListDocs()
        {
            //DataGridView
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Doctors", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvDoc.DataSource = dataTable;
        }

        private void FrmDocPanel_Load(object sender, EventArgs e)
        {
            ListDocs();

            //Branches
            SqlCommand sqlCommand = new SqlCommand("select BranchName from Tbl_Branches", sqlConnection.MyConnection());
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                cmbBranch.Items.Add(sqlDataReader[0]);
            }

            sqlConnection.MyConnection().Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtId.Text = "";

            if (txtName.Text.Trim().Length < 3 || txtSurname.Text.Trim().Length < 2)
            {
                MessageBox.Show("Please enter name and surname correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (cmbBranch.Text == "")
            {
                MessageBox.Show("Please choose a branch first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Doctors (DocName, DocSurname, DocBranch, DocIdentityNo, DocPassword) values (@p1, @p2, @p3, @p4, @p5)", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                sqlCommand.Parameters.AddWithValue("@p2", txtSurname.Text);
                sqlCommand.Parameters.AddWithValue("@p3", cmbBranch.Text);
                sqlCommand.Parameters.AddWithValue("@p4", mskIdentityNo.Text);
                sqlCommand.Parameters.AddWithValue("@p5", txtPassword.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Doctor added successfully !", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = txtSurname.Text = txtPassword.Text = mskIdentityNo.Text = "";
                cmbBranch.SelectedIndex = -1;
                ListDocs();
            }
        }

        private void dgvDoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dgvDoc.SelectedCells[0].RowIndex;

            txtId.Text = dgvDoc.Rows[selected].Cells[0].Value?.ToString() ?? string.Empty;
            txtName.Text = dgvDoc.Rows[selected].Cells[1].Value?.ToString() ?? string.Empty;
            txtSurname.Text = dgvDoc.Rows[selected].Cells[2].Value?.ToString() ?? string.Empty;
            cmbBranch.Text = dgvDoc.Rows[selected].Cells[3].Value?.ToString() ?? string.Empty;
            mskIdentityNo.Text = dgvDoc.Rows[selected].Cells[4].Value?.ToString() ?? string.Empty;
            txtPassword.Text = dgvDoc.Rows[selected].Cells[5].Value?.ToString() ?? string.Empty;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                SqlCommand sqlCommand = new SqlCommand("delete from Tbl_Doctors where DoctorId=@p1", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtId.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Doctor deleted successfully !!", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Text = txtName.Text = txtSurname.Text = txtPassword.Text = mskIdentityNo.Text = "";
                cmbBranch.SelectedIndex = -1;
                ListDocs();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
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
                    sqlCommand.Parameters.AddWithValue("@p6", txtId.Text);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.MyConnection().Close();
                    MessageBox.Show("Doctor updated successfully !", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtId.Text = txtName.Text = txtSurname.Text = txtPassword.Text = mskIdentityNo.Text = "";
                    cmbBranch.SelectedIndex = -1;
                    ListDocs();
                }
            }
        }
    }
}
