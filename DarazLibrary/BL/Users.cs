using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public  class Users
    {


        protected int ID;
        protected string Name;
        protected string Password;
        protected string Email;
        protected string Address;
        protected string Phone;
        protected string Designation;
        protected string Status;
        public Users(string name, string password, string email)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
        }
        public Users()
        {  }
        public Users(int id, string name, string password, string email, string address, string phone, string designation, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
        }
        public Users(string name, string password, string email, string address, string phone, string designation, string status)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
        }
        public int GetID() { return this.ID; }
        public void SetID(int id) { this.ID = id; }
        public void SetName(string name) { this.Name = name; }
        public void SetPassword(string password) { this.Password = password; }
        public void SetEmail(string email) { this.Email = email; }
        public void SetAddress(string address) { this.Address = address; }
        public void SetPhone(string phone) { this.Phone = phone; }
        public void SetDesignation(string designation) { this.Designation = designation; }
        public void SetStatus(string status) { this.Status = status; }
        public string GetName() { return this.Name; }
        public string GetEmail() { return this.Email; }
        public string GetPassword() { return this.Password; }
        public string GetPhone() { return this.Phone; }
        public string GetDesignation() { return this.Designation; }
        public string GetAddress() { return this.Address; }
        public string GetStatus() { return this.Status; }
    }
}
