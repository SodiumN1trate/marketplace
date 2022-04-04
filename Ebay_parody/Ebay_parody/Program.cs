using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Program {
        static void Main(string[] args) {
            /*
             string cs = @"server=localhost;
                        userid=root;
                        password=;
                        database=csharp";

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