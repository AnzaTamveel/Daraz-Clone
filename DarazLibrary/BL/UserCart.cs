using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class UserCart
    {
        private int CartId;
        private int UserId;
        private Products ProductId;
        private int Quantity;
        public UserCart(int user, Products product, int quantity)
        {
            this.UserId = user;
            this.ProductId = product;
            this.Quantity = quantity;
        }
        public UserCart(int Id, int user, Products product, int quantity)
        {

            this.CartId = Id;
            this.UserId = user;
            this.ProductId = product;
            this.Quantity = quantity;
        }
        public int GetUserId() { return this.UserId; }
        public void SetUserId(int Id) { this.UserId = Id; }
        public int GetId() { return this.CartId; }
        public void SetId(int Id) { this.CartId = Id; }
        public Products GetProductId() { return this.ProductId; }
        public void SetProductId(Products Id) { this.ProductId = Id; }

        public int GetQuantity() { return this.Quantity; }
        public void SetQuantity(int Id) { this.Quantity = Id; }
    }
}
