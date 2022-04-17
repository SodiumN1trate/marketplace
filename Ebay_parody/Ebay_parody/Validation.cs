using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Ebay_parody {
    class Validation {
        public static string Validate(string[] criterions, string parameter, string authenticationType = null, List<List<dynamic>> emailData = null) {
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
                    case "int-exist":
                        error = IsInt(parameter);
                        if (error != "") { return error; } else { break; }
                    case "decimal-exist":
                        error = IsDecimal(parameter);
                        if (error != "") { return error; } else { break; }
                    case "money-request":
                        error = IsMoneyDecimal(parameter);
                        if (error != "") { return error; } else { break; }
                    case "email-exist":
                        error = IsEmailExist(parameter, authenticationType);
                        if (error != "") { return error; } else { break; }
                    case "pass-exist":
                        error = IsPassExist(parameter, emailData);
                        if (error != "") { return error; } else { break; }
                    default:
                        return "asd";
                }
            }
            return "checked";
        }

        private static string Required(string parameter) {
            if (parameter.Length == 0 && parameter.IndexOf(" ") == -1) {
                Output();
                return "Empty imput";
            } else { return ""; }
        }

        private static string Email(string parameter) {
            if (Regex.IsMatch(parameter, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z") == false) {
                Output();
                return "Please match the requested format";
            } else { return ""; }
        }

        private static string MinLength(string criterions, string parameter) {
            int checks = Convert.ToInt32(Regex.Match(criterions, @"\d+").Value);
            if (checks > parameter.Length) {
                Output();
                return "Too less characters for password";
            } else { return ""; }
        }

        private static string MaxLength(string criterions, string parameter) {
            int checks = Convert.ToInt32(Regex.Match(criterions, @"\d+").Value);
            if (checks < parameter.Length) {
                Output();
                return "Too many characters for password";
            } else { return ""; }
        }

        private static string IsEmailExist(string parameter, string authenticationType) {
            QueryBuilder user = new QueryBuilder("user");
            try {
                List<List<dynamic>> emailData = user.Select(new string[] { "id" }, new dynamic[,] { { "email", $"'{ parameter }'" } });
                if (authenticationType == "login") {
                    emailData[0][0] = emailData[0][0];
                    return "";
                } else {
                    emailData[0][0] = emailData[0][0];
                    Output();
                    return "Email already exist";
                }
            } catch {
                if (authenticationType == "login") {
                    Output();
                    return "Email does not exist";
                } else {
                    return "";
                }
            }
        }

        private static string IsInt(string parameter) {
            if (Regex.IsMatch(parameter, @"\D+") == true) {
                Output();
                return "Unknown number";
            }
            decimal number = Convert.ToDecimal(parameter);
            if (number >= 100000000) {
                Output();
                return "Max stock can be 99999999 units";
            } else { return ""; }
        }

        private static string IsDecimal(string parameter) {
            if (Regex.IsMatch(parameter, @"\d+(\.\d+)?$") == false) {
                Output();
                return "Unknown number";
            }

            if (parameter.IndexOf('.') != -1) {
                parameter = parameter.Replace('.', ',');
            }

            decimal number = Convert.ToDecimal (parameter);
            if (DecimalDotNumbers(number) > 2) {
                Output();
                return "Incorrect decimal input";
            } else if (number >= 1000000) {
                Output();
                return "Max price can be at 1000000$";
            } else { return ""; }
        }

        private static int DecimalDotNumbers(decimal number) {
            int length = (number - Math.Truncate(number)).ToString().Length - 2;
            return length > 0 ? length : 0;
        }

        private static string IsMoneyDecimal(string parameter) {
            if (Regex.IsMatch(parameter, @"\D+") == true) {
                Output();
                return "Unknown number";
            }
            decimal Funds = Convert.ToDecimal(parameter);
            if (Funds > 100) {
                Output();
                return "You want add too much funds";
            } else { return ""; }
        }

        private static string IsPassExist(string parameter, List<List<dynamic>> emailData) {
            if (emailData[0][4] != parameter) {
                Output();
                return "Incorrect password";
            } else { return ""; }
        }

        private static void Output() {
            Console.Write("\r                                  ");
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            Console.Write("\r                                  ");
        }
    }
}
