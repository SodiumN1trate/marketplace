using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class User {
    private int ID;
    private string firstname;
    private string lastname;
    private string email;
    private decimal balance;
    private int productForSale; //CHANGE(list)

    public string Firstname {
        get { return firstname; }
        set { firstname = value; }
    }

    public string Lastname {
        get { return lastname; }
        set { lastname = value; }
    }

    public string Email {
        get { return email; }
        set { email = value; }
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
        string Cursor = Console.ReadLine();

        Console.Clear();

        switch (Cursor) {
            case "1":
                User.Login(ref LoginMenu);
                break;
            case "2":
                User.Register(ref RegisterMenu);
                break;
            default:
                Console.WriteLine("Not correct input...");
                Console.ReadKey();
                Environment.Exit(0); //Varbūt while ciklā ielikt, lai lietotājs atkārtoti vada skaitli
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
        Console.WriteLine();
        return pass;
    }

    public static void Login(ref string LoginMenu) {
        Console.WriteLine(LoginMenu);

        Console.Write("Firstname: ");
        string Firstname = Console.ReadLine();

        Console.Write("Password: ");
        string pass = User.PasswordHider();

        Console.ReadKey();
    }

    public static void Register(ref string RegisterMenu) {
        Console.WriteLine(RegisterMenu);

        Console.Write("Firstname: ");
        string Firstname = Console.ReadLine();

        Console.Write("Lastname: ");
        string Lastname = Console.ReadLine();

        Console.Write("Email: ");
        string Email = Console.ReadLine();

        Console.Write("Password: ");
        string pass = PasswordHider();

        User newUser = new User();
        newUser.Firstname = Firstname;
        newUser.Lastname = Lastname;
        newUser.Email = Email;
        newUser.Balance = 0;


        Console.WriteLine($"Firstname {newUser.Firstname}\nLastame {newUser.Lastname}\nEmail {newUser.Email}"); //debug

        Console.ReadKey();
    }
}