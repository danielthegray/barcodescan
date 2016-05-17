using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace ProductBarcodeScanner.DataAccess.Loaders
{
    public class DatabaseHelper
    {
        private static SQLiteConnection dbConnection;

        private DatabaseHelper() // initializer/constructor
        {
            // CREATE CONNECTION TO THE SQLITE file
            // store the connection variable inside this object
            
            dbConnection = new SQLiteConnection("Data Source=BarcodeDB.sqlite;Version=3;");
            dbConnection.Open();
        }

        private static DatabaseHelper _instance;

        public static DatabaseHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseHelper();
            }
            return _instance;
        }

        public Dictionary<string, dynamic> Query(String sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();

            reader.Read();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                // We could do a .ToString here
                result.Add(reader.GetName(i), reader.GetValue(i));
            }
            reader.Close();

            return result;
        }

        public void Execute(String sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
