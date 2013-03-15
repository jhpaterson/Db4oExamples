using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthwindDb4o;

namespace ConsoleClient
{   /* For more advanced operations see: http://msdn.microsoft.com/en-us/library/cc668789.aspx */
    class Program
    {
        static void Main(string[] args)
        {
            ManagementDataContext ctx = 
                new ManagementDataContext(new Uri("http://localhost:32767/NorthwindData.svc"));
            var query = ctx.Categories;

            /*
             * Note that you can use a full LINQ query here over the datastore like
            
                var query = from c in ctx.Customers 
                    where c.City == "London"
                    orderby c.CompanyName
                    select c;
             * 
             */

            Console.WriteLine("List of original Categories:");
            long maxId = 0;
            foreach (Category c in query)
            {
                Console.WriteLine("Name: {0} / Description: {1}", c.CategoryName, c.Description);
                if (c.CategoryID > maxId)
                    maxId = c.CategoryID;
            }

            //Insert test
            Category newCat = new Category();
            newCat.CategoryID = maxId + 1;
            newCat.CategoryName = "Dietary Supplements " + newCat.CategoryID.ToString();
            newCat.Description = "Just added!";

            ctx.AddObject("Categories", newCat);
            //Or you can also use the generated shortcut -> svc.AddToCategories(newCat);
            ctx.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine("After Inserting Category("+newCat.CategoryID.ToString()+"):");
            foreach (Category c in query)
            {
                Console.WriteLine("Name: {0} / Description: {1}", c.CategoryName, c.Description);
            }

            //Update test
            newCat.Description = "Just updated!";
            ctx.UpdateObject(newCat);
            ctx.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine("After Updating Category(" + newCat.CategoryID.ToString() + "):");
            foreach (Category c in query)
            {
                Console.WriteLine("Name: {0} / Description: {1}", c.CategoryName, c.Description);
            }

            //Delete test
            ctx.DeleteObject(newCat);
            ctx.SaveChanges();

            Console.WriteLine("");
            Console.WriteLine("After Deleting Category(" + newCat.CategoryID.ToString() + "):");
            foreach (Category c in query)
            {
                Console.WriteLine("Name: {0} / Description: {1}", c.CategoryName, c.Description);
            }

            Console.ReadLine();
        }
    }
}
