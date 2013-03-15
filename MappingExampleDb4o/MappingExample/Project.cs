using System.Collections.Generic;

namespace MappingExample
{
    public class Project
    {
        private int projectID;
        private string projectName;
        private ICollection<Employee> employees;

        public virtual int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }


        public ICollection<Employee> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public Project()
        {
        }

        public Project(string projectName)
        {
            this.projectName = projectName;
        }
    }
}
