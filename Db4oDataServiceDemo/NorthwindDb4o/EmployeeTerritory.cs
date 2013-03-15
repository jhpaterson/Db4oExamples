using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NorthwindDb4o
{
    public class EmployeeTerritory
    {
        Employee employeeID;
        Territory territoryID;

        public string EmployeeTerritoryID
        {
            get { return employeeID.EmployeeID.ToString() + "-" + territoryID.TerritoryID.ToString(); }
        }

        public Employee EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public Territory TerritoryID
        {
            get { return territoryID; }
            set { territoryID = value; }
        }
    }
}
