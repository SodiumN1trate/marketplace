using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Program {
        static void Main(string[] args) {
            QueryBuilder user = new QueryBuilder("user");

            bool isCursorCorrect = false;
            User userProfile = new User();
            //List<List<dynamic>> userData;
            while (isCursorCorrect != true) {
                string[] titles = { "Login", "Register" };
                Authentication.CreateButtonList(titles, true);
                Console.Write("Choose authentication method: ");
                string cursor = Console.ReadLine();

                Console.Clear();
                switch (cursor) {
                    case "1":
                        int userId = Authentication.Login();
                        List<List<dynamic>> userData = user.Select(new string[] { "id", "firstname", "lastname", "email", "balance" }, new dynamic[,] { { "id", $"'{ userId }'" } });
                        userProfile.ID = userData[0][0];
                        userProfile.Firstname = userData[0][1];
                        userProfile.Lastname = userData[0][2];
                        userProfile.Email = userData[0][3];
                        userProfile.Balance = userData[0][4];
                        isCursorCorrect = true;
                        Console.Clear();
                        break;
                    case "2":
                        Authentication.Register();
                        Console.WriteLine("Login to continue");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Not correct input...");
                        Console.Clear();
                        break;
                }
            }
            isCursorCorrect = false;

            while (isCursorCorrect != true) {
                string[] titles = { "Profile", "Wallet", "Explore products", "Product history", "List your product", "Exit" };
                Authentication.CreateButtonList(titles, true);
                Console.Write("Choose main menu button: ");
                string cursor = Console.ReadLine();

                switch(cursor) {
                    case "1":
                        Console.Clear();
                        userProfile.Profile();
                        break;
                    case "2":
                        Console.Clear();
                        userProfile.Wallet();
                        break;
                    case "3":
                        Console.Clear();
                        Product.BuyListedProducts(userProfile);
                        break;
                    case "4":
                        Console.Clear();
                        Product.UserBoughtProducts(userProfile);
                        break;
                    case "5":
                        Console.Clear();
                        Product.SellProduct(userProfile.ID);
                        break;
                    case "6":
                        Console.Clear();
                        isCursorCorrect = true;
                        break;
                    default:
                        Console.WriteLine("Not correct input...");
                        Console.Clear();
                        break;
                }
            }
            Console.WriteLine("The end!");
            Console.ReadKey();
        }
    }
}