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

            /*
            var cmd = new MySqlCommand("SELECT * FROM `user`;", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            */

            var con = new MySqlConnection(cs);
            con.Open();

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"CREATE TABLE user(id INTEGER PRIMARY KEY AUTO_INCREMENT, firstname TEXT, lastname INT)";
            cmd.ExecuteNonQuery();

            Console.ReadKey();
            Console.WriteLine($"MySQL version : {con.ServerVersion}");
            Console.ReadKey();
            */
            User.Authentication();
        }
    }
}