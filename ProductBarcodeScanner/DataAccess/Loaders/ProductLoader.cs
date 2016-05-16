using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProductBarcodeScanner.DataAccess.Loaders
{
    public class ProductLoader
    {
        private ProductLoader()
        {
        }

        private static ProductLoader _instance = new ProductLoader();

        public static ProductLoader GetInstance()
        {
            return _instance;
        }

        public Product GetProduct(int id)
        {
            // TODO query the database for a product with the given ID
            // and return it
            var result = DatabaseHelper.GetInstance()
                .Query("SELECT * FROM Product "
                    +"JOIN Source ON Product.idSource = Source.id "
                    +"WHERE Product.id=" + id);

            Product product = new Product();
            // SQLiteDataReader returns objects. This is there the ToString() method comes from.
            product.name = result["name"].ToString();
            product.description = result["description"].ToString();
            product.upc = result["upc"].ToString();
            //Source source = new Source();
            //source.id = int.Parse(result[4]);
            //source.name = result[5];
            //product.source = source;
            return product;
        }

        public void SaveProduct(Product product)
        {
            DatabaseHelper.GetInstance()
                .Execute("UPDATE Product SET name=" + product.name +
                ", description=" + product.description+" WHERE id="+product.id);
        }

        public List<Product> GetAllProducts()
        {
            // return ALL the products in the database
            return null;
        }

        public Product GetProductByUPC(String upc)
        {
            return null;
        }

    }
}
