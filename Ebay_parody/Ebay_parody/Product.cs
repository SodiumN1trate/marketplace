using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebay_parody {
    class Product {
        private int ID;
        private string title;
        private string description;
        private decimal price;

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
    }
}