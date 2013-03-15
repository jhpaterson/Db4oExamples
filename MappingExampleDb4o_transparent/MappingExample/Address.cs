using System.Collections.Generic;

namespace MappingExample
{

    public class Address
    {
        //private int addressID;
        private string propertyName;
        private int propertyNumber;
        private string postCode;
        private ICollection<Employee> employees;
      
        //public virtual int AddressID
        //{
        //    get { return addressID; }
        //    set { addressID = value; }
        //}

        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        public int PropertyNumber
        {
            get { return propertyNumber; }
            set { propertyNumber = value; }
        }

        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        public ICollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public Address()
        {
        }

        public Address(string propertyName,
                        int propertyNumber,
                        string postCode)
        {
            this.propertyName = propertyName;
            this.propertyNumber = propertyNumber;
            this.postCode = postCode;
        }
    }
}
