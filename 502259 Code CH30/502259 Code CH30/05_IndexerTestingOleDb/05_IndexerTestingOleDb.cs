using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _05_IndexerTestingOleDb
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "Provider=SQLOLEDB;" + GetDatabaseConnection();

            string select = "SELECT CategoryID,CategoryName FROM	Categories";

            using (OleDbConnection conn = new OleDbConnection(source))
            {
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(select, conn);

                OleDbDataReader reader = cmd.ExecuteReader();

                reader.Read();

                // Now time	access to the first	field in the reader...
                int maxIterations = 1000000;

                int theCategoryID = (int)reader[0];
                theCategoryID = (int)reader["CategoryId"];
                theCategoryID = reader.GetInt32(0);

                MethodTimer.TimeMethod(() => theCategoryID = (int)reader[0], maxIterations, "{0} iterations	using numeric indexer :	{1}ms");
                MethodTimer.TimeMethod(() => theCategoryID = (int)reader["CategoryId"], maxIterations, "{0} iterations	using string indexer :	{1}ms");
                MethodTimer.TimeMethod(() => theCategoryID = reader.GetInt32(0), maxIterations, "{0} iterations	using GetInt32() :	{1}ms");
            }

            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                SqlCommand cmd = new SqlCommand(select, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                int maxIterations = 1000000;

                int theCategoryID = (int)reader[0];
                theCategoryID = (int)reader["CategoryId"];
                theCategoryID = reader.GetInt32(0);

                MethodTimer.TimeMethod(() => theCategoryID = (int)reader[0], maxIterations, "{0} iterations	using numeric indexer :	{1}ms");
                MethodTimer.TimeMethod(() => theCategoryID = (int)reader["CategoryId"], maxIterations, "{0} iterations	using string indexer :	{1}ms");
                MethodTimer.TimeMethod(() => theCategoryID = reader.GetInt32(0), maxIterations, "{0} iterations	using GetInt32() :	{1}ms");
            }
        }

        private static string GetDatabaseConnection()
        {
            return "server=(local);" +
                "integrated security=SSPI;" +
                "database=Northwind";
        }

    }

    public class MethodTimer
    {
        public static void TimeMethod(Action method, int iterations, string message)
        {
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
                method();

            sw.Stop();

            Console.WriteLine(message, iterations, sw.ElapsedMilliseconds);
        }
    }
}
