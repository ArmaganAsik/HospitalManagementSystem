﻿using HospitalManagementSystem.SqlConnections;
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
    public partial class FrmNotices : Form
    {
        public FrmNotices()
        {
            InitializeComponent();
        }

        MySqlConnection sqlConnection = new MySqlConnection();

        private void FrmNotices_Load(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Tbl_Notices", sqlConnection.MyConnection());
            sqlDataAdapter.Fill(dataTable);
            dgvAnn.DataSource = dataTable;
        }
    }
}
