using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Ebay_parody {
    class Authentication {
        public static void CreateButtonList(string[] titles, bool serialNumbering = false) {
            for (int serialNumber = 0; serialNumber <= titles.Length - 1; serialNumber++) {
                string button = string.Format("{0,-15}", "+ - - - - - - +\n");
                button += string.Format("{0, 0} {1,-11} {2,0}", "|", $"{ (serialNumbering == true ? $"{serialNumber + 1}. {titles[serialNumber]}" : titles[serialNumber]) }", "|\n");
                button += string.Format("{0,-15}", "+ - - - - - - +\n");
                Console.WriteLine(button);
            }
        }

        private static string PasswordHider() {
            var pass = string.Empty;

            ConsoleKey key;
            do {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0) {
                    Console.Write("\b \b");
                    pass = pass.Remove(pass.Length - 1, 1);
                } else if (!char.IsControl(keyInfo.KeyChar)) {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }

        public static void Login() {
            Authentication.CreateButtonList(new string[] { "Login" });

            Console.Write("Firstname: ");
            string Firstname = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Authentication.PasswordHider();

            Console.ReadKey();
        }

        public static void Register() {
            Authentication.CreateButtonList(new string[] { "Register" });
            QueryBuilder user = new QueryBuilder("user");

            string firstname = Authentication.Input("Firstname", new string[] { "required", "min-length:5" });
            string lastname = Authentication.Input("Lastname", new string[] { "required", "min-length:5" });
            string email = Authentication.Input("Email", new string[] { "required", "email" });
            string pass = Authentication.Input("Password", new string[] { "required", "min-length:7", "max-length:30" });
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            user.Insert(new dynamic[] { 0, firstname, lastname, email, pass, 0 });
            //List<List<dynamic>> data = user.Select(new string[] { "id", "firstname" }, new dynamic[,] { { "email", $"'{ email }'" } });
            //Console.WriteLine(data[0][0]);

            //Console.WriteLine($"\nFirstname: {newUser.Firstname}\nLastame: {newUser.Lastname}\nEmail: {newUser.Email}"); //debug
        }

        public static string Input(string type, string[] parameters) {
            string error = "";
            string input = "";

            while (error != "checked") {
                Console.Write($"\r{type}: ");
                if (type == "Password") {
                    input = PasswordHider();
                } else {
                    input = Console.ReadLine();
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                error = Validation.Validate(parameters, input);
                if (error != "checked") {
                    Console.Write($"\r{error}");
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop + 3);
            return input;
        }

    }
}
