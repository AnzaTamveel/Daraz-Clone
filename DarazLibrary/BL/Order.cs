using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class Order
    {
        private string Status;
        private DateTime OrderDate;
        private float TotalAmount;
        private string ShipingAddress;
        private int OrderID;
        private int UserId;
        private List<OrderDetails> OrderDetails;

        public Order(int Userid, string status, DateTime orderDate, float totalAmount, string shippingAddress, int orderID)
        {
            this.OrderID = orderID;
            this.UserId = Userid;
            this.Status = status;
            this.OrderDate = orderDate;
            this.TotalAmount = totalAmount;
            this.ShipingAddress = shippingAddress;
            this.OrderDetails = new List<OrderDetails>(); 

        }
        public Order(int orderID, string status)
        {
            this.OrderID = orderID;
            this.Status = status;

        }

        public void SetOrderDetail(List<OrderDetails> Orderdetails) {  this.OrderDetails = Orderdetails;  }
        public List<OrderDetails>  GetOrderDetail() {  return this.OrderDetails ;  }
        public void   AddOrderDetail(OrderDetails Detail) { this.OrderDetails.Add(Detail);  }
        public string GetStatus() { return this.Status; }
        public void SetStatus(string status) { this.Status = status; }
        public DateTime GetOrderDate() { return this.OrderDate; }

        public void SetOrderDate(DateTime dateTime) { this.OrderDate = dateTime; }
        public float GetTotalAmount() { return this.TotalAmount; }
        public void SetTotalAmount(float Amount) { this.TotalAmount = Amount; }
        public string GetShipAddress() { return this.ShipingAddress; }
        public void SetShipAddress(string Address) { this.ShipingAddress = Address; }
        public int GetOrderId() { return this.OrderID; }
        public void SetOrderId(int Id) { this.OrderID = Id; }
        public int GetUserId() { return this.UserId; }
        public void SetUserId(int Id) { this.UserId = Id; }
    }
}
