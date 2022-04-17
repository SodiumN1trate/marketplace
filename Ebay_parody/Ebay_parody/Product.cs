using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Product{
        private int id;
        private int userId;
        private string productTitle;
        private string description;
        private decimal price;
        private int stock;

        public int ProductID {
            get { return id; }
            set { id = value; }
        }

        public int UserId {
            get { return userId; }
            set { userId = value; }
        }

        public string ProductTitle {
            get { return productTitle; }
            set { productTitle = value; }
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
        public void ProductList() {
            bool isCursorCorrect = false;
            while (isCursorCorrect != true) {
                Authentication.CreateButtonList(new string[] { "Purchased products" });
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

        public void ListAllProducts() {
            Authentication.CreateButtonList(new string[] { "Products" });
            List<List<dynamic>> listData = product.Select(new string[] { "*" });
            bool isRow = true;
            int row = -1;
            while (isRow == true) {
                row += 1;
                try {
                    listData[row][0] = listData[row][0];
                    ListFormating(listData[row]);
                    continue;
                } catch {
                    isRow = false;
                }
            }
        }
         
        public void ListFormating(List<dynamic> listData) {
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

        private static string TextAlign(string text, int length) {
            if (text.Length >= length) {
                return text;
            }

            int leftPadding = (length - text.Length) / 2;
            int rightPadding = length - text.Length - leftPadding;
            return new string(' ', leftPadding) + text + new string(' ', rightPadding);
        }

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

        public void SellProduct(int userId) {
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

        private void CreateList(int userId) {
            Product list = new Product();
            Authentication.CreateButtonList(new string[] { "List your product" });
            list.userId = userId;
            list.ProductTitle = Authentication.Input("default", "Title", new string[] { "required", "min-length:5", "max-length:29" });
            list.Description = Authentication.Input("default", "Description", new string[] { "required", "max-length:255" });
            list.Price = Convert.ToDecimal(Authentication.Input("default", "Price", new string[] { "required", "max-length:9", "decimal-exist" }));
            list.Stock = Convert.ToInt32(Authentication.Input("default", "In stock", new string[] { "required", "min-length:1", "max-length:8", "int-exist" }));

            product.Insert(new dynamic[] { 0, list.userId, list.ProductTitle, list.Description, list.Price, list.Stock });
            List<List<dynamic>> listData = product.Select(new string[] { "*" }, new dynamic[,] { { "id", $"{"LAST_INSERT_ID()"}" } });
            list.ProductID = listData[0][0];

        }

        public void BuyProduct() {
            Authentication.CreateButtonList(new string[] { "Products for sale" });

        }
    }
}
