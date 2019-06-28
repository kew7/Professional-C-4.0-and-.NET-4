using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_DataReaderOledb
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "Provider=SQLOLEDB;" + GetDatabaseConnection();

            string select = "SELECT ContactName,CompanyName FROM Customers";

            OleDbConnection conn = new OleDbConnection(source);

            conn.Open();

            OleDbCommand cmd = new OleDbCommand(select, conn);

            OleDbDataReader aReader = cmd.ExecuteReader();

            while (aReader.Read())
                Console.WriteLine("'{0}' from {1}", aReader.GetString(0), aReader.GetString(1));

            aReader.Close();

            conn.Close();
        }

        private static string GetDatabaseConnection()
        {
            return "server=(local);" +
                "integrated security=SSPI;" +
                "database=Northwind";
        }

    }
}
