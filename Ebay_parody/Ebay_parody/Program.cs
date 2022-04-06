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
            string cs = @"server=90.139.159.117;
                        userid=test;
                        password=123;
                        database=marketplace";

            var con = new MySqlConnection(cs);
            con.Open();

            var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = @"SELECT * FROM user";
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read()) {
                Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4));
            }
            */


            string[] titles = {"Login", "Register"};
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                User.CreateButtonList(" ", titles);
                Console.Write("Choose authentication method: ");
                string cursor = Console.ReadLine();

                Console.Clear();
                switch (cursor) {
                    case "1":
                        //User.CreateTitle(titles[0]);
                        User.Login();
                        isCursorCorrect = true;
                        break;
                    case "2":
                        //User.CreateTitle(titles[1]);
                        User.Register();
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