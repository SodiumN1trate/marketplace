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

        // Izvada menu ar 2 pogām(1. Izvadīs visus produktus, kuri pārdodas)
        public static void ProductList() {
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
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "2":
                        isCursorCorrect = true;
                        Console.Clear();
                        break;
                }
            }
        }

        // Izvada visus produktus, kuri pārdodas
        private static void ListAllProducts() {
            QueryBuilder product = new QueryBuilder("product");

            Authentication.CreateButtonList(new string[] { "Products" });
            List<List<dynamic>> listData = product.Select(new string[] { "*" });
            bool isRow = true;
            int row = -1;
            while (isRow == true) {
                row += 1;

                try {
                    listData[row][0] = listData[row][0];
                    if (listData[row][6] == true) {
                        continue;
                    } else {
                        ListFormating(listData[row]);
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
            list.Stock = Convert.ToInt32(Authentication.Input("default", "In stock", new string[] { "required", "min-length:1", "max-length:8", "int-exist" }));

            product.Insert(new dynamic[] { 0, list.userId, list.title, list.Description, list.Price, list.Stock, 1 });
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

        public static void BuyProduct() {
            Authentication.CreateButtonList(new string[] { "Products for sale" });

        }
    }
}
