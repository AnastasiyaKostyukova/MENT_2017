using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
  public class CategoriesManager
  {
    private readonly INastyaCache cache;

    public CategoriesManager(INastyaCache cache)
    {
      this.cache = null;
      this.cache = cache;
    }

    public IEnumerable<Category> GetCategories()
    {
      Console.Write("Get Categories ");

      var user = Thread.CurrentPrincipal.Identity.Name;
      var categories = cache.GetCategories(user);

      if (categories == null)
      {
        Console.WriteLine("from DB");

        using (var dbContext = new Northwind())
        {
          dbContext.Configuration.LazyLoadingEnabled = false;
          dbContext.Configuration.ProxyCreationEnabled = false;
          categories = dbContext.Categories.ToList();
          cache.Set(user, categories);
        }
      }
      else { Console.WriteLine("from Cache");  }

      return categories;
    }

    public IEnumerable<Region> GetRegions()
    {
      Console.Write("Get Regions ");

      var user = Thread.CurrentPrincipal.Identity.Name;
      var regions = cache.GetRegions(user);

      if (regions == null)
      {
        Console.WriteLine("from DB");

        using (var dbContext = new Northwind())
        {
          dbContext.Configuration.LazyLoadingEnabled = false;
          dbContext.Configuration.ProxyCreationEnabled = false;
          regions = dbContext.Regions.ToList();
          this.cache.SetRegions(user, regions);
        }
      }
      else { Console.WriteLine("from Cache"); }
      return regions;
    }

    public IEnumerable<Supplier> GetSuppliers()
    {
      Console.Write("Get Suppliers");

      var user = Thread.CurrentPrincipal.Identity.Name;
      var suppliers = this.cache.GetSuppliers(user);

      if (suppliers == null)
      {
        Console.WriteLine("from DB");

        using (var dbContext = new Northwind())
        {
          dbContext.Configuration.LazyLoadingEnabled = false;
          dbContext.Configuration.ProxyCreationEnabled = false;
          suppliers = dbContext.Suppliers.ToList();
          this.cache.SetSuppliers(user, suppliers);
        }
      }
      else { Console.WriteLine("from Cache"); }

      return suppliers;
    }
  }
}
