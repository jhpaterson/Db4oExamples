These are the projects available in this .NET solution: 

Db4oDataService: exposes a db4o database hosting the Northwind entities as a data service. 
It also provides an AJAX client (CategoryAdmin.aspx) to perfrom CRUD operations over 
Categories (and a second page that lists categories -> CategoryList.aspx). The AJAX client 
is based on a demo available in the blog post "Working with ADO.NET Data Services in AJAX",
so thanks goes to Jim Wang =). Note: don't forget to add the db4o v7.10 dlls as reference.

NorthwindDb4oGen: connects to a SQL Server instance and imports Northwind data into a db4o 
database. Use this project only once if you want to recreate the db4o db file. The project
requires a SQL Server instance running the Northwind database (you can just go ahead and 
use the db file 'northwind.db4o' in folder Db4oAstoriaDemo which has already been populated
with Northwind db entities). Note: don't forget to add the db4o v7.10 dll as reference.

ConsoleClient: a simple .NET console client to test CRUD operations over Categories.
NorthwindDataProxy.cs was created by running datasvcutil.exe on the db4o data aservice URI:
datasvcutil.exe /uri:http://localhost:32767/NorthwindData.svc /out:NorthwindDataProxy.cs

db4o v7.10 is available here:
http://developer.db4o.com/files/folders/db4o_710/default.aspx

Enjoy!

German Viscuso
german@db4o.com