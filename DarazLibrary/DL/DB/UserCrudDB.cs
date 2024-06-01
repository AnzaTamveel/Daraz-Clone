using System;
using System.Collections.Generic;
using System.Data;

namespace DarazLibrary
{
    public class UserCrudDB : IUserCrud
    {
        public UserCrudDB()
        {
        }
        static UserCrudDB Instance;
        public static UserCrudDB GetUserCrudDB()
        {
            if (Instance == null)
            {
                Instance = new UserCrudDB();

            }
            return Instance;
        }
        static private FunctionsDB Con = new FunctionsDB();
        public bool DeleteUserThroughEmail(Users User)
        {
            try
            {
                string Query = string.Format(@"Update Users  SET Status = 'Deleted'  where Email = '{0}' " , User.GetEmail());
                int RowAffected = Con.SetData(Query);
                if (RowAffected > 0)
                {
                    Query = string.Format(@"Update Products  SET Status = 'Deleted' where SellerId = '{0}'", User.GetID());
                    Con.SetData(Query);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public List<Users> ViewAllUser()
        {
            string Query = string.Format(@"select * from Users");
            DataTable result = Con.GetData(Query);
            List<Users> users = new List<Users>();

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];

                Users loggedInUser = new Users(
                    Convert.ToInt32(row["ID"]),
                    row["Name"].ToString(),
                    row["Password"].ToString(),
                    row["Email"].ToString(),
                    row["Address"].ToString(),
                    row["Phone"].ToString(),
                    row["Designation"].ToString(),
                    row["Status"].ToString()
                );
                users.Add(loggedInUser);
            }
            return users;

        }
        public List<Users> SearchUserByName(string searchTerm)
        {
            string query = "SELECT * FROM Users WHERE Name LIKE @SearchTerm";
            searchTerm = '%' + searchTerm + '%';

            DataTable result = Con.GetDataWithParameters(query, searchTerm);

            List<Users> users = new List<Users>();

            foreach (DataRow row in result.Rows)
            {
                Users user = new Users(
                    Convert.ToInt32(row["ID"]),
                    row["Name"].ToString(),
                    row["Password"].ToString(),
                    row["Email"].ToString(),
                    row["Address"].ToString(),
                    row["Phone"].ToString(),
                    row["Designation"].ToString(),
                    row["Status"].ToString()
                );
                users.Add(user);
            }

            return users;
        }
        public bool IsEmailExists(string email)
        {
            string emailCheckQuery = $"SELECT COUNT(*) FROM Users WHERE Email = '{email}' and Status = 'ACTIVE' ";
            int emailCount = (int)Con.GetData(emailCheckQuery).Rows[0][0];
            return emailCount > 0;
        }
        public bool AddCustomer(Customer customer)
        {

            if (IsEmailExists(customer.GetEmail()))
            {
                return false;
            }
            try
            {
                string Query = string.Format(@" Insert into Users(Name, Email, Password, Address, Designation, Phone, Status) 
                                           Values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                                          customer.GetName(), customer.GetEmail(), customer.GetPassword(),
                                                          customer.GetAddress(), customer.GetDesignation(), customer.GetPhone(),
                                                          customer.GetStatus());
                int RowAffected = Con.SetData(Query);
                if (RowAffected > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;

        }
        public bool UpdateCustomer(Customer customer)
        {

            try
            {
                string Query = string.Format(@"UPDATE Users 
                     SET Name = '{0}', Email = '{1}', Password = '{2}' , 
                         Address = '{3}' , Designation = '{4}' , 
                         Phone = '{5}' , Status = '{6}' 
                     WHERE Email = '{7}' ",
                                                          customer.GetName(), customer.GetEmail(), customer.GetPassword(),
                                                          customer.GetAddress(), customer.GetDesignation(), customer.GetPhone(),
                                                          customer.GetStatus(), customer.GetEmail());

                int RowAffected = Con.SetData(Query);
                if (RowAffected > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        public bool AddSeller(Seller seller)
        {

            if (IsEmailExists(seller.GetEmail()))
            {
                return false;
            }
            else
            {
                try
                {
                    string userQuery = string.Format(@"INSERT INTO Users(Name, Email, Password, Address, Phone, Designation, Status) 
                                               VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                                                      seller.GetName(), seller.GetEmail(), seller.GetPassword(),
                                                      seller.GetAddress(), seller.GetPhone(), seller.GetDesignation(),
                                                      seller.GetStatus());

                    int userRowsAffected = Con.SetData(userQuery);

                    if (userRowsAffected > 0)
                    {
                        try
                        {
                            string sellerQuery = string.Format(@"INSERT INTO Sellers(UserID, Title, Address) 
                                                     VALUES (IDENT_CURRENT('Users'), '{0}', '{1}')",
                                                                 seller.GetStoreName(), seller.GetStoreAddress());

                            int RowAffected = Con.SetData(sellerQuery);
                            if (RowAffected > 0)
                            {
                                return true;
                            }
                        }
                        catch
                        {
                            return false;
                        }
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
                return false;
            }
        }
        public Seller GetSeller(Users User)
        {
            string Query = string.Format(@"select * from Sellers Where UserID = '{0}'", User.GetID());

            DataTable Data = Con.GetData(Query);
            if (Data.Rows.Count > 0)
            {
                DataRow sellerRow = Data.Rows[0];
                string storeName = sellerRow["Title"].ToString();
                string storeAddress = sellerRow["Address"].ToString();
                Seller Seller = new Seller(User.GetID(), User.GetName(), User.GetPassword(), User.GetEmail(), User.GetAddress(), User.GetPhone(), User.GetDesignation(), User.GetStatus(), storeName, storeAddress);
                return Seller;
            }
            return null;
        }

        public Customer GetCustomer(Users User)
        {

            Customer Seller = new Customer(User.GetID(), User.GetName(), User.GetPassword(), User.GetEmail(), User.GetAddress(), User.GetPhone(), User.GetDesignation(), User.GetStatus());
            return Seller;
        }
        public Employees GetEmployee(Users User)
        {

            Employees Seller = new Employees(User.GetID(), User.GetName(), User.GetPassword(), User.GetEmail(), User.GetAddress(), User.GetPhone(), User.GetDesignation(), User.GetStatus());
            return Seller;
        }
        public Users LogIn(Users User)
        {
            string query = $"SELECT * FROM Users WHERE Email = '{User.GetEmail()}' AND Password = '{User.GetPassword()}' and Status = 'ACTIVE' ";
            DataTable result = Con.GetData(query);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                int userId = Convert.ToInt32(row["ID"]);
                string name = row["Name"].ToString();
                string address = row["Address"].ToString();
                string phone = row["Phone"].ToString();
                string designation = row["Designation"].ToString();
                string status = row["Status"].ToString();
                if (designation == "SELLER")
                {
                    string sellerQuery = $"SELECT * FROM Sellers WHERE UserID = {userId}";
                    DataTable sellerResult = Con.GetData(sellerQuery);

                    if (sellerResult.Rows.Count > 0)
                    {
                        DataRow sellerRow = sellerResult.Rows[0];
                        string storeName = sellerRow["Title"].ToString();
                        string storeAddress = sellerRow["Address"].ToString();

                        Seller Sller = new Seller(userId, name, User.GetPassword(), User.GetEmail(), address, phone, designation, status, storeName, storeAddress);

                        return Sller;
                    }
                }
                else if (designation == "CUSTOMER")
                {
                    Customer customer = new Customer(userId, name, User.GetPassword(), User.GetEmail(), address, phone, designation, status);

                    return customer;
                }
                else if (designation == "EMPLOYEE")
                {
                    Employees employee = new Employees(userId, name, User.GetPassword(), User.GetEmail(), address, phone, designation, status);

                    return employee;
                }
            }
            return null;
        }
    }
}
