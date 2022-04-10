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

   
       public List<List<dynamic>> Select(string[] columns) {

            MySqlCommand cmd = new MySqlCommand($"SELECT {String.Join(", ", columns)} FROM {this.tableName}", db.Connection);
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

        public MySqlDataReader Insert(dynamic[] data) {
            MySqlCommand cmd = new MySqlCommand($"INSERT INTO {tableName} VALUES (\"{String.Join("\", \"", data)}\");", this.db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            return rdr;
        }

        public MySqlDataReader Update(dynamic[,] data, dynamic[,] where) {
            dynamic updateData = "";
            dynamic whereData = "";

            for(int i = 0; i < data.Length - 1; i++) {
                dynamic tempData = String.Join("=", new dynamic[] { data[i, 0], "\"" + data[i, 1] + "\"" });
                updateData += tempData + ",";
            }

            for (int i = 0; i < where.Length - 1; i++) {
                dynamic tempData = String.Join("=", new dynamic[] { where[i, 0], where[i, 1] });
                Console.WriteLine(tempData);
                whereData += tempData + ",";
            }
            updateData = updateData.Remove(updateData.Length - 1, 1);
            whereData = whereData.Remove(whereData.Length - 1, 1);
            MySqlCommand cmd = new MySqlCommand($"UPDATE {tableName} SET {updateData} WHERE {whereData};", db.Connection);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Close();
            return rdr;
        }

        public MySqlDataReader Delete(dynamic[,] where) {
            dynamic whereData = "";

            for (int i = 0; i < where.Length - 1; i++) {
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
