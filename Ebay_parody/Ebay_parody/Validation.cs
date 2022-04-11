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

                switch (criterion) {
                    case "required":
                        break;
                    case "email":
                        break;
                    case "min-length:":
                        int checks = Convert.ToInt32(Regex.Match(criterions[i], @"\d+").Value);
                        if (checks > parameter.Length) {
                            Console.Write("\r                                  ");
                            Console.SetCursorPosition(0, Console.CursorTop + 1);
                            Console.Write("Too less characters for password");
                            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                            return "Too less characters for password";
                        } else {
                            return "checked";
                        }
                    case "max-length:":
                        break;
                    default:
                        Console.WriteLine("FALSE");
                        return "error";
                }
                //int asd = Convert.ToInt16(Regex.Match(lengt, @"\d+").Value);
            }
            return "asd";
        }
    }
}
