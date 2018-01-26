using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using NorthwindLibrary;
using StackExchange.Redis;

namespace CachingSolutionsSamples
{
  class CategoriesRedisCache : INastyaCache
  {
    private readonly ConnectionMultiplexer redisConnection;

    string categories_prefix = "R_Cache_Categories";
    string regions_prefix = "R_Cache_Regions";
    string suppliers_prefix = "R_Cache_Suppliers";

    readonly DataContractSerializer categories_serializer = new DataContractSerializer(typeof(IEnumerable<Category>));
    readonly DataContractSerializer regions_serializer = new DataContractSerializer(typeof(IEnumerable<Region>));
    readonly DataContractSerializer suppliers_serializer = new DataContractSerializer(typeof(IEnumerable<Supplier>));

    public CategoriesRedisCache(string hostName)
    {
      var option = new ConfigurationOptions { AbortOnConnectFail = false, EndPoints = { hostName } };
      this.redisConnection = ConnectionMultiplexer.Connect(option);
    }

    public IEnumerable<Category> GetCategories(string forUser)
    {
      var db = this.redisConnection.GetDatabase();
      byte[] s = db.StringGet(this.categories_prefix + forUser);
      if (s == null) return null;

      return (IEnumerable<Category>)this.categories_serializer.ReadObject(new MemoryStream(s));
    }

    public void Set(string forUser, IEnumerable<Category> categories)
    {
      var db = this.redisConnection.GetDatabase();
      var key = this.categories_prefix + forUser;

      if (categories == null)
      {
        db.StringSet(key, RedisValue.Null);
      }
      else
      {
        var stream = new MemoryStream();
        this.categories_serializer.WriteObject(stream, categories);
        db.StringSet(key, stream.ToArray());
      }
    }

    public IEnumerable<Region> GetRegions(string forUser)
    {
      var db = this.redisConnection.GetDatabase();
      byte[] s = db.StringGet(this.regions_prefix + forUser);
      if (s == null) return null;

      return (IEnumerable<Region>)this.regions_serializer.ReadObject(new MemoryStream(s));
    }

    public void SetRegions(string forUser, IEnumerable<Region> regions)
    {
      var db = this.redisConnection.GetDatabase();
      var key = this.regions_prefix + forUser;

      if (regions == null)
      {
        db.StringSet(key, RedisValue.Null);
      }
      else
      {
        var stream = new MemoryStream();
        this.regions_serializer.WriteObject(stream, regions);
        db.StringSet(key, stream.ToArray());
      }
    }

    public IEnumerable<Supplier> GetSuppliers(string forUser)
    {
      var db = this.redisConnection.GetDatabase();
      byte[] s = db.StringGet(this.suppliers_prefix + forUser);
      if (s == null) return null;

      return (IEnumerable<Supplier>)this.suppliers_serializer.ReadObject(new MemoryStream(s));
    }

    public void SetSuppliers(string forUser, IEnumerable<Supplier> suppliers)
    {
      var db = this.redisConnection.GetDatabase();
      var key = this.suppliers_prefix + forUser;

      if (suppliers == null)
      {
        db.StringSet(key, RedisValue.Null);
      }
      else
      {
        var stream = new MemoryStream();
        this.suppliers_serializer.WriteObject(stream, suppliers);
        db.StringSet(key, stream.ToArray());
      }
    }
  }
}
