using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Program {
        static void Main(string[] args) {
            QueryBuilder users = new QueryBuilder("user");


            /*
            List<List<dynamic>> data = users.Select(new string[] { "*" });
            foreach (List<dynamic> row in data) {
                foreach (dynamic cell in row) {
                    Console.Write($"{cell}\t|");
                }
                Console.WriteLine();
            }
            */

            // Console.WriteLine(users.Insert(new dynamic[] {"", "Jânis", "Alhastino", "123221@xxl.c", "12321", ""}));
            /*
            var cmd = new MySqlCommand("SELECT * FROM `user`;", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            */

            users.Update(new dynamic[,] { {"firstname", "Janis" } }, new dynamic[,] { { "id", 3 } });

            users.Delete(new dynamic[,] { { "id", 3 } });

            User.Authentication();
        }
    }
}