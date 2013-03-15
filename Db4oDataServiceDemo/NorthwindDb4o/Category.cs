using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindDb4o
{
    public class Category
    {
        long categoryID;
        string categoryName;
        string description;
        Byte[] picture;

        public long CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public Byte[] Picture
        {
            get { return picture; }
            set { picture = value; }
        }
    }
}
