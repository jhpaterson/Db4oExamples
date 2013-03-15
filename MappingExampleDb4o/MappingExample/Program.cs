using System;
using System.Collections.Generic;
using System.Linq;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Query;



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

            
            // Open embedded server and open client for that server
            using (IObjectServer server = Db4oClientServer.OpenServer(GetDbFileName(), 0))
            {
                using (IObjectContainer client = server.OpenClient())
                {
                    // set activation depth (default = 5)
                    client.Ext().Configure().ActivationDepth(1);

                    // QBE
                    Employee target = new Employee { Name = "Felipe" };
                    IObjectSet qberesult = client.QueryByExample(target);
                    Employee emp = (Employee)qberesult.Next();
                    Console.WriteLine(emp.Name + ", " + emp.Address.PropertyName);

                    // activate a property dynamically
                    Address empAddress = emp.Address;
                    client.Activate(empAddress, 1);

                    // NQ (type-safe)
                    IList<Project> nqresult = client.Query<Project>(delegate(Project pr)
                    {
                        return pr.ProjectName.StartsWith("Se");
                    });
                    Project projnq = nqresult.First<Project>();
                    Console.WriteLine(projnq.ProjectName);

                    // SODA
                    IQuery sodaquery = client.Query();
                    sodaquery.Constrain(typeof(Project));
                    sodaquery.Descend("projectName")        // name of field, not property
                        .Constrain("Se")
                        .StartsWith(true);                  // case sensitive
                    Project projsoda = (Project)sodaquery.Execute().Next();
                    Console.WriteLine(projsoda.ProjectName);


                    // LINQ (type-safe)
                    var projectQuery = from Project p in client
                                        where p.ProjectName.StartsWith("Se")
                                        select p;
                    Project proj = projectQuery.First<Project>();
                    Console.WriteLine(proj.ProjectName);

                    var projectQuery2 = from Project p in client
                                        where p.ProjectName.StartsWith("Se")
                                        select new { 
                                            name = p.ProjectName, 
                                            headcount = p.Employees.Count 
                                        };
                    var proj2 = projectQuery2.First();
                    Console.WriteLine(proj2.name + ", " + proj2.headcount);

                    // wait for key press before ending
                    Console.ReadLine();

                    server.Dispose();
                }
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
                Employees = new List<Employee> { nico }
            };
            Address linqTower = new Address
            {
                PropertyName = "LINQ Tower",
                PropertyNumber = 9,
                PostCode = "KA1 1XX",
                Employees = new List<Employee> { fernando, felipe }
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
                Employees = new List<Employee> { felipe, nico }
            };
            Project financeSystem = new Project
            {
                ProjectID = 2,
                ProjectName = "Finance System",
                Employees = new List<Employee> { fernando }
            };
            Project secret = new Project
            {
                ProjectID = 3,
                ProjectName = "Secret",
                Employees = new List<Employee> { fernando, felipe }
            };

            // SET PROJECTS FOR EMPLOYEES
            fernando.Projects = new List<Project> { financeSystem, secret };
            felipe.Projects = new List<Project> { webShop, secret };
            nico.Projects = new List<Project> { webShop };

            // OBJECT GRAPH
            List<Employee> employees = new List<Employee> { fernando, felipe, nico };

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
