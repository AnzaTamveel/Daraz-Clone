using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class OrderDetails
    {

        private int ID;
        private int ProductId;
        private int Quantity;
        private decimal Price;

        public OrderDetails(int iD, int productId, int quantity, decimal price)
        {
            this.ID = iD;
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
        }
        public int GetID() { return this.ID; }
        public int GetProductId() { return this.ProductId; }
        public int GetQuantity() { return this.Quantity; }
        public decimal GetPrice() { return this.Price; }
    }
}
