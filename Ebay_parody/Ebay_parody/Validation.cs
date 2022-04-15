using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Ebay_parody {
    class Validation {
        public static string Validate(string[] criterions, string parameter) {
            for (int i = 0; i < criterions.Length; i++) {
                string criterion = Regex.Match(criterions[i], @"\D+").Value;
                string error;
                switch (criterion) {
                    case "required":
                        error =  Required(parameter);
                        if (error != "") { return error; } else { break; }
                    case "email":
                        error = Email(parameter);
                        if (error != "") { return error; } else { break; }
                    case "min-length:":
                        error = MinLength(criterions[i], parameter);
                        if (error != "") { return error; } else { break; }
                    case "max-length:":
                        error = MaxLength(criterions[i], parameter);
                        if (error != "") { return error; } else { break; }
                    default:
                        Console.WriteLine("FALSE");
                        return "error";
                }
            }
            return "checked";
        }

        public static string Required(string parameter) {
            if (parameter.Length == 0 && parameter.IndexOf(" ") == -1) {
                Output();
                return "Empty imput";
            } else { return ""; }
        }

        public static string Email(string parameter) {
            if (Regex.IsMatch(parameter, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z") == false) {
                Output();
                return "Please match the requested format";
            } else { return ""; }
        }

        public static string MinLength(string criterions, string parameter) {
            int checks = Convert.ToInt32(Regex.Match(criterions, @"\d+").Value);
            if (checks > parameter.Length) {
                Output();
                return "Too less characters for password";
            } else { return ""; }
        }

        public static string MaxLength(string criterions, string parameter) {
            int checks = Convert.ToInt32(Regex.Match(criterions, @"\d+").Value);
            if (checks < parameter.Length) {
                Output();
                return "Too many characters for password";
            } else { return ""; }
        }

        public static void Output() {
            Console.Write("\r                                  ");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("\r                                  ");
        }
    }
}
