using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using ProductBarcodeScanner.DataAccess.Model;
using System.Data;

namespace ProductBarcodeScanner.DataAccess.Loaders
{
    public static class ProductHandler
    {

        private static const String SelectByIdQuery = SelectAllProductsQuery
            + " WHERE "+Product.IDColumn+"=@ProductId";

        private static const String SelectByUPCQuery = SelectAllProductsQuery
            + " WHERE "+Product.UPCColumn+"=@ProductUPC";

        private static const String SelectAllProductsQuery = "SELECT "
            // this next row ends up like "id,name,description,upc...."
            // As a general tip, in queries that you will use in code, it's a good
            // idea to put SELECT field1,field2,field3  
            // and not SELECT *, because it's less clear what you are selecting.
            // It's not a life-death matter, just a nice thing someone pointed out
            // to me a while back :)
            + Product.IDColumn + "," + Product.NameColumn + ","
                + Product.DescriptionColumn + "," + Product.BarcodeColumn + ","
                + Product.UPCColumn + ","
            + Provider.IDColumn + "," + Provider.NameColumn + "," + Provider.AddressColumn
            + " FROM " + Product.TableName
            + " JOIN " + Provider.TableName + " ON " + Product.ProviderIDColumn + "=" + Provider.IDColumn;

        public Product GetProduct(int id)
        {
            IDbCommand productSelectCommand = DatabaseHelper.GetInstance().
                BuildCommandForQueryString(SelectByIdQuery);
            IDbDataParameter productIdParameter = productSelectCommand.CreateParameter();
            productIdParameter.ParameterName = "@ProductId";
            productIdParameter.Value = id;
            productSelectCommand.Parameters.Add(productIdParameter);

            IDataReader productReader = productSelectCommand.ExecuteReader();
            productReader.Read();

            return BuildProductObjectFromDataReaderResult(productReader);
        }

        private static Product BuildProductObjectFromDataReaderResult(IDataReader productResult)
        {
            Product product = new Product();
            product.id = (int)productResult[Product.IDColumn];
            // SQLiteDataReader returns objects. This is there the ToString() method comes from.
            product.name = productResult[Product.NameColumn].ToString();
            product.description = productResult[Product.DescriptionColumn].ToString();
            product.upc = (int)productResult[Product.UPCColumn];

            Provider provider = new Provider();
            provider.id = (int)productResult[Provider.IDColumn];
            provider.name = productResult[Provider.NameColumn].ToString();
            provider.address = productResult[Provider.AddressColumn].ToString();

            product.provider = provider;

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
            List<Product> productList = new List<Product>();

            IDbCommand allProductsCommand = DatabaseHelper.GetInstance()
                .BuildCommandForQueryString(SelectAllProductsQuery);

            IDataReader allProductsReader = allProductsCommand.ExecuteReader();

            while (allProductsReader.Read())
            {
                productList.Add(BuildProductObjectFromDataReaderResult(allProductsReader));
            }

            return productList;
        }

        public Product GetProductByUPC(String upc)
        {
            IDbCommand productSelectCommand = DatabaseHelper.GetInstance().
                BuildCommandForQueryString(SelectByUPCQuery);
            IDbDataParameter productUPCParameter = productSelectCommand.CreateParameter();
            productUPCParameter.ParameterName = "@ProductUPC";
            productUPCParameter.Value = upc;
            productSelectCommand.Parameters.Add(productUPCParameter);

            IDataReader productReader = productSelectCommand.ExecuteReader();
            productReader.Read();

            return BuildProductObjectFromDataReaderResult(productReader);
        }

    }
}
