using System;

namespace DarazLibrary
{
    public class Reviews
    {

        private int ReviewID;
        private int ProductID;
        private int UserID;
        private string ReviewerName;
        private string ReviewContent;
        private DateTime ReviewDate;


        public Reviews(int reviewID, int productID, int userID, string reviewerName, string reviewContent, DateTime reviewDate)
        {
            this.ReviewID = reviewID;
            this.ProductID = productID;
            this.UserID = userID;
            this.ReviewerName = reviewerName;
            this.ReviewContent = reviewContent;
            this.ReviewDate = reviewDate;
        }
        public Reviews(int productID, int userID, string reviewerName, string reviewContent, DateTime reviewDate)
        {
            this.ProductID = productID;
            this.UserID = userID;
            this.ReviewerName = reviewerName;
            this.ReviewContent = reviewContent;
            this.ReviewDate = reviewDate;
        }

        public int GetReviewID() { return this.ReviewID; }
        public void SetReviewID(int value) { this.ReviewID = value; }

        public int GetProductID() { return this.ProductID; }
        public void SetProductID(int value) { this.ProductID = value; }

        public int GetUserID() { return this.UserID; }
        public void SetUserID(int value) { this.UserID = value; }

        public string GetReviewerName() { return this.ReviewerName; }
        public void SetReviewerName(string value) { this.ReviewerName = value; }

        public string GetReviewContent() { return this.ReviewContent; }
        public void SetReviewContent(string value) { this.ReviewContent = value; }

        public DateTime GetReviewDate() { return this.ReviewDate; }
        public void SetReviewDate(DateTime value) { this.ReviewDate = value; }
    }
}
