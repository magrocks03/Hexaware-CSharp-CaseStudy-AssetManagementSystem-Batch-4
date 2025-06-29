using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AssetManagementSystem.util
{
    public static class DBConnUtil
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection(string filePath)
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                string connectionString = DBPropertyUtil.GetConnectionString(filePath);
                connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database connection failed: " + ex.Message);
                    return null;
                }
            }
            return connection;
        }
    }
}

