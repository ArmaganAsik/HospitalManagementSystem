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
    public partial class FrmBranchPanel : Form
    {
        public FrmBranchPanel()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        void GetBranch()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Branches", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvBranch.DataSource = dataTable;
        }

        private void FrmBranchPanel_Load(object sender, EventArgs e)
        {
            GetBranch();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtId.Text = "";

            if (txtName.Text.Trim().Length >= 3)
            {
                SqlCommand sqlCommand = new SqlCommand("insert into Tbl_Branches (BranchName) values (@p1)", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Branch added successfully !", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtName.Text = "";
                GetBranch();
            }
            else MessageBox.Show("Please enter the branch name correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                SqlCommand sqlCommand = new SqlCommand("delete from Tbl_Branches where BranchId=@p1", sqlConnection.MyConnection());
                sqlCommand.Parameters.AddWithValue("@p1", txtId.Text);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.MyConnection().Close();
                MessageBox.Show("Branch deleted successfully !!", "DELETED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtId.Text = txtName.Text = "";
                GetBranch();
            }
        }

        private void dgvBranch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selected = dgvBranch.SelectedCells[0].RowIndex;
            var value = dgvBranch.Rows[selected].Cells[0].Value;
            txtId.Text = value != null ? value.ToString() : string.Empty;
            var value2 = dgvBranch.Rows[selected].Cells[1].Value;
            txtName.Text = value2 != null ? value2.ToString() : string.Empty;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "")
            {
                if (txtName.Text.Trim().Length >= 3)
                {
                    SqlCommand sqlCommand = new SqlCommand("update Tbl_Branches set BranchName=@p1 where BranchId=@p2", sqlConnection.MyConnection());
                    sqlCommand.Parameters.AddWithValue("@p1", txtName.Text);
                    sqlCommand.Parameters.AddWithValue("@p2", txtId.Text);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.MyConnection().Close();
                    MessageBox.Show("Branch updated successfully !", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtId.Text = txtName.Text = "";
                    GetBranch();
                }
                else MessageBox.Show("Please enter the branch name correctly !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
