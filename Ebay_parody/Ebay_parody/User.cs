using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
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
    }
}
