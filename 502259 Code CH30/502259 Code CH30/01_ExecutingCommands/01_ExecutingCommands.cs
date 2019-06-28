using System;
using System.Data.SqlClient;
using System.Xml;

namespace _01_ExecutingCommands
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteNonQuery();
            ExecuteReader();
            ExecuteScalar();
            ExecuteXmlReader();
            Console.WriteLine();
        }

        // метод, который изменяет поле ContactName, возвращает количество изменений через rowsReturned
        static void ExecuteNonQuery()
        {           
            string select = "UPDATE Customers " +
                            "SET ContactName = 'Lino Rodriguez'" +
                            "WHERE ContactName = 'Evgeny Krivisheev'";
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                int rowsReturned = cmd.ExecuteNonQuery();
                Console.WriteLine("{0} rows returned.", rowsReturned);
                conn.Close();
            }
        }

        // метод для вывода значений полей ContactName,CompanyName
        static void ExecuteReader()
        {
            string select = "SELECT ContactName,CompanyName FROM Customers";
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("Contact: {0,-20} Company: {1}",
                                                 reader[0], reader[1]);
                }
            }
          
        }

        // метод для вывода количества записей в БД
        static void ExecuteScalar()
        {
            string select = "SELECT COUNT(*) FROM Customers";
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                object o = cmd.ExecuteScalar();
                Console.WriteLine(cmd.ExecuteScalar());
            }
            
        }

        // метод для вывода значений полей ContactName,CompanyName в XML-формате
        static void ExecuteXmlReader()
        {
            string select = "SELECT ContactName,CompanyName " +
                             "FROM Customers FOR XML AUTO";
            using (SqlConnection conn = new SqlConnection(GetDatabaseConnection()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(select, conn);
                XmlReader xr = cmd.ExecuteXmlReader();
                xr.Read();
                string data;
                do
                {
                    data = xr.ReadOuterXml();
                    if (!string.IsNullOrEmpty(data))
                        Console.WriteLine(data);
                } while (!string.IsNullOrEmpty(data));
                conn.Close();
            }

        }

        // строка подключения
        static string GetDatabaseConnection()
        {
             return "server=(local);" +
                    "integrated security=SSPI;" +
                    "database=Northwind";
        }
    }
}
