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

    public int IDs {
        get { return ID; }
    }

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

    public static void CreateButtonList(string[] titles, bool serialNumbering) {
        
        for(int serialNumber = 0; serialNumber <= titles.Length - 1; serialNumber++) {
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
        User.CreateButtonList(new string[] {"Login"}, false);

        Console.Write("Firstname: ");
        string Firstname = Console.ReadLine();

        Console.Write("Password: ");
        string pass = User.PasswordHider();

        Console.ReadKey();
    }

    public static void Register() {
        User.CreateButtonList(new string[] {"Register"}, false);

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


        Console.WriteLine($"\nFirstname: {newUser.Firstname}\nLastame: {newUser.Lastname}\nEmail: {newUser.Email}"); //debug

        Console.ReadKey();
    }
}