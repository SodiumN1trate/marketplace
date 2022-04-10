using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Ebay_parody {
    class QueryBuilder {
        private string tableName;
        private Database db;
        

        public QueryBuilder(string tableName) {
            this.tableName = tableName;
            db = new Database();
        }

        /**
        * Izvelk ierakstus no datubāzes
        *
        * @param string[] columns - saraksts ar kolonām no datubāzes, kuras ir jāizvelk, tas ir piem.: { "id", "name" } vai {"*"}
        * optional @param dynamic[,] where - saraksts ar filtrācijas datiem, tas ir piem.: { {"id", 3} }
        * 
        */
        public List<List<dynamic>> Select(string[] columns, dynamic[,] where = null) {
            dynamic whereData = "";

            if (where != null) {
                for (int i = 0; i < where.Length / 2; i++) {
                    dynamic tempData = String.Join("=", new dynamic[] { where[i, 0], where[i, 1] });
                    whereData += tempData + ",";
                }
                whereData = whereData.Remove(whereData.Length - 1, 1);
            } else {
                whereData = "1=1";
            }

            MySqlCommand cmd = new MySqlCommand($"SELECT {String.Join(", ", columns)} FROM {tableName} WHERE {whereData};", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();

            List<List<dynamic>> data = new List<List<dynamic>>();
            dynamic[] currentRow = new dynamic[rdr.FieldCount];

            while(rdr.Read()) {
                rdr.GetValues(currentRow);
                data.Add(new List<dynamic>(currentRow));
            }

            rdr.Close();
            return data;
       }

        /**
        * Ievieto ierakstu datubāzē
        * 
        * @param dynamic[] data - saraksts ar visiem datiem, kas atbilst datubāzes kolonai. Tādu kā id rakstīt šadi - ""
        * Piem. { "", "Pēteris", "Birziņš", "testers@lc.es", "123", "" }
        *  
        */
        public MySqlDataReader Insert(dynamic[] data) {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tableName} VALUES (\"{String.Join("\", \"", data)}\");", this.db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            return rdr;
        }


        /**
        * Rediģē ierakstu datubāzē, pēc kritērijiem
        * 
        * @param dynamic[,] data - Saraksts ar kolonu nosaukumu un ar vērtību uz kuru vajaga rediģēt. Piem. { {"firstname", "Jānis"}, {"lastname", "Pētersons"} }
        * @param dynamic[,] where - saraksts ar filtrācijas datiem, tas ir piem.: { {"id", 3} }
        *
        *
        */
        public MySqlDataReader Update(dynamic[,] data, dynamic[,] where) {
            dynamic updateData = "";
            dynamic whereData = "";
            for(int i = 0; i < data.Length / 2; i++) {
                dynamic tempData = String.Join("=", new dynamic[] { data[i, 0], "\"" + data[i, 1] + "\"" });
                updateData += tempData + ",";
            }

            for (int i = 0; i < where.Length / 2; i++) {
                dynamic tempData = String.Join("=", new dynamic[] { where[i, 0], where[i, 1] });
                whereData += tempData + ",";
            }
            updateData = updateData.Remove(updateData.Length - 1, 1);
            whereData = whereData.Remove(whereData.Length - 1, 1);
            MySqlCommand cmd = new MySqlCommand($"UPDATE {tableName} SET {updateData} WHERE {whereData};", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            return rdr;
        }

        /**
        * Izdzēš ierakstu no datubāzēs
        * 
        * @param dynamic[,] where - saraksts ar filtrācijas datiem, tas ir piem.: { {"id", 3} }
        * 
        */
        public MySqlDataReader Delete(dynamic[,] where) {
            dynamic whereData = "";

            for (int i = 0; i < where.Length / 2; i++) {
                dynamic tempData = String.Join("=", new dynamic[] { where[i, 0], where[i, 1] });
                Console.WriteLine(tempData);
                whereData += tempData + ",";
            }

            whereData = whereData.Remove(whereData.Length - 1, 1);
            MySqlCommand cmd = new MySqlCommand($"DELETE FROM {tableName} WHERE {whereData};", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();

            return rdr;

        }

    }
}
