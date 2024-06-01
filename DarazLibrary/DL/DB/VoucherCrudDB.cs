
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class VoucherCrudDB : IVoucherCrud
    {


        string connectionstring = Utility.GetConnectionString();

        public VoucherCrudDB()
        {
        }
        static VoucherCrudDB Instance;
        public static VoucherCrudDB GetVoucherCrudDB()
        {
            if (Instance == null)
            {
                Instance = new VoucherCrudDB();

            }
            return Instance;
        }



        static FunctionsDB Con = new FunctionsDB();
        public  List<Voucher> GetVoucherOfProduct(Products product)
        {
            List<Voucher> vouchers = new List<Voucher>();
            string Query = string.Format(@" SELECT V.* 
                  FROM Vouchers V
                  JOIN Products P ON V.SellerID = P.SellerID
                  WHERE P.ProductID = '{0}'", product.GetID());
            DataTable Table = Con.GetData(Query);
            foreach (DataRow row in Table.Rows)
            {
                Voucher voucher = new Voucher(
                   int.Parse(row["VoucherCode"].ToString()),
                    decimal.Parse(row["DiscountAmount"].ToString()),
                    DateTime.Parse(row["ExpiryDate"].ToString()),
                    row["IsActive"].ToString(),
                    float.Parse(row["Validation"].ToString()),
                    int.Parse(row["SellerID"].ToString())
                );
                vouchers.Add(voucher);
            }
            return vouchers;


        }
        public  List<Voucher> LoadVouchersOfSeller(int ID)
        {

            string UpdatingQuery = string.Format(@"UPDATE Vouchers SET IsActive = 'EXPIRED' WHERE ExpiryDate < '{0}' ", DateTime.Now);
            Con.SetData(UpdatingQuery);


            string Query = string.Format(@"SELECT * FROM vouchers WHERE SellerID = '{0}'" , ID);
            DataTable result = Con.GetData(Query);

            List<Voucher> vouchers = new List<Voucher>();
            foreach (DataRow row in result.Rows)
            {
                Voucher voucher = new Voucher(
                   int.Parse(row["VoucherCode"].ToString()),
                    decimal.Parse(row["DiscountAmount"].ToString()),
                    DateTime.Parse(row["ExpiryDate"].ToString()),
                    row["IsActive"].ToString(),
                    float.Parse(row["Validation"].ToString()),
                    int.Parse(row["SellerID"].ToString())
                );
                vouchers.Add(voucher);
            }
            return vouchers;
        }
        public  void AddVoucher(Voucher voucher)
        {
            string Query = string.Format(@"INSERT INTO Vouchers (DiscountAmount, ExpiryDate, IsActive, Validation, SellerID)
                                   VALUES ({0}, '{1}', '{2}', {3}, {4})",
                                   voucher.GetDiscountAmount(),
                                   voucher.GetExpiryDate().ToString("yyyy-MM-dd"),
                                   voucher.GetIsActive(),
                                   voucher.GetValidation(),
                                   voucher.GetUserID());

            Con.SetData(Query);

        }

        public  void DeleteVoucher(Voucher voucher)
        {
            string Query = string.Format(@"delete from Vouchers where VoucherCode = '{0}'", voucher.GetCode());
            Con.SetData(Query);
        }
        public  void UpdateVoucher(Voucher voucher)
        {
            string Query = string.Format("UPDATE Vouchers SET DiscountAmount = '{1}', ExpiryDate = '{2}', IsActive = '{3}', Validation = '{4}' WHERE VoucherCode = '{0}'",
                                            voucher.GetCode(),
                                            voucher.GetDiscountAmount(),
                                            voucher.GetExpiryDate().ToString("yyyy-MM-dd"), 
                                            voucher.GetIsActive(),
                                            voucher.GetValidation());
            Con.SetData(Query);
        }
    }
}
