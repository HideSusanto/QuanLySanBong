using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using QLSANBONG.Models;

namespace QLSANBONG
{
    public class Database
    {
        public static string connectionString = @"Data Source=ACERNITRO5EAGLE;Initial Catalog=QLSANBONG;Persist Security Info=True;User ID=" + UserLoginInfo.username + ";Password=" + UserLoginInfo.password;
        private static string connectionStringAdmin = @"Data Source=ACERNITRO5EAGLE;Initial Catalog=QLSANBONG;Integrated Security=True";
        private SqlConnection conn = new SqlConnection(connectionString);
        private SqlConnection connAdmin = new SqlConnection(connectionStringAdmin);
        public SqlConnection getConnection
        {
            get
            {
                return conn;
            }
        }
        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void closeConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
        public SqlConnection getConnectionAdmin
        {
            get
            {
                return connAdmin;
            }
        }
        public void openConnectionAdmin()
        {
            if (conn.State == ConnectionState.Closed)
            {
                connAdmin.Open();
            }
        }
        public void closeConnectionAdmin()
        {
            if (conn.State == ConnectionState.Open)
            {
                connAdmin.Close();
            }
        }
        public static void updateConnectionString()
        {
            connectionString = @"Data Source=ACERNITRO5EAGLE;Initial Catalog=QLSANBONG;Persist Security Info=True;User ID=" + UserLoginInfo.username + ";Password=" + UserLoginInfo.password;
        }
        public Database()
        {
           
        }
    }
}
