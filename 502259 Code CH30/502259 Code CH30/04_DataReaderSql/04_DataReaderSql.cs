using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_DataReaderSql
{
    class Program
    {
        static void Main(string[] args)
        {
            string select = "SELECT ContactName,CompanyName FROM Customers";

            SqlConnection conn = new SqlConnection(GetDatabaseConnection());

            using (conn)
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(select, conn);

                using (SqlDataReader aReader = cmd.ExecuteReader())
                {
                    while (aReader.Read())
                        Console.WriteLine("'{0}' from {1}", aReader.GetString(0), aReader.GetString(1));

                    aReader.Close();
                }

                // Not strictly necessary as we're in a using block
                conn.Close();
            }
        }

        private static string GetDatabaseConnection()
        {
            return "server=(local);" +
                "integrated security=SSPI;" +
                "database=Northwind";
        }

    }
}
