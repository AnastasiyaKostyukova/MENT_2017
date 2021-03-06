﻿using System;
using System.Collections.Generic;
using NorthwindLibrary;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
  internal class CategoriesMemoryCache : INastyaCache
  {
    ObjectCache cache = MemoryCache.Default;

    string prefix = "Cache_Categories_";
    string regionPrefix = "Cache_Regions_";
    string supplierPrefix = "Cache_Suppliers_";

    public IEnumerable<Category> GetCategories(string forUser)
    {
      return (IEnumerable<Category>)cache.Get(prefix + forUser);
    }

    public void Set(string forUser, IEnumerable<Category> categories)
    {
      cache.Set(prefix + forUser, categories, DateTimeOffset.Now.AddMinutes(5));
    }
    ////////////////////////////////////////////////////////
    public IEnumerable<Region> GetRegions(string forUser)
    {
      return (IEnumerable<Region>)cache.Get(regionPrefix + forUser);
    }

    public void SetRegions(string forUser, IEnumerable<Region> regions)
    {
      cache.Set(regionPrefix + forUser, regions, DateTimeOffset.Now.AddMinutes(5));
    }

    public IEnumerable<Supplier> GetSuppliers(string forUser)
    {
      return (IEnumerable<Supplier>)cache.Get(supplierPrefix + forUser);
    }

    public void SetSuppliers(string forUser, IEnumerable<Supplier> supplier)
    {
      cache.Set(supplierPrefix + forUser, supplier, DateTimeOffset.Now.AddMinutes(5));
    }
  }
}
