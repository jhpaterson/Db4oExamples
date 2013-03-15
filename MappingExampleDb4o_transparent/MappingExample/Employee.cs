using System.Collections.Generic;

namespace MappingExample
{
    public class Employee 
    {
        protected int employeeID;
        protected string name;
        protected string username;
        protected string phoneNumber;
        protected Address address;
        protected Employee supervisor;
        private ICollection<Project> projects;

        public int EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public Employee Supervisor
        {
            get { return supervisor; }
            set { supervisor = value; }
        }

        public ICollection<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }

        public Employee()
        {
        }

        public Employee(string name,
                        string username,
                        string phoneNumber)
        {
            this.name = name;
            this.username = username;
            this.phoneNumber = phoneNumber;
        }
    }
}
