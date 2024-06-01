using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DarazLibrary.DL.FH
{
    public class CategoryCrudFH //: ICategoryCrud
    {
        string filePath = @"C:\path\to\your\file\categories.txt"; // Path to your text file

      
        public void DeleteCategory(string Name)
        {
            Name = Name.ToUpper(); // Convert category name to uppercase

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            // Read all existing categories from the file
            List<string> categories = File.ReadAllLines(filePath).ToList();

            // Check if the category to be deleted exists
            if (categories.Contains(Name))
            {
                // Remove the specified category
                categories.Remove(Name);

                // Write the updated list of categories back to the file
                File.WriteAllLines(filePath, categories);
                Console.WriteLine("Category deleted successfully.");
            }
            else
            {
                Console.WriteLine("Category not found.");
            }
        }

        public void AddCategory(string Name)
        {
            Name = Name.ToUpper(); // Convert category name to uppercase

            // Check if the file exists, and create it if it doesn't
            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    // Write the category name to the file
                    sw.WriteLine(Name);
                }
            }
            else
            {
                // Append the category name to the existing file
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    // Write the category name to the file
                    sw.WriteLine(Name);
                }
            }
        }
        public List<string> ShowCategory()
        {
            List<string> categories = new List<string>();

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Add each line (category) to the list of categories
                foreach (string line in lines)
                {
                    categories.Add(line);
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }

            return categories;
        }
    }
}
