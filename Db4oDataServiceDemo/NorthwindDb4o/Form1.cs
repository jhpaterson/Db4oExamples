using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NorthwindDb4o.NorthwindDataSetTableAdapters;
using Db4objects;
using Db4objects.Db4o;
using Db4objects.Db4o.Config;
using System.Collections;

namespace NorthwindDb4o
{
    public partial class Form1 : Form
    {
        NorthwindDataSet ds;
        IObjectContainer container;
        private static string basePath = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).Substring(6);
        private static string DB4O_FILE = basePath + "\\northwind.db4o";

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            if (File.Exists(DB4O_FILE))
                File.Delete(DB4O_FILE);
            ds = new NorthwindDataSet();
            container = Db4oFactory.OpenFile(Configuration(), DB4O_FILE);

            CopyCustomers();
            CopyCategories();
            CopySuppliers();
            CopyShippers();
            CopyRegions();
            CopyEmployees();
            CopyCustomerDemographics();
            CopyCustomerCustomerDemo();
            CopyTerritories();
            CopyEmployeeTerritories();
            CopyProducts();
            CopyOrders();
            CopyOrderDetails();
            Terminate();
        }

        private IConfiguration Configuration()
        {
            IConfiguration config = Db4oFactory.NewConfiguration();

            //Set indexes
            config.ObjectClass(typeof(Customer)).ObjectField("customerID").Indexed(true);
            config.ObjectClass(typeof(Category)).ObjectField("categoryID").Indexed(true);
            config.ObjectClass(typeof(Supplier)).ObjectField("supplierID").Indexed(true);
            config.ObjectClass(typeof(Region)).ObjectField("cregionID").Indexed(true);
            config.ObjectClass(typeof(Employee)).ObjectField("employeeID").Indexed(true);
            config.ObjectClass(typeof(Shipper)).ObjectField("shipperID").Indexed(true);
            config.ObjectClass(typeof(CustomerDemographics)).ObjectField("customerTypeID").Indexed(true);
            config.ObjectClass(typeof(CustomerCustomerDemo)).ObjectField("customerID").Indexed(true);
            config.ObjectClass(typeof(CustomerCustomerDemo)).ObjectField("customerTypeID").Indexed(true);
            config.ObjectClass(typeof(Territory)).ObjectField("territoryID").Indexed(true);
            config.ObjectClass(typeof(Territory)).ObjectField("regionID").Indexed(true);
            config.ObjectClass(typeof(EmployeeTerritory)).ObjectField("territoryID").Indexed(true);
            config.ObjectClass(typeof(EmployeeTerritory)).ObjectField("employeeID").Indexed(true);
            config.ObjectClass(typeof(Product)).ObjectField("productID").Indexed(true);
            config.ObjectClass(typeof(Product)).ObjectField("supplierID").Indexed(true);
            config.ObjectClass(typeof(Product)).ObjectField("categoryID").Indexed(true);
            config.ObjectClass(typeof(Order)).ObjectField("orderID").Indexed(true);
            config.ObjectClass(typeof(Order)).ObjectField("customerID").Indexed(true);
            config.ObjectClass(typeof(Order)).ObjectField("employeeID").Indexed(true);
            config.ObjectClass(typeof(Order)).ObjectField("shipVia").Indexed(true);
            config.ObjectClass(typeof(OrderDetail)).ObjectField("orderID").Indexed(true);
            config.ObjectClass(typeof(OrderDetail)).ObjectField("productID").Indexed(true);

            //Set cascades
            config.ObjectClass(typeof(Employee)).CascadeOnUpdate(true);
            config.ObjectClass(typeof(CustomerCustomerDemo)).CascadeOnUpdate(true);
            config.ObjectClass(typeof(EmployeeTerritory)).CascadeOnUpdate(true);
            config.ObjectClass(typeof(Product)).CascadeOnUpdate(true);
            config.ObjectClass(typeof(Order)).CascadeOnUpdate(true);
            config.ObjectClass(typeof(OrderDetail)).CascadeOnUpdate(true);

            return config;
        }

        public void Terminate()
        {
            if (ds != null)
                ds.Dispose();
            if (container != null)
                container.Close();
        }

        private void LogMessage(string msg, bool linefeed)
        {
            if (linefeed)
                msg = msg + Environment.NewLine;
            textBox1.AppendText(msg);
        }

        public void CopyCustomers()
        {
            //Processing Customers
            LogMessage("Reading Customers...", false);
            CustomersTableAdapter adapter1 = new CustomersTableAdapter();
            NorthwindDb4o.NorthwindDataSet.CustomersDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.CustomersRow row in table1)
            {
                LogMessage("Customer: " + row.CustomerID.ToString() + " ...", false);
                
                Customer c = new Customer();

                c.CustomerID = row.CustomerID;
                c.CompanyName = row.CompanyName;
                c.ContactName = row.IsContactNameNull() ? null : row.ContactName;
                c.ContactTitle = row.IsContactTitleNull() ? null : row.ContactTitle;
                c.Address = row.IsAddressNull() ? null : row.Address;
                c.City = row.IsCityNull() ? null : row.City;
                c.Region = row.IsRegionNull() ? null : row.Region;
                c.PostalCode = row.IsPostalCodeNull() ? null : row.PostalCode;
                c.Country = row.IsCountryNull() ? null : row.Country;
                c.Phone = row.IsPhoneNull() ? null : row.Phone;
                c.Fax = row.IsFaxNull() ? null : row.Fax;

                container.Store(c);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Customer)).Count;
            if(table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Customers" + Environment.NewLine, true);
        }

        public void CopyCategories()
        {
            //Processing Categories
            LogMessage("Reading Categories...", false);
            CategoriesTableAdapter adapter1 = new CategoriesTableAdapter();
            NorthwindDb4o.NorthwindDataSet.CategoriesDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.CategoriesRow row in table1)
            {
                LogMessage("Categories: " + row.CategoryID.ToString() + " ...", false);

                Category c = new Category();

                c.CategoryID = row.CategoryID;
                c.CategoryName = row.CategoryName;
                c.Description = row.IsDescriptionNull() ? null : row.Description;
                c.Picture = row.IsPictureNull() ? null : row.Picture;

                container.Store(c);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Category)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Categories" + Environment.NewLine, true);
        }

        public void CopySuppliers()
        {
            //Processing Suppliers
            LogMessage("Reading Suppliers...", false);
            SuppliersTableAdapter adapter1 = new SuppliersTableAdapter();
            NorthwindDb4o.NorthwindDataSet.SuppliersDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.SuppliersRow row in table1)
            {
                LogMessage("Supplier: " + row.SupplierID.ToString() + " ...", false);

                Supplier s = new Supplier();

                s.SupplierID = row.SupplierID;
                s.CompanyName = row.CompanyName;
                s.ContactName = row.IsContactNameNull() ? null : row.ContactName;
                s.ContactTitle = row.IsContactTitleNull() ? null : row.ContactTitle;
                s.Address = row.IsAddressNull() ? null : row.Address;
                s.City = row.IsCityNull() ? null : row.City;
                s.Region = row.IsRegionNull() ? null : row.Region;
                s.PostalCode = row.IsPostalCodeNull() ? null : row.PostalCode;
                s.Country = row.IsCountryNull() ? null : row.Country;
                s.Phone = row.IsPhoneNull() ? null : row.Phone;
                s.Fax = row.IsFaxNull() ? null : row.Fax;
                s.HomePage = row.IsHomePageNull() ? null : row.HomePage;

                container.Store(s);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Supplier)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Suppliers" + Environment.NewLine, true);
        }

        public void CopyShippers()
        {
            //Processing Shippers
            LogMessage("Reading Shippers...", false);
            ShippersTableAdapter adapter1 = new ShippersTableAdapter();
            NorthwindDb4o.NorthwindDataSet.ShippersDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.ShippersRow row in table1)
            {
                LogMessage("Shippers: " + row.ShipperID.ToString() + " ...", false);

                Shipper s = new Shipper();

                s.ShipperID = row.ShipperID;
                s.CompanyName = row.CompanyName;
                s.Phone = row.IsPhoneNull() ? null : row.Phone;

                container.Store(s);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Shipper)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Shippers" + Environment.NewLine, true);
        }

        public void CopyRegions()
        {
            //Processing Regions
            LogMessage("Reading Regions...", false);
            RegionTableAdapter adapter1 = new RegionTableAdapter();
            NorthwindDb4o.NorthwindDataSet.RegionDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.RegionRow row in table1)
            {
                LogMessage("Regions: " + row.RegionID.ToString() + " ...", false);

                Region r = new Region();

                r.RegionID = row.RegionID;
                r.RegionDescription = row.RegionDescription;

                container.Store(r);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Region)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Regions" + Environment.NewLine, true);
        }

        public void CopyCustomerDemographics()
        {
            //Processing CustomerDemographics
            LogMessage("Reading CustomerDemographics...", false);
            CustomerDemographicsTableAdapter adapter1 = new CustomerDemographicsTableAdapter();
            NorthwindDb4o.NorthwindDataSet.CustomerDemographicsDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.CustomerDemographicsRow row in table1)
            {
                LogMessage("CustomerDemographics: " + row.CustomerTypeID.ToString() + " ...", false);

                CustomerDemographics cd = new CustomerDemographics();

                cd.CustomerTypeID = row.CustomerTypeID;
                cd.CustomerDesc = row.IsCustomerDescNull() ? null : row.CustomerDesc;

                container.Store(cd);
                LogMessage("saved", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(CustomerDemographics)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with CustomerDemographics" + Environment.NewLine, true);
        }

        public void CopyEmployees()
        {
            List<Employee> employees = new List<Employee>();
            Hashtable reportingEmployees = new Hashtable();
            //Processing Employees
            LogMessage("Reading Employees...", false);
            EmployeesTableAdapter adapter1 = new EmployeesTableAdapter();
            NorthwindDb4o.NorthwindDataSet.EmployeesDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.EmployeesRow row in table1)
            {
                Employee e = new Employee();

                e.EmployeeID = row.EmployeeID;
                e.FirstName = row.FirstName;
                e.LastName = row.LastName;
                e.Title = row.IsTitleNull() ? null : row.Title;
                e.TitleOfCourtesy = row.IsTitleOfCourtesyNull() ? null : row.TitleOfCourtesy;
                if (!row.IsBirthDateNull())
                    e.BirthDate = row.BirthDate;
                if (!row.IsHireDateNull())
                    e.HireDate = row.HireDate;
                e.Address = row.IsAddressNull() ? null : row.Address;
                e.City = row.IsCityNull() ? null : row.City;
                e.Region = row.IsRegionNull() ? null : row.Region;
                e.PostalCode = row.IsPostalCodeNull() ? null : row.PostalCode;
                e.Country = row.IsCountryNull() ? null : row.Country;
                e.HomePhone = row.IsHomePhoneNull() ? null : row.HomePhone;
                e.Extension = row.IsExtensionNull() ? null : row.Extension;
                e.Notes = row.IsNotesNull() ? null : row.Notes;
                e.Photo = row.IsPhotoNull() ? null : row.Photo;
                e.PhotoPath = row.IsPhotoPathNull() ? null : row.PhotoPath;
                if (!row.IsReportsToNull())
                    reportingEmployees.Add(e.EmployeeID, row.ReportsTo);

                employees.Add(e);
            }
            foreach (Employee e in employees)
            {
                LogMessage("Employee: " + e.EmployeeID.ToString() + " ...", false);
                if(reportingEmployees.ContainsKey(e.EmployeeID))
                {
                    LogMessage("linking member...", false);
                    long reportsToID = Convert.ToInt64(reportingEmployees[e.EmployeeID]);
                    Employee found = employees.Find(delegate(Employee p) { return p.EmployeeID == reportsToID; });
                    e.ReportsTo = found;
                }
                container.Store(e);
                LogMessage("saved (" + e.EmployeeID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Employee)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Employees" + Environment.NewLine, true);
        }

        public void CopyCustomerCustomerDemo()
        {
            //Processing CustomerCustomerDemo
            LogMessage("Reading CustomerCustomerDemo...", false);
            CustomerCustomerDemoTableAdapter adapter1 = new CustomerCustomerDemoTableAdapter();
            NorthwindDb4o.NorthwindDataSet.CustomerCustomerDemoDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.CustomerCustomerDemoRow row in table1)
            {
                LogMessage("CustomerCustomerDemo: " + row.CustomerID.ToString() + "/" + row.CustomerTypeID.ToString() + " ...", false);

                CustomerCustomerDemo ccd = new CustomerCustomerDemo();
                LogMessage("linking members...", false);
                ccd.CustomerID = (Customer)Db4oUtil.GetByStringID(container, typeof(Customer), "customerID",row.CustomerID);
                ccd.CustomerTypeID = (CustomerDemographics)Db4oUtil.GetByStringID(container, typeof(CustomerDemographics), "customerTypeID", row.CustomerTypeID);

                container.Store(ccd);
                LogMessage("saved (" + ccd.CustomerID.CustomerID.ToString() + "/" + ccd.CustomerTypeID.CustomerTypeID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(CustomerCustomerDemo)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with CustomerCustomerDemo" + Environment.NewLine, true);
        }

        public void CopyTerritories()
        {
            //Processing Territories
            LogMessage("Reading Territories...", false);
            TerritoriesTableAdapter adapter1 = new TerritoriesTableAdapter();
            NorthwindDb4o.NorthwindDataSet.TerritoriesDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.TerritoriesRow row in table1)
            {
                LogMessage("Territories: " + row.TerritoryID.ToString() + " ...", false);

                Territory t = new Territory();

                t.TerritoryID = row.TerritoryID;
                t.TerritoryDescription = row.TerritoryDescription;
                LogMessage("linking member...", false);
                t.RegionID = (Region)Db4oUtil.GetByNumericalID(container, typeof(Region), "regionID", row.RegionID);

                container.Store(t);
                LogMessage("saved (" + t.TerritoryID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Territory)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Territories" + Environment.NewLine, true);
        }

        public void CopyEmployeeTerritories()
        {
            //Processing EmployeeTerritories
            LogMessage("Reading EmployeeTerritories...", false);
            EmployeeTerritoriesTableAdapter adapter1 = new EmployeeTerritoriesTableAdapter();
            NorthwindDb4o.NorthwindDataSet.EmployeeTerritoriesDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.EmployeeTerritoriesRow row in table1)
            {
                LogMessage("EmployeeTerritories: " + row.EmployeeID.ToString() + "/" + row.TerritoryID.ToString() + " ...", false);

                EmployeeTerritory et = new EmployeeTerritory();
                LogMessage("linking members...", false);
                et.EmployeeID = (Employee)Db4oUtil.GetByNumericalID(container, typeof(Employee), "employeeID", row.EmployeeID);
                et.TerritoryID = (Territory)Db4oUtil.GetByStringID(container, typeof(Territory), "territoryID", row.TerritoryID);

                container.Store(et);
                LogMessage("saved (" + et.EmployeeID.EmployeeID.ToString() + "/" + et.TerritoryID.TerritoryID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(EmployeeTerritory)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with EmployeeTerritories" + Environment.NewLine, true);
        }

        public void CopyProducts()
        {
            List<Product> products = new List<Product>();
            Hashtable suppliers = new Hashtable();
            Hashtable categories = new Hashtable();
            //Processing Products
            LogMessage("Reading Products...", false);
            ProductsTableAdapter adapter1 = new ProductsTableAdapter();
            NorthwindDb4o.NorthwindDataSet.ProductsDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.ProductsRow row in table1)
            {
                Product p = new Product();

                p.ProductID = row.ProductID;
                p.ProductName = row.ProductName;
                if (!row.IsSupplierIDNull())
                    suppliers.Add(p.ProductID, row.SupplierID);
                if (!row.IsCategoryIDNull())
                    categories.Add(p.ProductID, row.CategoryID);
                p.QuantityPerUnit = row.IsQuantityPerUnitNull() ? null : row.QuantityPerUnit;
                p.UnitPrice = row.IsUnitPriceNull() ? 0 : Convert.ToDouble(row.UnitPrice);
                p.UnitsInStock = row.IsUnitsInStockNull() ? 0 : row.UnitsInStock;
                p.UnitsOnOrder = row.IsUnitsOnOrderNull() ? 0 : row.UnitsOnOrder;
                p.ReorderLevel = row.IsReorderLevelNull() ? 0 : row.ReorderLevel;
                p.Discontinued = row.Discontinued;

                products.Add(p);
            }
            foreach (Product p in products)
            {
                LogMessage("Product: " + p.ProductID.ToString() + " ...", false);
                if (suppliers.ContainsKey(p.ProductID))
                {
                    LogMessage("linking member...", false);
                    long supplierID = Convert.ToInt64(suppliers[p.ProductID]);
                    Supplier found = (Supplier)Db4oUtil.GetByNumericalID(container, typeof(Supplier), "supplierID", supplierID);
                    p.SupplierID = found;
                }
                if (categories.ContainsKey(p.ProductID))
                {
                    LogMessage("linking member...", false);
                    long categoryID = Convert.ToInt64(categories[p.ProductID]);
                    Category found = (Category)Db4oUtil.GetByNumericalID(container, typeof(Category), "categoryID", categoryID);
                    p.CategoryID = found;
                }
                container.Store(p);
                LogMessage("saved (" + p.ProductID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Product)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Products" + Environment.NewLine, true);
        }

        public void CopyOrders()
        {
            List<Order> orders = new List<Order>();
            Hashtable customers = new Hashtable();
            Hashtable employees = new Hashtable();
            Hashtable shippers = new Hashtable();
            //Processing Orders
            LogMessage("Reading Orders...", false);
            OrdersTableAdapter adapter1 = new OrdersTableAdapter();
            NorthwindDb4o.NorthwindDataSet.OrdersDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.OrdersRow row in table1)
            {
                Order o = new Order();

                o.OrderID = row.OrderID;

                if (!row.IsCustomerIDNull())
                    customers.Add(o.OrderID, row.CustomerID);
                if (!row.IsEmployeeIDNull())
                    employees.Add(o.OrderID, row.EmployeeID);
                if (!row.IsShipViaNull())
                    shippers.Add(o.OrderID, row.ShipVia);
                if (!row.IsOrderDateNull())
                    o.OrderDate = row.OrderDate;
                if (!row.IsRequiredDateNull())
                    o.RequiredDate = row.RequiredDate;
                if (!row.IsShippedDateNull())
                    o.ShippedDate = row.ShippedDate;
                o.Freight = row.IsFreightNull() ? 0 : Convert.ToDouble(row.Freight);
                o.ShipName = row.IsShipNameNull() ? null : row.ShipName;
                o.ShipAddress = row.IsShipAddressNull() ? null : row.ShipAddress;
                o.ShipCity = row.IsShipCityNull() ? null : row.ShipCity;
                o.ShipRegion = row.IsShipRegionNull() ? null : row.ShipRegion;
                o.ShipPostalCode = row.IsShipPostalCodeNull() ? null : row.ShipPostalCode;
                o.ShipCountry = row.IsShipCountryNull() ? null : row.ShipCountry;

                orders.Add(o);
            }
            foreach (Order o in orders)
            {
                LogMessage("Order: " + o.OrderID.ToString() + " ...", false);
                if (customers.ContainsKey(o.OrderID))
                {
                    LogMessage("linking member...", false);
                    string customerID = Convert.ToString(customers[o.OrderID]);
                    Customer found = (Customer)Db4oUtil.GetByStringID(container, typeof(Customer), "customerID", customerID);
                    o.CustomerID = found;
                }
                if (employees.ContainsKey(o.OrderID))
                {
                    LogMessage("linking member...", false);
                    long employeeID = Convert.ToInt64(employees[o.OrderID]);
                    Employee found = (Employee)Db4oUtil.GetByNumericalID(container, typeof(Employee), "employeeID", employeeID);
                    o.EmployeeID = found;
                }
                if (shippers.ContainsKey(o.OrderID))
                {
                    LogMessage("linking member...", false);
                    long shipperID = Convert.ToInt64(shippers[o.OrderID]);
                    Shipper found = (Shipper)Db4oUtil.GetByNumericalID(container, typeof(Shipper), "shipperID", shipperID);
                    o.ShipVia = found;
                }
                container.Store(o);
                LogMessage("saved (" + o.OrderID.ToString() + ")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(Order)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with Orders" + Environment.NewLine, true);
        }

        public void CopyOrderDetails()
        {
            //Processing OrderDetails
            LogMessage("Reading OrderDetails...", false);
            Order_DetailsTableAdapter adapter1 = new Order_DetailsTableAdapter();
            NorthwindDb4o.NorthwindDataSet.Order_DetailsDataTable table1 = adapter1.GetData();
            LogMessage("processing " + table1.Count.ToString() + " rows", true);
            foreach (NorthwindDb4o.NorthwindDataSet.Order_DetailsRow row in table1)
            {
                LogMessage("OrderDetails: " + row.OrderID.ToString() + "/" + row.ProductID.ToString() + " ...", false);

                OrderDetail od = new OrderDetail();
                LogMessage("linking members...", false);
                od.OrderID = (Order)Db4oUtil.GetByNumericalID(container, typeof(Order), "orderID", row.OrderID);
                od.ProductID = (Product)Db4oUtil.GetByNumericalID(container, typeof(Product), "productID", row.ProductID);
                od.UnitPrice = Convert.ToDouble(row.UnitPrice);
                od.Quantity = row.Quantity;
                od.Discount = Convert.ToDouble(row.Discount);

                container.Store(od);
                LogMessage("saved ("+od.OrderID.OrderID.ToString()+"/"+od.ProductID.ProductID.ToString()+")", true);
            }
            container.Commit();
            long objectCount = Db4oUtil.GetAllInstances(container, typeof(OrderDetail)).Count;
            if (table1.Count == objectCount)
                LogMessage(table1.Count + " objects saved", true);
            else
                LogMessage("Error: " + table1.Count + " rows retrieved but " + objectCount + " objects were saved", true);
            LogMessage("Done with OrderDetails" + Environment.NewLine, true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Terminate();
        }
        
    }
}
