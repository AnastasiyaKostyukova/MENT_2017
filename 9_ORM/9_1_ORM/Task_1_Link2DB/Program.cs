using System;
using System.Linq;
using LinqToDB;
using Task_1_Link2DB.ObjectModels;

namespace Task_1_Link2DB
{
  using System.Collections.Generic;

  class Program
  {
    static void Main()
    {
      Task3_3();
    }

    static void Task2()
    {
      using (var db = new DbNorthwind())
      {
        //var products = db.Products.LoadWith(r => r.Category).LoadWith(r => r.Supplier).ToList();
        //var employees =
        //  db.Employees
        //    .Join(db.EmployeeTerritories, E => E.EmployeeID, ET => ET.EmployeeID, (E, ET) => new { E, ET })
        //    .Join(db.Territories, E_ET => E_ET.ET.TerritoryID, T => T.TerritoryID, (E_ET, T) => new { E_ET, T })
        //    .Join(db.Regions, E_ET_T => E_ET_T.T.RegionID, R => R.RegionID, (E_ET_T, R) => new { E_ET_T, R })
        //    .GroupBy(NameRes => NameRes.E_ET_T.E_ET.E.FirstName + " " + NameRes.E_ET_T.E_ET.E.LastName + "; ", RegRes => RegRes.R.Description, (RegRes, NameRes) => new { Id = RegRes, Region = NameRes.FirstOrDefault() });

        //var employees2 = db.Employees.Join(db.EmployeeTerritories, e => e.EmployeeID, et => et.EmployeeID, (e, et) => new { e, et })
        //    .Join(db.Territories, et => et.et.TerritoryID, t => t.TerritoryID, (et, t) => new { et, t })
        //    .Join(db.Regions, t => t.t.RegionID, r => r.RegionID, (t, r) => new { t, r })
        //    .GroupBy(r => r.r.Description, r => r.t.et.e.EmployeeID, (r, e) => new { Region = r, Employees = e.Count() });

        var employees3 = db.Employees.Join(db.Orders, e => e.EmployeeID, o => o.EmployeeID, (e, o) => new { e, o })
            .Join(db.Shippers, o => o.o.ShipperID, s => s.ShipperID, (o, s) => new { o, s })
            .GroupBy(s => s.o.e.FirstName, s => s.s.CompanyName, (e, s) => new { Employee = e, Shippers = s.Distinct() });

        int counter = 1;
        //foreach (var item in products)
        //{
        //  Console.WriteLine($"{counter}. Product: {item.Name} Category: {item.Category.Name} Supplier: {item.Supplier.Name}");
        //  counter++;
        //}

        //counter = 1;
        //foreach (var item in employees)
        //{
        //  Console.WriteLine($"{counter}. Employee: {item?.Id} Region: {item?.Region}");
        //}

        //counter = 1;
        //foreach (var item in employees2)
        //{
        //  Console.WriteLine($"{counter} Region: {item?.Region} Employees: {item?.Employees}");
        //  counter++;
        //}

        //foreach (var item in employees3)
        //{
        //  Console.WriteLine($"{counter} Employee: {item.Employee} Shippers: " + String.Join(" | ", item.Shippers));
        //  counter++;
        //}
      }
    }

    static void Task3_1()
    {
      var employee = new Employee() { FirstName = "Nastya", LastName = "Kastsiukova" };
      using (var db = new DbNorthwind())
      {
        employee.EmployeeID = Convert.ToInt32(db.InsertWithIdentity(employee));

        db.EmployeeTerritories.Insert(() => new EmployeeTerritories() { EmployeeID = employee.EmployeeID, TerritoryID = 94105 });
      }
    }

    static void Task3_2()
    {
      using (var db = new DbNorthwind())
      {
        db.Products.Where(p => p.ProductID == 1).Set(p => p.CategoryID, 5).Update();
      }
    }

    static void Task3_3()
    {
      using (var db = new DbNorthwind())
      {
        var products = new List<Product>
                {
                    new Product { Name = "First", SupplierID = 1, CategoryID = 100 },
                    new Product { Name = "Second", SupplierID = 100, CategoryID = 2 }
                };

        foreach (var product in products)
        {
          if (!db.Suppliers.Any(s => s.SupplierID == product.SupplierID))
          {
            var supplier = new Supplier { Name = "Task3_3 comp" };
            supplier.SupplierID = Convert.ToInt32(db.InsertWithIdentity(supplier));
            product.SupplierID = supplier.SupplierID;
          }

          if (!db.Categories.Any(c => c.CategoryID == product.CategoryID))
          {
            var category = new Category { Name = "Task3_3 categ" };
            category.CategoryID = Convert.ToInt32(db.InsertWithIdentity(category));
            product.CategoryID = category.CategoryID;
          }

          db.Insert(product);
        }
      }
    }

    static void Task3_4()
    {
      using (var db = new DbNorthwind())
      {
        db.OrderDetails.LoadWith(o => o.Order)
          .Where(od => db.Orders.Where(o => o.ShippedDate == null).Select(o => o.OrderId).Contains(od.OrderID) && od.ProductID == 1)
          .Set(od => od.ProductID, 4)
          .Update();
      }
      Console.WriteLine("");
    }
  }
}
