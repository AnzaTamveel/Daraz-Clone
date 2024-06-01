
using System.Collections.Generic;
using System.Data;

namespace DarazLibrary
{
    public class CategoryCrudDB : ICategoryCrud
    {


        public CategoryCrudDB()
        {
        }
        static CategoryCrudDB Instance;
        public static CategoryCrudDB GetCategoryCrudDB()
        {
            if (Instance == null)
            {
                Instance = new CategoryCrudDB();

            }
            return Instance;
        }








        static FunctionsDB Con = new FunctionsDB();


        public List<string> ShowCategory()
        {
            string Query = string.Format(@"SELECT * FROM Category");
            DataTable result = Con.GetData(Query);

            List<string> categories = new List<string>();
            foreach (DataRow row in result.Rows)
            {
                string categoryName = row["Categories"].ToString();
                categories.Add(categoryName);
            }
            return categories;
        }
        public void AddCategory(string Name)
        {
            Name = Name.ToUpper();
            string Query = string.Format(@"INSERT INTO Category (Categories) VALUES ('{0}')", Name);
            Con.SetData(Query);

        }
        public void DeleteCategory(string Name)
        {
            string Query = string.Format(@"DELETE FROM Category WHERE Categories = '{0}'", Name);
            Con.SetData(Query);

        } 
      
    
    }
}
