using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarazLibrary
{
    public class Voucher
    {
        private int Code;
        private decimal DiscountAmount;
        private DateTime ExpiryDate;
        private string IsActive;
        private float Validation;
        private int UserID;
        public Voucher(int code, decimal discount, DateTime date, string status, float valid, int Id)
        {
            this.Code = code;
            this.DiscountAmount = discount;
            this.ExpiryDate = date;
            this.IsActive = status;
            this.Validation = valid;
            this.UserID = Id;
        }
        public Voucher(decimal discount, DateTime date, string status, float valid, int Id)
        {
            this.DiscountAmount = discount;
            this.ExpiryDate = date;
            this.IsActive = status;
            this.Validation = valid;
            this.UserID = Id;
        }
        public int GetCode() { return this.Code; }
        public void SetCode(int code) { this.Code = code; }

        public decimal GetDiscountAmount() { return this.DiscountAmount; }
        public void SetDiscountAmount(decimal discount) { this.DiscountAmount = discount; }

        public DateTime GetExpiryDate() { return this.ExpiryDate; }
        public void SetExpiryDate(DateTime date) { this.ExpiryDate = date; }

        public string GetIsActive() { return this.IsActive; }
        public void SetIsActive(string status) { this.IsActive = status; }

        public float GetValidation() { return this.Validation; }
        public void SetValidation(float valid) { this.Validation = valid; }

        public int GetUserID() { return this.UserID; }
        public void SetUserID(int id) { this.UserID = id; }
    }
}
