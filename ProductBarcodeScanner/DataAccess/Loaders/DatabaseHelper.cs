using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBarcodeScanner.DataAccess.Loaders
{
    public class DatabaseHelper
    {
        private DatabaseHelper() // initializer/constructor
        {
            // CREATE CONNECTION TO THE SQLITE file
            // store the connection variable inside this object
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

        public List<String> Query(String query)
        {
            return null;
        }

        public void Execute(String query)
        {

        }
    }
}
