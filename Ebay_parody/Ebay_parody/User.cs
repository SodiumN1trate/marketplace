using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

class User {
    private int ID;
    private string firstname;
    private string surname;
    private decimal balance;
    private Product productForSale;

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

    public Product ProductForSale {
        get { return productForSale; }
        set { productForSale = value; }
    }
}