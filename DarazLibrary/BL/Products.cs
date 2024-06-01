using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DarazLibrary
{
    public class Products
    {
        private int ID;
        private string Name;
        private string Description;
        private decimal Price;
        private string Category;
        private Image Image1;
        private Image Image2;
        private int SellerID;
        private List<Reviews> ProductReviews = new List<Reviews>();

        public Products(int iD, string name, string description, decimal price, string category, int Sellerid)
        {
            this.ID = iD;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Category = category;
            this.SellerID = Sellerid;
        }
        public Products(string name, string description, decimal price, string category, Image image1, Image image2, int Sellerid)
        {

            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Category = category;
            this.Image1 = image1;
            this.Image2 = image2;
            this.SellerID = Sellerid;

        }
        public Products()
        {

        }
        public Products(Products product)
        {
            this.ID = product.GetID();
            this.Name = product.GetName();
            this.Description = product.GetDescription();
            this.Price = product.GetPrice();
            this.Category = product.GetCategory();
            this.Image1 = product.GetImage1();
            this.Image2 = product.GetImage2();

        }
        
        public Products(int Id, string name, string description, decimal price, string category, Image image1, Image image2, int Sellerid)
        {

            this.ID = Id;
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Category = category;
            this.Image1 = image1;
            this.Image2 = image2;
            this.SellerID = Sellerid;

        }
        public Image GetImage1() { return this.Image1; }
        public Image GetImage2() { return this.Image2; }
        public void SetImage1(Image bytes) { this.Image1 = bytes; }
        public void SetImage2(Image bytes) { this.Image2 = bytes; }
        public int GetID() { return this.ID; }
        public void SetID(int iD) { this.ID = iD; }
        public int GetSeelerID() { return this.SellerID; }
        public void SetSellerID(int iD) { this.SellerID = iD; }

        public string GetName() { return Name; }
        public void SetName(string name) { this.Name = name; }

        public string GetDescription() { return Description; }
        public void SetDescription(string description) { this.Description = description; }

        public decimal GetPrice() { return Price; }
        public void SetPrice(decimal price) { this.Price = price; }

        public string GetCategory() { return Category; }
        public void SetCategory(string category) { this.Category = category; }
    }
}
