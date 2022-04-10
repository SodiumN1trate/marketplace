using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class Database {

        private MySqlConnection connection;
        private string cs = @"server=localhost;userid=root;password=;database=marketplace";
        public Database() {
            this.connection = new MySqlConnection(cs);
            this.connection.Open();
        }

        public MySqlConnection Connection {
            get { return connection; }
        }
    }
}