using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindDb4o
{
    public class Territory
    {
        string territoryID;
        string territoryDescription;
        Region regionID;

        public string TerritoryID
        {
            get { return territoryID; }
            set { territoryID = value; }
        }

        public string TerritoryDescription
        {
            get { return territoryDescription; }
            set { territoryDescription = value; }
        }

        public Region RegionID
        {
            get { return regionID; }
            set { regionID = value; }
        }
    }
}
