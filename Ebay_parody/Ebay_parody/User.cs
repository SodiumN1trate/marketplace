using System;

namespace Ebay_parody {
    class User {
        private int id;
        private string firstname;
        private string lastname;
        private string email;
        private decimal balance;
        private int productForSale; //CHANGE(list)

        public int ID {
            get { return id; }
            set { id = value; }
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

        QueryBuilder user = new QueryBuilder("user");
        // Lietotājam izvada viņa profilu, kuru viņš var izmainit nospiežot vienu no pogām
        public void Profile() {
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                Authentication.CreateButtonList(new string[] { "Profile" });
                Authentication.CreateButtonList(new string[] { this.firstname, this.lastname, this.email, "Exit" }, true);
                Console.Write("Select operation: ");
                string cursor = Console.ReadLine();

                switch (cursor) {
                    case "1":
                        Console.Write("\nEnter new firstname\n");
                        string firstname = Authentication.Input("default", "Firstname", new string[] { "required", "min-length:5" });
                        user.Update(new dynamic[,] { { "firstname", firstname } }, new dynamic[,] { { "id", this.id } });
                        this.firstname = firstname;
                        Console.WriteLine("Firstname changed successfully");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        Console.Write("\nEnter new lastname\n");
                        string lastname = Authentication.Input("default", "Lastname", new string[] { "required", "min-length:5" });
                        user.Update(new dynamic[,] { { "lastname", lastname } }, new dynamic[,] { { "id", this.id } });
                        this.lastname = lastname;
                        Console.WriteLine("Lastname changed successfully");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3":
                        Console.Write("\nEnter new lastname\n");
                        string email = Authentication.Input("default", "Email", new string[] { "required", "email", "email-exist" });
                        user.Update(new dynamic[,] { { "email", email } }, new dynamic[,] { { "id", this.id } });
                        this.email = email;
                        Console.WriteLine("Email changed successfully");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        isCursorCorrect = true;
                        break;
                    default:
                        Console.WriteLine("Not correct input...");
                        Console.Clear();
                        break;
                }
            }
        }

        // Lietotājam izvada viņa maciņu ar to cik naudas ir maciņā un ir iespēja to papildināt.
        public void Wallet() {
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                Authentication.CreateButtonList(new string[] { "Wallet", $"Balance: {this.balance}" });
                Authentication.CreateButtonList(new string[] { "Add funds", "Back" }, true);
                Console.Write("Select operation: ");
                string cursor = Console.ReadLine();

                switch (cursor) {
                    case "1":
                        Console.Write("\nAdd funds\n");
                        decimal funds = Convert.ToDecimal(Authentication.Input("default", "Add funds", new string[] { "money-request" }));
                        this.balance += funds;
                        user.Update(new dynamic[,] { { "balance", this.balance } }, new dynamic[,] { { "id", this.id } });
                        Console.WriteLine("Funds added successfully");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        isCursorCorrect = true;
                        Console.Clear();
                        break;
                    default:
                        Console.Clear();
                        continue;
                }
            }
        }
    }
}
