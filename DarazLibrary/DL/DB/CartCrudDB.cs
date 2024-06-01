using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace DarazLibrary
{
    public class CartCrudDB : ICartCrud
    {


        public CartCrudDB()
        {
        }
        static CartCrudDB Instance;
        public static CartCrudDB GetCartCrudDB()
        {
            if (Instance == null)
            {
                Instance = new CartCrudDB();

            }
            return Instance;
        }





        static FunctionsDB Con = new FunctionsDB();
        public int AddProductToCart(UserCart Cart)
        {
            string Query = string.Format(@" MERGE INTO Cart AS target
             USING (VALUES ('{0}', '{1}' , '{2}' )) AS source (UserID, ProductID, Quantity)
             ON target.UserID = source.UserID AND target.ProductID = source.ProductID
             WHEN MATCHED THEN
             UPDATE SET target.Quantity = target.Quantity + source.Quantity
             WHEN NOT MATCHED THEN
             INSERT (UserID, ProductID, Quantity)
             VALUES (source.UserID, source.ProductID, source.Quantity);", Cart.GetUserId(), Cart.GetProductId().GetID(), Cart.GetQuantity());

            int RowsAffected = Con.SetData(Query);
            return RowsAffected;

        }
        public void DeleteFromCart(int Id)
        {
            string Query = string.Format(@"delete  from Cart where CartID = '{0}' ", Id);
            Con.SetData(Query);


        }
        public void ClearCart()
        {
            string Query = string.Format(@"delete from Cart");
            int RowsAffected = Con.SetData(Query);

        }
        public void LoadCartOfUser(Customer customer )
        {

            string Query = @"
            SELECT c.ProductID, p.ProductName, p.Description , p.Price , p.Category , p.Image1 , p.Image2 , c.Quantity , c.CartID, p.SellerID
            FROM Cart c
            INNER JOIN Products p ON c.ProductID = p.ProductID
            WHERE c.UserID = " + customer.GetID();


            DataTable dt = Con.GetData(Query);

            foreach (DataRow row in dt.Rows)
            {
                ProductCrudDB productCrud = new ProductCrudDB();
                Image image1 = productCrud.BinaryToImage((byte[])row["Image1"]);
                Image image2 = row["Image2"] == DBNull.Value ? null : productCrud.BinaryToImage((byte[])row["Image2"]);
                int quantity = Convert.ToInt32(row["Quantity"]);
                int Id = Convert.ToInt32(row["CartID"]);

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
                    IUserCrud Crud = new UserCrudDB();
                    UserCart cartItem = new UserCart(Id, customer.GetID(), product, quantity);

                customer.AddMyCart(cartItem);
            }


        }
    }
}
