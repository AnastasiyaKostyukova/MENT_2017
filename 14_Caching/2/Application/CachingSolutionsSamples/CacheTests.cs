using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
  [TestClass]
  public class CacheTests
  {
    [TestMethod]
    public void MemoryCache()
    {
      var categoryManager = new CategoriesManager(new CategoriesMemoryCache());
      var tryCount = 5;
      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetCategories().Count()} categories.");
        Thread.Sleep(100);
      }
      Console.WriteLine("__________________________________");
      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetRegions().Count()} regions.");
        Thread.Sleep(100);
      }
      Console.WriteLine("__________________________________");
      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetSuppliers().Count()} suppliers.");
        Thread.Sleep(100);
      }
    }

    [TestMethod]
    public void RedisCache()
    {
      var categoryManager = new CategoriesManager(new CategoriesRedisCache("localhost"));
      var tryCount = 5;

      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetCategories().Count()} categories.");
        Thread.Sleep(100);
      }
      Console.WriteLine("__________________________________");
      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetRegions().Count()} regions.");
        Thread.Sleep(100);
      }
      Console.WriteLine("__________________________________");
      for (var i = 1; i <= tryCount; i++)
      {
        Console.WriteLine($"{i} try : {categoryManager.GetSuppliers().Count()} suppliers.");
        Thread.Sleep(100);
      }
    }
  }
}
