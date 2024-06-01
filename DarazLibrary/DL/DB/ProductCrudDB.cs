using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
namespace DarazLibrary
{
    public class ProductCrudDB : IProductCrud
    {

        public ProductCrudDB()
        {
        }
        static ProductCrudDB Instance;
        public static ProductCrudDB GetProductCrudDB()
        {
            if (Instance == null)
            {
                Instance = new ProductCrudDB();

            }
            return Instance;
        }



        static FunctionsDB Con = new FunctionsDB();


        public List<Products> LoadAllProducts()
        {
            string Query = string.Format(@"SELECT * FROM Products where Status IS NULL ");
            DataTable result = Con.GetData(Query);

            List<Products> products = new List<Products>();
            foreach (DataRow row in result.Rows)
            {
                Image image1 = BinaryToImage((byte[])row["Image1"]);
                Image image2 = row["Image2"] == DBNull.Value ? null : BinaryToImage((byte[])row["Image2"]);

                Products product = new Products(
                    int.Parse(row["ProductID"].ToString()),
                    row["ProductName"].ToString(),
                    row["Description"].ToString(),
                    decimal.Parse(row["Price"].ToString()),
                    row["Category"].ToString(),
                    image1,
                    image2,
                                  int.Parse(row["SellerID"].ToString())

                );
                products.Add(product);
            }
            return products;
        }


        public Image BinaryToImage(byte[] image)
        {
            Image Coverted;
            using (MemoryStream ms = new MemoryStream(image))
            {
                Coverted = Image.FromStream(ms);
            }
            return Coverted;

        }

        public void DeleteProductByID(int ID)
        {
            string Query = string.Format(@"UPDATE Products SET Status = 'Deleted' WHERE ProductID = '{0}'", ID);
            Con.SetData(Query);
        }

        public List<Products> SearchProductByNameorCat(string SearchTerm)
        {
            string query = "SELECT * FROM Products WHERE ( ProductName LIKE @SearchTerm OR Category LIKE @SearchTerm ) AND Status IS NULL";
            SearchTerm = '%' + SearchTerm + '%';

            DataTable result = Con.GetDataWithParameters(query, SearchTerm);

            List<Products> products = new List<Products>();

            foreach (DataRow row in result.Rows)
            {
                Image image1 = BinaryToImage((byte[])row["Image1"]);
                Image image2 = row["Image2"] == DBNull.Value ? null : BinaryToImage((byte[])row["Image2"]);
                Products product = new Products(
             int.Parse(row["ProductID"].ToString()),
             row["ProductName"].ToString(),
             row["Description"].ToString(),
             decimal.Parse(row["Price"].ToString()),
             row["Category"].ToString(),
           image1,
                    image2,
              int.Parse(row["SellerID"].ToString())

                );
                products.Add(product);
            }

            return products;
        }
        public List<Products> LoadProductForSeller(Seller seller)
        {
            string Query = string.Format(@"SELECT * FROM Products WHERE SellerID = '{0}' and Status IS NULL ", seller.GetID()   );
            DataTable result = Con.GetData(Query);

            List<Products> products = new List<Products>();
            foreach (DataRow row in result.Rows)
            {
                Image image1 = BinaryToImage((byte[])row["Image1"]);
                Image image2 = row["Image2"] == DBNull.Value ? null : BinaryToImage((byte[])row["Image2"]);

                Products product = new Products(
             int.Parse(row["ProductID"].ToString()),
             row["ProductName"].ToString(),
             row["Description"].ToString(),
             decimal.Parse(row["Price"].ToString()),
             row["Category"].ToString(),
             image1,
             image2,
             int.Parse(row["SellerID"].ToString())

                );
                products.Add(product);
            }
            return products;
        }



        public bool ADDProduct(Products product , Seller CurrentSeller)
        {
            string query = "INSERT INTO Products (ProductName, Description, Price, SellerID, Category, Image1, Image2) " +
                           "VALUES (@ProductName, @Description, @Price, @SellerID, @Category, @Image1, @Image2)";

            using (MemoryStream ms1 = new MemoryStream())
            using (MemoryStream ms2 = new MemoryStream())
            {
                product.GetImage1().Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                byte[] image1Bytes = ms1.ToArray();

                byte[] image2Bytes = null;
                if (product.GetImage2() != null)
                {
                    product.GetImage2().Save(ms2, System.Drawing.Imaging.ImageFormat.Png);
                    image2Bytes = ms2.ToArray();
                }

                using (SqlConnection connection = new SqlConnection(Con.ConString()))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", product.GetName());
                        command.Parameters.AddWithValue("@Description", product.GetDescription());
                        command.Parameters.AddWithValue("@Price", product.GetPrice());
                        command.Parameters.AddWithValue("@SellerID", CurrentSeller.GetID());
                        command.Parameters.AddWithValue("@Category", product.GetCategory());
                        command.Parameters.Add("@Image1", SqlDbType.VarBinary).Value = image1Bytes;
                        command.Parameters.Add("@Image2", SqlDbType.VarBinary).Value = (object)image2Bytes ?? DBNull.Value;

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
        }


        public string ByteArrayToHexString(byte[] bytes)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
        public byte[] ConvertImageToBinary(Image img)
        {
            using (var memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, img.RawFormat);
                return memoryStream.ToArray();
            }
        }


    }
}
