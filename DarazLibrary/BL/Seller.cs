using System.Collections.Generic;
namespace DarazLibrary
{
    public class Seller : Users
    {
        private string StoreName;
        private string StoreAddress;
        private List<Products> MyProduct = new List<Products>();

        private List<Voucher> MyVoucher = new List<Voucher>();


        public Seller(int Id, string name, string password, string email, string address, string phone, string designation, string status, string storeName, string storeAddress) : base(Id, name, password, email, address, phone, designation, status)
        {
            this.ID = Id;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
            this.StoreName = storeName;
            this.StoreAddress = storeAddress;
        }
        public Seller(string name, string password, string email, string address, string phone, string designation, string status, string storeName, string storeAddress) : base(name, password, email, address, phone, designation, status)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
            this.StoreName = storeName;
            this.StoreAddress = storeAddress;

        }


        public Seller(string storeName, string storeAddress)
        {
            this.StoreName = storeName;
            this.StoreAddress = storeAddress;
        }

        public bool AddProductIntoList(Products products)
        {
            MyProduct.Add(new Products(products));
            return true;
        }
        public void SetMyVoucher(List<Voucher> Vouchers) { this.MyVoucher = Vouchers; }
        public List<Voucher> GetMyVoucher() { return this.MyVoucher; }

        public void SetMyProduct(List<Products> Products) { this.MyProduct = Products; }
        public List<Products> GetMyProduct() { return this.MyProduct; }
        public string GetStoreName() { return this.StoreName; }
        public string GetStoreAddress() { return this.StoreAddress; }
        public void SetStoreName(string name) { this.StoreName = name; }
        public void SetStoreAddress(string address) { this.StoreAddress = address; }

    }
}
