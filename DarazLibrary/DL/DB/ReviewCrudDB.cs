using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class ReviewCrudDB : IReviewCrud
    {

        string connectionstring = Utility.GetConnectionString();

        public ReviewCrudDB()
        {
        }
        static ReviewCrudDB Instance;
        public static ReviewCrudDB GetReviewCrudDB()
        {
            if (Instance == null)
            {
                Instance = new ReviewCrudDB();

            }
            return Instance;
        }


        static FunctionsDB Con = new FunctionsDB();

        public  List<Reviews> GetReviewOfProduct(int Id)
        {
            string Query = string.Format(@"select * from  Review where ProductID = '{0}' ", Id);
            Con.GetData(Query);
            DataTable result = Con.GetData(Query);
            List<Reviews> AllReview = new List<Reviews>();
            foreach (DataRow row in result.Rows)
            {
                Reviews review = new Reviews(
              Convert.ToInt32(row["ReviewID"]),
              Convert.ToInt32(row["ProductID"]),
              Convert.ToInt32(row["UserID"]),
              row["ReviewerName"].ToString(),
              row["ReviewContent"].ToString(),
              Convert.ToDateTime(row["ReviewDate"])
                 );
                AllReview.Add(review);

            }
            return AllReview;
        }

        public  void Addreview(Reviews reviews)
        {
            string Query = string.Format(@"insert into Review(ProductID , UserID , ReviewerName , ReviewContent , ReviewDate ) 
                values ('{0}' , '{1}' , '{2}' , '{3}' , '{4}' )", reviews.GetProductID(), reviews.GetUserID(), reviews.GetReviewerName(), reviews.GetReviewContent() , DateTime.Now);

            Con.SetData(Query);
           
        }
        public  List<Products> GetProductsForReviews(Customer customer)
        {

            string Query = string.Format(@"SELECT P.ProductID,P.SellerID,  P.ProductName, P.Description, P.Price, P.Category
                             FROM Products P
                             JOIN OrderDetails OD ON P.ProductID = OD.ProductID
                             JOIN Orders O ON OD.OrderID = O.OrderID
                             WHERE O.Status = 'SHIPPED'
                             AND NOT EXISTS (
                                 SELECT 1
                                 FROM Review R
                                 WHERE R.ProductID = P.ProductID
                                 AND R.UserID = '{0}')", customer.GetID());

            DataTable result = Con.GetData(Query);
            List<Products> AllProduct = new List<Products>();
            foreach (DataRow row in result.Rows)
            {
                Products product = new Products(

                    int.Parse(row["ProductID"].ToString()),
                    row["ProductName"].ToString(),
                    row["Description"].ToString(),
                    decimal.Parse(row["Price"].ToString()),
                    row["Category"].ToString(),
                    int.Parse(row["SellerID"].ToString())

                );
                AllProduct.Add(product);
            }

            return AllProduct;
        }
    }
}
