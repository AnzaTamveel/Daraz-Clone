using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class Customer : Users
    {


        private List<UserCart> MyCart = new List<UserCart>();

        private List<Reviews> MyReviews = new List<Reviews>();
        private List<Order> MyOrders = new List<Order>();
        public Customer(int Id, string name, string password, string email, string address, string phone, string designation, string status) : base(Id, name, password, email, address, phone, designation, status)
        {
            this.ID = Id;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;

        }  
        public Customer( string name, string password, string email, string address, string phone, string designation, string status) : base( name, password, email, address, phone, designation, status)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;

        }


        public void SetMyCart(List<UserCart> MyCart) { this.MyCart = MyCart; }
        public void AddMyCart(UserCart MyCart) { this.MyCart.Add(MyCart); }
        public List<UserCart> GetMyCart() { return this.MyCart; }
        public void SetMyReview(List<Reviews> MyReviews) { this.MyReviews = MyReviews; }
        public void AddMyReview(Reviews MyReviews) { this.MyReviews.Add(MyReviews); }
        public List<Reviews> GetMyReview() { return  this.MyReviews; }
        public void SetMyOrder(List<Order> orders) { this.MyOrders = orders; }
        public void AddMyOrder(Order orders) { this.MyOrders.Add(orders); }
        public List<Order> GetMyOrder() { return this.MyOrders; }

    }
}
