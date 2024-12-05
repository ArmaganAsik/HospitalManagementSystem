using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.SqlConnections
{
    internal class MySqlConnection
    {
        public SqlConnection MyConnection()
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=DESKTOP-FMRSALO\\SQLEXPRESS;Initial Catalog=HospitalManagementDb;Integrated Security=True;TrustServerCertificate=True");
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
