using System.Linq;
using System.Web;

using Db4objects.Db4o;
using Db4objects.Db4o.Linq;
using Db4objects.Db4o.Data.Services;

using NorthwindDb4o;


namespace Db4oDataService
{
    //Db4oDataContext implements IUpdatable
    public class ManagementDataContext : Db4oDataContext
    {
        private static IEmbeddedObjectContainer _container;

        protected override IObjectContainer OpenSession()
        {
            return GetContainer().Ext().OpenSession();
        }

    	private static IEmbeddedObjectContainer GetContainer()
    	{
			if (null == _container || _container.Ext().IsClosed())
			{
				var file = MapPath("northwind.db4o");

				_container = Db4oEmbedded.OpenFile(Db4oEmbedded.NewConfiguration(), file);

				HttpContext.Current.ApplicationInstance.Disposed += delegate
				{
					_container.Close();
				};
			}
    		return _container;
    	}

    	private static string MapPath(string path)
    	{
    		return HttpContext.Current.Request.MapPath(path);
    	}

    	//Below we query the db4o container using its LINQ provider
        
        public IQueryable<Customer> Customers
        {
            get { return Container.AsQueryable<Customer>(); }
        }

        public IQueryable<Category> Categories
        {
            get { return Container.AsQueryable<Category>(); }
        }

        public IQueryable<Supplier> Suppliers
        {
            get { return Container.AsQueryable<Supplier>(); }
        }

        public IQueryable<Shipper> Shippers
        {
            get { return Container.AsQueryable<Shipper>(); }
        }

        public IQueryable<Region> Regions
        {
            get { return Container.AsQueryable<Region>(); }
        }

        public IQueryable<Employee> Employees
        {
            get { return Container.AsQueryable<Employee>(); }
        }

        public IQueryable<CustomerDemographics> CustomerDemographics
        {
            get { return Container.AsQueryable<CustomerDemographics>(); }
        }
        
        public IQueryable<CustomerCustomerDemo> CustomerCustomerDemo
        {
            get { return Container.AsQueryable<CustomerCustomerDemo>(); }
        }
        
        public IQueryable<EmployeeTerritory> EmployeeTerritories
        {
            get { return Container.AsQueryable<EmployeeTerritory>(); }
        }

        public IQueryable<Territory> Territories
        {
            get { return Container.AsQueryable<Territory>(); }
        }

        public IQueryable<Product> Products
        {
            get { return Container.AsQueryable<Product>(); }
        }

        public IQueryable<Order> Orders
        {
            get { return Container.AsQueryable<Order>(); }
        }
        
        public IQueryable<OrderDetail> OrderDetails
        {
            get { return Container.AsQueryable<OrderDetail>(); }
        }

    }
}
