using DarazLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class OrderCrudDB : IOrderCrud
    {

        public OrderCrudDB()
        {
        }
        static OrderCrudDB Instance;
        public static OrderCrudDB GetOrderCrudDB()
        {
            if (Instance == null)
            {
                Instance = new OrderCrudDB();

            }
            return Instance;
        }


        static FunctionsDB Con = new FunctionsDB();


        public  List<Order> GetAllOrderOfCustomer(Customer customer)
        {
            string Query = string.Format(@"select * from Orders where UserID = '{0}'", customer.GetID());

            DataTable result = Con.GetData(Query);

            List<Order> order = new List<Order>();
            foreach (DataRow row in result.Rows)
            {
                Order user = new Order(

                    Convert.ToInt32(row["UserID"]),
                    row["Status"].ToString(),
                    DateTime.Parse(row["OrderDate"].ToString()),
                    float.Parse(row["TotalAmount"].ToString()),
                    row["ShippingAddress"].ToString(),
                    Convert.ToInt32(row["OrderID"])

                );
                order.Add(user);
            }

            return order;

        }
        public  List<Order> GetAllOrder()
        {
            string Query = string.Format(@"select * from Orders");

            DataTable result = Con.GetData(Query);

            List<Order> order = new List<Order>();
            foreach (DataRow row in result.Rows)
            {
                Order user = new Order(

                    Convert.ToInt32(row["UserID"]),
                    row["Status"].ToString(),
                    DateTime.Parse(row["OrderDate"].ToString()),
                    float.Parse(row["TotalAmount"].ToString()),
                    row["ShippingAddress"].ToString(),
                    Convert.ToInt32(row["OrderID"])

                );
                order.Add(user);
            }

            return order;

        }
        public  List<Order> SearchOrderStatus(string Searchterm)
        {
            string Query = string.Format(@"select * from Orders where Status = '{0}' ", Searchterm);

            DataTable result = Con.GetData(Query);

            List<Order> order = new List<Order>();
            foreach (DataRow row in result.Rows)
            {
                Order user = new Order(

                    Convert.ToInt32(row["UserID"]),
                    row["Status"].ToString(),
                    DateTime.Parse(row["OrderDate"].ToString()),
                    float.Parse(row["TotalAmount"].ToString()),
                    row["ShippingAddress"].ToString(),
                    Convert.ToInt32(row["OrderID"])

                );
                order.Add(user);
            }

            return order;
        }
        public  List<Order> SearchOrderByID(int Searchterm)
        {
            string Query = string.Format(@"select * from Orders where OrderID = '{0}' ", Searchterm);

            DataTable result = Con.GetData(Query);

            List<Order> order = new List<Order>();
            foreach (DataRow row in result.Rows)
            {
                Order user = new Order(

                    Convert.ToInt32(row["UserID"]),
                    row["Status"].ToString(),
                    DateTime.Parse(row["OrderDate"].ToString()),
                    float.Parse(row["TotalAmount"].ToString()),
                    row["ShippingAddress"].ToString(),
                    Convert.ToInt32(row["OrderID"])

                );
                order.Add(user);
            }

            return order;
        }
        public  void UpdateOrderByStatus(Order orders)
        {
            string Query = string.Format(@"UPDATE Orders SET Status = '{0}' WHERE OrderID = '{1}'", orders.GetStatus(), orders.GetOrderId());

            Con.SetData(Query);


        }
        public  void AddOrder(decimal totalAmount , Customer customer)
        {

            string orderQuery = string.Format(@"INSERT INTO Orders (UserID, OrderDate, TotalAmount, Status, ShippingAddress)
                                        OUTPUT INSERTED.OrderID
                                        VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
                                               customer.GetID(), DateTime.Now, totalAmount, "PENDING", customer.GetAddress());

            DataTable orderResult = Con.GetData(orderQuery);
            if (orderResult.Rows.Count > 0 && orderResult.Columns.Contains("OrderID"))
            {
                int orderID = Convert.ToInt32(orderResult.Rows[0]["OrderID"]);
               CartCrudDB cartCrud = new CartCrudDB();
                List<UserCart> cartItems = customer.GetMyCart();
                
                foreach (UserCart Items in cartItems)
                {
                    string orderDetailQuery = string.Format(@"INSERT INTO OrderDetails (OrderID, ProductID, Quantity, Price)
                                                      VALUES ('{0}', '{1}', '{2}', '{3}')",
                                                              orderID, Items.GetProductId().GetID(), Items.GetQuantity(), (Items.GetProductId().GetPrice() * Items.GetQuantity()));
                    int orderDetailRowsAffected = Con.SetData(orderDetailQuery);
                }

                cartCrud.ClearCart();
            }
            

        }
    }
}
