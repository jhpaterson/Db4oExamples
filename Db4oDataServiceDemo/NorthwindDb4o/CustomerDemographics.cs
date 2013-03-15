using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindDb4o
{
    [System.Data.Services.Common.DataServiceKey("CustomerTypeID")]
    public class CustomerDemographics
    {
        string customerTypeID;
        string customerDesc;

        public string CustomerTypeID
        {
            get { return customerTypeID; }
            set { customerTypeID = value; }
        }

        public string CustomerDesc
        {
            get { return customerDesc; }
            set { customerDesc = value; }
        }
    }
}
