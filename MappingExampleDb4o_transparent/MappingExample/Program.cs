using System;
using System.Collections.Generic;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.TA;
using Db4objects.Db4o.Collections;



namespace MappingExample
{
    class Program
    {
        private static string GetDbFileName()
        {
            string dbFileName = "company.db4o";
            return dbFileName;
        }

        static void Main(string[] args)
        {
            // SetupObjects();

            
            IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
            config.Common.Add(new TransparentActivationSupport());
            using (IObjectContainer db = Db4oEmbedded.OpenFile(GetDbFileName()))
            {

                // QBE
                Employee target = new Employee { Name = "Felipe" };
                IObjectSet qberesult = db.QueryByExample(target);
                Employee emp = (Employee)qberesult.Next();
                Console.WriteLine(emp.Name + ", " + emp.Address.PropertyName);
                foreach (Project proj in emp.Projects)
                {
                    Console.WriteLine(proj.ProjectName);
                }

                // NQ 
                IList<Project> nqresult = db.Query<Project>(delegate(Project pr)
                {
                    return pr.ProjectName.StartsWith("Se");
                });
                Project projnq = nqresult.First<Project>();
                Console.WriteLine(projnq.ProjectName);

                // wait for key press before ending
                Console.ReadLine();

                db.Dispose();
            }
                
        }

            

        static void SetupObjects()
        {
            // EMPLOYEES
            Employee fernando = new SalariedEmployee
            {
                EmployeeID = 1,
                Name = "Fernando",
                Username = "fernando",
                PhoneNumber = "9999",
                PayGrade = 7
            };
            Employee felipe = new HourlyPaidEmployee
            {
                EmployeeID = 2,
                Name = "Felipe",
                Username = "felipe",
                PhoneNumber = "8888",
                Supervisor = fernando
            };
            Employee nico = new HourlyPaidEmployee
            {
                EmployeeID = 3,
                Name = "Nico",
                Username = "nico",
                PhoneNumber = "7777",
                Supervisor = felipe
            };

            // ADDRESSES
            Address ormHouse = new Address
            {
                PropertyName = "ORM House",
                PropertyNumber = 1,
                PostCode = "G4 0BA",
                Employees = new ArrayList4<Employee> { nico }           // db4o activateable collections
            };
            Address linqTower = new Address
            {
                PropertyName = "LINQ Tower",
                PropertyNumber = 9,
                PostCode = "KA1 1XX",
                Employees = new ArrayList4<Employee> { fernando, felipe }
            };

            // SET ADDRESSES FOR EMPLOYEES
            fernando.Address = linqTower;
            felipe.Address = linqTower;
            nico.Address = ormHouse;

            // PROJECTS
            Project webShop = new Project
            {
                ProjectID = 1,
                ProjectName = "Web Shop",
                Employees = new ArrayList4<Employee> { felipe, nico }
            };
            Project financeSystem = new Project
            {
                ProjectID = 2,
                ProjectName = "Finance System",
                Employees = new ArrayList4<Employee> { fernando }
            };
            Project secret = new Project
            {
                ProjectID = 3,
                ProjectName = "Secret",
                Employees = new ArrayList4<Employee> { fernando, felipe }
            };

            // SET PROJECTS FOR EMPLOYEES
            fernando.Projects = new ArrayList4<Project> { financeSystem, secret };
            felipe.Projects = new ArrayList4<Project> { webShop, secret };
            nico.Projects = new ArrayList4<Project> { webShop };

            // OBJECT GRAPH
            IList<Employee> employees = new ArrayList4<Employee> { fernando, felipe, nico };

            // STORE OBJECT GRAPH
            string dbFileName = GetDbFileName();

            using (IObjectContainer db = Db4oEmbedded.OpenFile(dbFileName))
            {
                foreach (Employee emp in employees)
                {
                    db.Store(emp);
                }
            }
        }
    }
}
