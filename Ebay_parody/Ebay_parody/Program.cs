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

            List<List<dynamic>> data = users.Select(new string[] { "*" });


            foreach (List<dynamic> row in data) {
                foreach (dynamic cell in row) {
                    Console.Write($"{cell}\t|");
                }
                Console.WriteLine();
            }

            users.Update(new dynamic[,] { { "lastname", "kazakevièa" }, {"firstname", "dsadsajânis"}  }, new dynamic[,] { { "id", 5 } });
            users.Insert(new dynamic[] { "", "Pçteris", "Birziòð", "testers@lc.es", "123", "" });

            string[] titles = { "Login", "Register" };
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                User.CreateButtonList(titles, true);
                Console.Write("Choose authentication method: ");
                string cursor = Console.ReadLine();

                Console.Clear();
                switch (cursor) {
                    case "1":
                        User.Login();
                        isCursorCorrect = true;
                        break;
                    case "2":
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