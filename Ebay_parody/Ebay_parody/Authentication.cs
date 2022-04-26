using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Ebay_parody {
    class Authentication {
        // Automātiski tiek izveidota poga, kura smuki atkarībā no teksta to uztaisa
        public static void CreateButtonList(string[] titles, bool serialNumbering = false) {
            for (int serialNumber = 0; serialNumber <= titles.Length - 1; serialNumber++) {
                string buttonTitle;
                string buttonLength;
                if (serialNumbering == true) {
                    buttonTitle = $"{serialNumber + 1}. {titles[serialNumber]}";
                    buttonLength = string.Join("", Enumerable.Repeat("-", titles[serialNumber].Length + 3));
                } else {
                    buttonTitle = titles[serialNumber];
                    buttonLength = string.Join("", Enumerable.Repeat("-", titles[serialNumber].Length));
                }
                string button = string.Format("{0,0} {1,0} {2,1}", "+", $"{buttonLength}", "+\n");
                button += string.Format("{0, 0} {1,0} {2,0}", "|", $"{buttonTitle}", "|\n");
                button += string.Format("{0,0} {1,0} {2,1}", "+", $"{buttonLength}", "+\n");
                Console.WriteLine(button);
            }
        }

        // Slēp lietotāja vadīšanas laikā paroli
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

        // Pieprasa lietotājam ievadīt datus lai tiktu iekšā
        public static int Login() {
            QueryBuilder user = new QueryBuilder("user");
            Authentication.CreateButtonList(new string[] { "Login" });
            
            Console.Write("Email: ");
            string email = Authentication.Input("login", "Email", new string[] { "required", "email", "email-exist" });
            List<List<dynamic>> data = user.Select(new string[] { "*" }, new dynamic[,] { { "email", $"'{ email }'" } });

            Console.Write("Password: ");
            string pass = Authentication.Input("login", "Password", new string[] { "required", "min-length:7", "max-length:30", "pass-exist" }, data);

            Console.WriteLine("Successful logged-in");
            Console.ReadKey();
            return data[0][0];
        }

        // Pieprasa lietotājam tikt iekšā
        public static void Register() {
            Authentication.CreateButtonList(new string[] { "Register" });
            QueryBuilder user = new QueryBuilder("user");

            string firstname = Authentication.Input("default", "Firstname", new string[] { "required", "min-length:5" });
            string lastname = Authentication.Input("default", "Lastname", new string[] { "required", "min-length:5" });
            string email = Authentication.Input("default", "Email", new string[] { "required", "email", "email-exist" });
            string pass = Authentication.Input("default", "Password", new string[] { "required", "min-length:7", "max-length:30" });


            user.Insert(new dynamic[] { 0, firstname, lastname, email, pass, 0 });
            Console.WriteLine("Account successful registered");
            Console.ReadKey();
            //List<List<dynamic>> data = user.Select(new string[] { "id", "firstname" }, new dynamic[,] { { "email", $"'{ email }'" } });
            //Console.WriteLine(data[0][0]);

            //Console.WriteLine($"\nFirstname: {newUser.Firstname}\nLastame: {newUser.Lastname}\nEmail: {newUser.Email}"); //debug
        }

        // Pirmais ceļš lai pārbaudītu vai lietotājs ir ievadijis korektus datus
        public static string Input(string authenticationType, string type, string[] parameters, List<List<dynamic>> emailData = null) {
            string error = "";
            string input = "";

            while (error != "checked") {
                Console.Write($"\r{type}: ");
                if (type == "Password") {
                    input = PasswordHider();
                } else {
                    input = Console.ReadLine();
                }

                if (input.IndexOf('.') != -1 && type == "Price") {
                    input = input.Replace('.', ',');
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                if (authenticationType == "default" && type == "Email") {
                    error = Validation.Validate(parameters, input, authenticationType: $"{authenticationType}");
                } else if (authenticationType == "login" && type == "Email") {
                    error = Validation.Validate(parameters, input, authenticationType, emailData);
                } else if (authenticationType == "default") {
                    error = Validation.Validate(parameters, input);
                } else {
                    error = Validation.Validate(parameters, input, emailData: emailData);
                }
                
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
