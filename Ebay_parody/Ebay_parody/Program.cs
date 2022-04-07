using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Program {
        static void Main(string[] args) {

            string[] titles = {"Login", "Register"};
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