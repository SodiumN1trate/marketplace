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

            string[] titles = {"Login", "Register"};
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                User.CreateButtonList(titles);
                Console.Write("Choose authentication method: ");
                string Cursor = Console.ReadLine();

                Console.Clear();
                switch (Cursor) {
                    case "1":
                        User.Login(User.CreateTitle(titles[0]));
                        isCursorCorrect = true;
                        break;
                    case "2":
                        User.Register(User.CreateTitle(titles[1]));
                        isCursorCorrect = true;
                        break;
                    default:
                        Console.WriteLine("Not correct input...");
                        Console.Clear();
                        break;
                }
            } 
        }
    }
}