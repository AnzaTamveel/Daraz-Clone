using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class Employees : Users
    {


        public Employees( int Id , string name, string password, string email, string address, string phone, string designation, string status) : base(Id, name, password, email, address, phone, designation, status)
        {
            this.ID = Id ;
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
        }
        
        public Employees( string name, string password, string email, string address, string phone, string designation, string status) : base( name, password, email, address, phone, designation, status)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Address = address;
            this.Phone = phone;
            this.Designation = designation;
            this.Status = status;
        }

    }
}
