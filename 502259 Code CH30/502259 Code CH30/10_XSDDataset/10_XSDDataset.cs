using System;
using System.Data.SqlClient;

namespace _10_XSDDataset
{
    class Program
    {
        static void Main(string[] args)
        {
            string select = "SELECT * FROM Products";

            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                SqlDataAdapter da = new SqlDataAdapter(select, conn);

                Products ds = new Products();

                da.Fill(ds, "Product");

                foreach (Products.ProductRow row in ds.Product)
                    Console.WriteLine("'{0}' from {1}",
                        row.ProductID,
                        row.ProductName);

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
