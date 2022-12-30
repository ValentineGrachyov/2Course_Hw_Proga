using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpServer.Attributes;

namespace HttpServer.Controllers
{
    [HttpController("accounts")]
    internal class Accounts
    {
        [HttpGET("getaccount")]
        public string GetAccount(int id)
        {
            return $"{id}";
        }

        [HttpGET("getbyid")]
        public Account GetAccountById()
        {
            //List<Account> accounts = new List<Account>();

            Account acc = null;

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SteamDB;Integrated Security=True";

            string sqlExpression = "SELECT * FROM ACCOUNT";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    // выводим названия столбцов
                    Console.WriteLine("{0}\t{1}\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));

                    while (reader.Read()) // построчно считываем данные
                    {
                        acc = new Account
                                (
                                    reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2)
                                );
                            
                    }
                }

                reader.Close();
            }
            return acc;
        }
         
        public void GetAccounts() { }

    }
}
