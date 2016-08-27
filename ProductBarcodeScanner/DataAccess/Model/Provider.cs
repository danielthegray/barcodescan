using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBarcodeScanner.DataAccess.Model
{
    public class Provider
    {
        public static const String TableName = "Provider";
        public static const String IDColumn = "id";
        public static const String NameColumn = "name";
        public static const String AddressColumn = "address";

        public int id;
        public String name;
        public String address;
    }
}
