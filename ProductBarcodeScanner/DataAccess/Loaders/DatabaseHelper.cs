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
        private SQLiteConnection dbConnection;

        private DatabaseHelper() // initializer/constructor
        {
            // CREATE CONNECTION TO THE SQLITE file
            // store the connection variable inside this object
            
            dbConnection = new SQLiteConnection("Data Source=BarcodeDB.sqlite;Version=3;");
            dbConnection.Open();
        }

        private static volatile DatabaseHelper _instance;

        public static DatabaseHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseHelper();
            }
            return _instance;
        }

        public IDbConnection GetConnection()
        {
            return dbConnection;
        }

        // note that I am not returning an SQLiteCommand in the method signature!!
        // This is a general IDbCommand interface type.  This would let us change
        // the database type more easily in the future without retyping all the code
        // everywhere... since most other libraries for other database providers
        // all implement this interface, as long as we use the interface, the actual
        // objects that are being called don't matter (the other code doesn't care
        // if the database is SQLite or SQL Server).
        public IDbCommand BuildCommandForQueryString(String query)
        {
            return new SQLiteCommand(query, dbConnection);
        }

        public void Execute(String sql)
        {
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
