﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBarcodeScanner.DataAccess.Model
{
    public class Product
    {
        public static const String TableName = "Product";
        // normally, people configure the id to be auto-generated by the database engine
        public static const String IDColumn = TableName+".id";
        public static const String NameColumn = TableName+".name";
        public static const String DescriptionColumn = TableName+".description";
        // having as a separate field lets us define barcodes manually without
        // having to change the IDs. Also, saving it as a String allows us to 
        // put any characters we may need, and numbers of any size (int or even longs,
        // eventually run out)
        public static const String BarcodeColumn = TableName+".barcode";
        public static const String ProviderIDColumn = TableName+".idProvider";
        public static const String UPCColumn = TableName+".UPC";

        public int id;
        public String name;
        public String description;
        public String barcode;
        public Provider provider;
        public int upc;
    }
}