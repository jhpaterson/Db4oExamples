using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindDb4o
{
    public class Region
    {
        long regionID;
        string regionDescription;

        public long RegionID
        {
            get { return regionID; }
            set { regionID = value; }
        }

        public string RegionDescription
        {
            get { return regionDescription; }
            set { regionDescription = value; }
        }
    }
}
