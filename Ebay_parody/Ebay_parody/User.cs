using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class User {
    private int ID;
    private string firstname;
    private string surname;
    private string email;
    private decimal balance;
    private int productForSale; //CHANGE(list)

    public string Firstname {
        get { return firstname; }
        set { firstname = value; }
    }

    public string Surname {
        get { return surname; }
        set { surname = value; }
    }

    public decimal Balance {
        get { return balance; }
        set { balance = value; }
    }

    public int ProductForSale { //CHANGE(list)
        get { return productForSale; }
        set { productForSale = value; }
    }

    public static void Authentication() {
        string LoginMenu = string.Format("{0,-15}", "+ - - - - - - +\n");
        LoginMenu += string.Format("{0, 0} {1,-11}", "|", "1. Login    |\n");
        LoginMenu += string.Format("{0,-15}", "+ - - - - - - +\n");

        string RegisterMenu = string.Format("{0,-15}",  "+ - - - - - - +\n");
        RegisterMenu += string.Format("{0, 0} {1,-11}", "|", "2. Register |\n");
        RegisterMenu += string.Format("{0,-15}", "+ - - - - - - +\n");
        Console.WriteLine(LoginMenu);
        Console.WriteLine(RegisterMenu);

        Console.Write("Choose authentication method: ");
        string cursor = Console.ReadLine();
        Console.WriteLine(cursor);

        while (cursor != "1" || cursor != "2") {
            if (cursor != "1" || cursor != "2") {
                Console.WriteLine("Incorrect input, try again!");
                Console.Write("Choose authentication method: ");
                cursor = Console.ReadLine();
                continue;
            } else {
                break;
            }
        }

        Console.Clear();

        switch (cursor) {
            case "1":
                User.Login();
                break;
            case "2":
                User.Register();
                break;
            default:
                Console.WriteLine("Not correct number input...");
                Console.ReadKey();
                Environment.Exit(0);
                break;
        }     
    }

    private static string PasswordHider() {
        var pass = string.Empty;

        ConsoleKey key;
        do {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0) { //need some rework with backsapce
                Console.Write("\b \b");
                pass = pass.Remove(pass.Length - 1, 1);
            } else if (!char.IsControl(keyInfo.KeyChar)) {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        return pass;
    }

    public static void Login() {
        string LoginMenu = string.Format("{0,-15}", "+ - - - - - - +\n");
        LoginMenu += string.Format("{0, 0} {1,-11}", "|", "1. Login    |\n");
        LoginMenu += string.Format("{0,-15}", "+ - - - - - - +\n");

        Console.WriteLine(LoginMenu);

        Console.Write("Firstname: ");
        string Firstname = Console.ReadLine();

        Console.Write("Password: ");
        string pass = User.PasswordHider();



        //Console.WriteLine($"\nPass is {pass}"); //debug

        Console.ReadKey();
    }

    public static void Register() {
        string RegisterMenu = string.Format("{0,-15}", "+ - - - - - - +\n");
        RegisterMenu += string.Format("{0, 0} {1,-11}", "|", "1. Register sd|\n");
        RegisterMenu += string.Format("{0,-15}", "+ - - - - - - +\n");
        Console.WriteLine(RegisterMenu);


        Console.ReadKey();
    }
}