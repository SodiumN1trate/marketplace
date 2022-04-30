using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Product {
        private int id;
        private int userId;
        private string title;
        private string description;
        private decimal price;
        private int stock;

        public int ID {
            get { return id; }
            set { id = value; }
        }

        public int UserId {
            get { return userId; }
            set { userId = value; }
        }

        public string Title {
            get { return title; }
            set { title = value; }
        }

        public string Description {
            get { return description; }
            set { description = value; }
        }

        public decimal Price {
            get { return price; }
            set { price = value; }
        }

        public int Stock {
            get { return stock; }
            set { stock = value; }
        }

        QueryBuilder product = new QueryBuilder("product");
        QueryBuilder user = new QueryBuilder("user");

        // Izvada menu ar 2 pogām(1. Izvadīs visus produktus, kuri pārdodas)
        // zem switcha pieprasa no lietotāja kādu produktu vēlas iegādāties
        public static void ProductList(int userID, decimal userBalance) {
            QueryBuilder product = new QueryBuilder("product");
            Product productBuy = new Product();
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                Authentication.CreateButtonList(new string[] { "Products" });
                Authentication.CreateButtonList(new string[] { "Show product list", "back" }, true);
                Console.Write("Select operation: ");
                string cursor = Console.ReadLine();

                switch (cursor) {
                    case "1":
                        Console.Write("\n\n");
                        ListAllProducts();
                        break;
                    case "2":
                        isCursorCorrect = true;
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        continue;
                }

                Console.Write("Select product by \"ID\": ");
                string buyID = Console.ReadLine();
                try {
                    int convert = Convert.ToInt16(buyID);
                } catch {

                    Console.Clear();
                    continue;
                }
                //MATCH(title, description) AGAINST('vertigo')
                List<List<dynamic>> productToBuy = product.Select(new string[] { "*" }, new dynamic[,] { { "id", $"'{ buyID }'" } });

                try {
                    productBuy.ID = productToBuy[0][0];
                    if (productToBuy[0][5] == 1) {
                        Console.WriteLine($"Product with id - {buyID} does not exist");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                } catch {
                    Console.WriteLine($"Product with id - {buyID} does not exist");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                productBuy.ID = productToBuy[0][0];

                productBuy.ConfirmOrder(productToBuy[0], userID, userBalance);
                Console.ReadKey();
            }
        }


        // Lietotājs apstiprina vai atceļ produkta pasūtijumu
        private void ConfirmOrder(List<dynamic> productToBuy, int userID, decimal userBalance) {
            Authentication.CreateButtonList(new string[] { "Order" });
            
            Console.ForegroundColor = ConsoleColor.Green;
            ListFormating(productToBuy);
            Console.ForegroundColor = ConsoleColor.Gray;

            Authentication.CreateButtonList(new string[] { "Confirm order", "Cancel order" }, true);
            Console.Write("Please confirm order: ");
            string confirmation = Console.ReadLine();

            if (confirmation == "1") {
                if (userBalance - productToBuy[4] < 0) {
                    Console.WriteLine("You don't have enough money to buy this product");
                    Console.ReadKey();
                    Console.Clear();
                } else {
                    userBalance = userBalance - productToBuy[4];
                    product.Update(new dynamic[,] { { "user_id", userID }, { "is_bought", 1 } }, new dynamic[,] { { "id", this.id } });
                    user.Update(new dynamic[,] { { "balance", userBalance } }, new dynamic[,] { { "id", userID } });
                    Console.WriteLine($"You successfully ordered {productToBuy[2]}");
                    Console.ReadKey();
                    Console.Clear();
                }
            } else {
                Console.WriteLine($"Order {productToBuy[2]} canceled");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Izvada visus produktus vai tikai tos ko pieprasa lietotājs(Savus nopirktos produktus)
        private static void ListAllProducts(bool is_bought = false, int userID = -1) {
            QueryBuilder product = new QueryBuilder("product");
            List<List<dynamic>> listData;
            Authentication.CreateButtonList(new string[] { "Products" });
            if (is_bought == false) {
                listData = product.Select(new string[] { "*" });
            } else {
                listData = product.Select(new string[] { "*" }, new dynamic[,] { { "user_id", $"'{ userID }'" } });
            }
            
            bool isRow = true;
            int row = -1;
            while (isRow == true) {
                row += 1;

                try {
                    listData[row][0] = listData[row][0];
                    if (listData[row][6] == true && is_bought == true) {
                        ListFormating(listData[row]);
                        continue;
                    } else if (is_bought == false && listData[row][6] != true) {
                        ListFormating(listData[row]);
                        continue;
                    } else {
                        continue;
                    }
                } catch {
                    isRow = false;
                }
            }
        }
         
        // Metode, kura smuki formatē tekstu
        private static void ListFormating(List<dynamic> listData) {
            var lines = TextWrap(listData[3], 82);
            string listFormating = string.Format("{0,0}", "+-------------------------------+------------------------------------------------------------------------------------+\n");
            listFormating += string.Format("{0,0} {1,-29} {2,0} {3,-82} {4,0}", "|", $"{TextAlign(listData[2], 29)}", "|",$"{TextAlign("Product description", 82)}", "|\n");
            listFormating += string.Format("{0,0}", "+--------+-----------+----------+------------------------------------------------------------------------------------+\n");
            listFormating += string.Format("{0,0} {1,-6} {2,0} {3,-9} {4,0} {5,-8} {6,0} {7,-82} {8, 0}", "|", $"{TextAlign("ID", 6)}", "|", $"{TextAlign("PRICE", 9)}", "|", $"{TextAlign("In stock", 8)}", "|", $"{lines[0]}", "|\n");
            listFormating += string.Format("{0,0} {1,-82} {2,0}", "+--------+-----------+----------+", lines[1],"|\n");
            listFormating += string.Format("{0,0} {1,-6} {2,0} {3,-9} {4,0} {5,-8} {6,0} {7,-82} {8, 0}", "|", $"{TextAlign(Convert.ToString(listData[0]), 6)}", "|", $"{TextAlign(Convert.ToString(listData[4]),9)}", "|", $"{TextAlign(Convert.ToString(listData[5]), 8)}", "|", lines[2], "|\n");
            listFormating += string.Format("{0,0}", "+--------+-----------+----------+------------------------------------------------------------------------------------+\n");
            Console.WriteLine(listFormating);
        }

        // metoda, kura Centrē tekstu
        private static string TextAlign(string text, int length) {
            if (text.Length >= length) {
                return text;
            }

            int leftPadding = (length - text.Length) / 2;
            int rightPadding = length - text.Length - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }


        // Metode, kura sadala tekstu 3 daļās, lai ir vienkārši ielikt tekstu ListFormating() metodē
        public static List<String> TextWrap(string text, int maxLength) {
            var words = text.Split(' ');
            var lines = new List<string>();
            var currentLine = "";

            int outPutLine = 0;

            foreach (var currentWord in words) {
                if ((currentLine.Length > maxLength) || ((currentLine.Length + currentWord.Length) > maxLength)) {
                    lines.Add(currentLine);
                    currentLine = "";
                }

                if (currentLine.Length > 0) {
                    currentLine += " " + currentWord;
                } else {
                    currentLine += currentWord;
                }   
            }

            if (currentLine.Length > 0) {
                lines.Add(currentLine);
                outPutLine++;
            }

            switch (lines.Count) {
                case 1:
                    lines.Add("");
                    lines.Add("");
                    break;
                case 2:
                    lines.Add("");
                    break;
                case 3:
                    break;
            }
            return lines;
        }

        // Menu kur lietotājam dota izvēla vai izveidot produktu kurš tiks pārdots
        public static void SellProduct(int userId) {
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                Authentication.CreateButtonList(new string[] { "List your product" });
                Authentication.CreateButtonList(new string[] { "Create List", "back" }, true);
                Console.Write("Select operation: ");
                string cursor = Console.ReadLine();

                switch (cursor) {
                    case "1":
                        Console.Clear();
                        CreateList(userId);
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

        // Lieotājs izveido un izliek pārdot jaunu savu produktu
        private static void CreateList(int userId) {
            QueryBuilder product = new QueryBuilder("product");

            Product list = new Product();
            Authentication.CreateButtonList(new string[] { "List your product" });
            list.UserId = userId;
            list.Title = Authentication.Input("default", "Title", new string[] { "required", "min-length:5", "max-length:29" });
            list.Description = Authentication.Input("default", "Description", new string[] { "required", "max-length:255" });
            list.Price = Convert.ToDecimal(Authentication.Input("default", "Price", new string[] { "required", "max-length:9", "decimal-exist" }));
            list.Stock = 1;/*Convert.ToInt32(Authentication.Input("default", "In stock", new string[] { "required", "min-length:1", "max-length:8", "int-exist" }));*/

            product.Insert(new dynamic[] { 0, list.userId, list.title, list.Description, list.Price, list.Stock, 0 });
            List<List<dynamic>> listData = product.Select(new string[] { "*" }, new dynamic[,] { { "id", $"{"LAST_INSERT_ID()"}" } });
            list.ID = listData[0][0];
            list.ListConfirmation();
        }

        // Confirm stadija, kur lietotājam aplūko savu produtka saraksta izskatu un pienem vai nepieņem pārdot
        private void ListConfirmation() {
            Authentication.CreateButtonList(new string[] { "Your product list confirmation" });
            List<List<dynamic>> listData = product.Select(new string[] { "*" }, new dynamic[,] { { "id", this.id } });
            ListFormating(listData[0]);
            Authentication.CreateButtonList(new string[] { "Confirm list posting", "Cancel list posting"}, true);
            Console.Write("Select operation: ");
            string cursor = Console.ReadLine();

            switch (cursor) {
                case "1":
                    Console.WriteLine("Your listing has been published");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                default:
                    product.Delete(new dynamic[,] { { "id", listData[0][0] } });
                    Console.WriteLine("You list post is canceled");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }

        // izvada visu produktus ko pieslēgtais lietotājs ir nopircis
        public static void UserBoughtProducts(int userID) {
            
            QueryBuilder product = new QueryBuilder("product");

            Authentication.CreateButtonList(new string[] { "Your bought products" });
            
            ListAllProducts(true, userID);
            Console.ReadKey();
            Console.Clear();
        }

    }
}
