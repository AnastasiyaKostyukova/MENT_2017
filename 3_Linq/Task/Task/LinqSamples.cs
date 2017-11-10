// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;

namespace SampleQueries
{
  [Title("LINQ Module")]
  [Prefix("Linq")]
  public class LinqSamples : SampleHarness
  {
    private DataSource dataSource = new DataSource();

    [Category("Restriction Operators")]
    [Title("Where - Task 1")]
    [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
    public void Linq_01()
    {
      ObjectDumper.Write("All orders sum:");
      var all_clients = dataSource.Customers.Select(c => c.Orders.Sum(o => o.Total)).OrderBy(s => s);

      int i = 0;
      foreach (var c in all_clients)
      {
        ObjectDumper.Write(i + ": " + c);
        i++;
      }

      var minOrdersSum = 30000;
      ObjectDumper.Write("");
      ObjectDumper.Write("Filtered orders sum, where min value = " + minOrdersSum);
      i = 0;
      var filtered_clients = all_clients.Where(c => c > minOrdersSum);

      foreach (var c in filtered_clients)
      {
        ObjectDumper.Write(i + ": " + c);
        i++;
      }
    }

    [Category("Restriction Operators")]
    [Title("Where - Task 3")]
    [Description("Gets all customers who had orders with total > 5000")]
    public void Linq_03()
    {
      var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > 5000));
      int i = 0;
      foreach (var c in customers)
      {
        Console.WriteLine(i);
        ObjectDumper.Write(c);
        Console.Write("Orders: " + String.Join("|", c.Orders.Select(s => Decimal.ToInt32(s.Total))));
        Console.WriteLine();
        i++;
      }
    }

    [Category("Restriction Operators")]
    [Title("Where - Task 4")]
    [Description("Gets all suplpliers for customers int his city")]
    public void Linq_04()
    {
      var customers =
        dataSource.Customers.Where(c => c.Orders.Any())
          .Select(c => new { Customer = c, Date = c.Orders.Select(o => o.OrderDate).Min() });
      var i = 0;
      foreach (var c in customers)
      {
        Console.WriteLine($"{i} Customer ID = {c.Customer.CustomerID}; First order = {c.Date.Month}/{c.Date.Year};");
        i++;
      }
    }

    [Category("Restriction Operators")]
    [Title("Where - Task 6")]
    [Description("Gets all customers without region or mobile operator and non-digit postal code")]
    public void Linq_06()
    {
      var number = 0;
      var i = 0;
      var customers =
        dataSource.Customers.Where(
          c => String.IsNullOrEmpty(c.Region) || c.Phone.First() != '(' || !int.TryParse(c.PostalCode, out number));


      foreach (var c in customers)
      {
        Console.Write($"{i}: ");
        ObjectDumper.Write(c);
        Console.WriteLine($"Region: {c.Region}; Phone: {c.Phone}; PostalCode: {c.PostalCode}");
        i++;
      }
    }

    [Category("Join Operators")]
    [Title("Join - Task 2")]
    [Description("Gets all suplpliers for customers int his city")]
    public void Linq_02()
    {
      var all_Customers = dataSource.Customers;
      var all_Suppliers = dataSource.Suppliers;
      int i = 0;
      var pairs_1 = all_Customers.GroupJoin(
        all_Suppliers,
        c => c.Country + c.City,
        s => s.Country + s.City,
        (c, s) => new { Customer = c, Suppliers = s });

      foreach (var p in pairs_1)
      {
        Console.Write($"{i}: Customer: {p.Customer.CustomerID}; suppliers: ");
        if (p.Suppliers.Any()) Console.Write(String.Join(" | ", p.Suppliers.Select(s => s.SupplierName)));
        Console.WriteLine();
        i++;
      }

      i = 0;
      var pairs_2 =
        all_Customers.Select(
          c => new { Customer = c, Suppliers = all_Suppliers.Where(s => s.Country == c.Country && s.City == c.City) });
      Console.WriteLine();
      foreach (var p in pairs_2)
      {
        Console.Write($"{i}: Customer: {p.Customer.CustomerID}; suppliers: ");
        if (p.Suppliers.Any()) Console.Write(String.Join(" | ", p.Suppliers.Select(s => s.SupplierName)));
        Console.WriteLine();
        i++;
      }
    }


    [Category("Ordering Operators")]
    [Title("Order - Task 5")]
    [Description(
      "Gets all customers with date of their first order order by year, month, sum of orders, customer's name")]
    public void Linq_05()
    {
      var customers =
        dataSource.Customers.Where(c => c.Orders.Any())
          .Select(c => new { Customer = c, Date = c.Orders.Select(o => o.OrderDate).Min() })
          .OrderBy(c => c.Date.Year)
          .ThenBy(c => c.Date.Month)
          .ThenByDescending(c => c.Customer.Orders.Sum(o => o.Total))
          .ThenBy(c => c.Customer.CustomerID);

      foreach (var c in customers)
      {
        Console.WriteLine(
          $"CustomerID = {c.Customer.CustomerID} First order = {c.Date.Year} yesr, {c.Date.Month} momth; Sum = {c.Customer.Orders.Sum(o => o.Total)}");
      }
    }

    [Category("Grouping Operators")]
    [Title("GroupBy - Task 7")]
    [Description("Groups products")]
    public void Linq_07()
    {
      var products = dataSource.Products.GroupBy(x => x.Category, (key, g1) => g1.GroupBy(x => x.UnitsInStock));

      foreach (var p in products.SelectMany(o => o))
      {
        foreach (var i in p.OrderBy(l => l.UnitPrice))
        {
          ObjectDumper.Write(i);
          Console.WriteLine($"Category: {i.Category.PadRight(15)}; UnitsInStock: {i.UnitsInStock.ToString().PadRight(6)}; UnitPrice: {i.UnitPrice}");
        }
      }
    }

    [Category("Grouping Operators")]
    [Title("GroupBy - Task 8")]
    [Description("Groups product by their price in 3 groups")]
    public void Linq_08()
    {
      var products = dataSource.Products.GroupBy(p => p.UnitPrice > 50 ? "High price > 50:" : p.UnitPrice > 15 ? "Medium price:" : "Low price <= 15:");

      foreach (var p in products)
      {
        Console.WriteLine(p.Key);
        ObjectDumper.Write(p);
        Console.WriteLine($"Unit Prices: {string.Join("|", p.Select(f => Decimal.ToInt32(f.UnitPrice)))}");
      }
    }

    [Category("Grouping Operators")]
    [Title("GroupBy - Task 9")]
    [Description("Gets average profitability of city and its intensity")]
    public void Linq_09()
    {
      var averageProfitability = dataSource.Customers.GroupBy(c => c.City, (key, c) => new { City = key, Average = c.SelectMany(o => o.Orders).Average(p => p.Total) });
      var averageIntensity = dataSource.Customers.GroupBy(c => c.City, (key, c) => new { City = key, Average = c.Average(cus => cus.Orders.Count()) });

      Console.WriteLine("Profitability:");
      ObjectDumper.Write(averageProfitability);

      Console.WriteLine();
      Console.WriteLine("Intensity:");
      ObjectDumper.Write(averageIntensity);
    }

    [Category("Grouping Operators")]
    [Title("GroupBy - Task 10")]
    [Description("Gets statistics")]
    public void Linq_10()
    {
      var byMonth = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => o.OrderDate.Month);
      var byYear = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => o.OrderDate.Year);
      var byYearAndMonth = dataSource.Customers.SelectMany(c => c.Orders).GroupBy(o => new { month = o.OrderDate.Month, year = o.OrderDate.Year });

      Console.WriteLine("Group by month:");
      foreach (var item in byMonth)
      {
        Console.WriteLine($"In the {item.Key}th month there were {item.Count()} orders. These orders:");
        //ObjectDumper.Write(item);
      }

      Console.WriteLine();
      Console.WriteLine("Group by year:");
      foreach (var item in byYear)
      {
        Console.WriteLine($"In the {item.Key}th year there were {item.Count()} orders. These orders:");
        //ObjectDumper.Write(item);
      }

      Console.WriteLine();
      Console.WriteLine("Group by year and month:");
      foreach (var item in byYearAndMonth)
      {
        Console.WriteLine($"In the {item.Key} there were {item.Count()} orders. These orders:");
        //ObjectDumper.Write(item);
      }
    }


    //[Category("Restriction Operators")]
    //[Title("Where - Task 1")]
    //[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
    //public void Linq1()
    //{
    //  int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

    //  var lowNums = from num in numbers where num < 5 select num;

    //  Console.WriteLine("Numbers < 5:");
    //  foreach (var x in lowNums)
    //  {
    //    Console.WriteLine(x);
    //  }
    //}

    //[Category("Restriction Operators")]
    //[Title("Where - Task 2")]
    //[Description("This sample return return all presented in market products")]

    //public void Linq2()
    //{
    //  var products = from p in dataSource.Products where p.UnitsInStock > 0 select p;

    //  foreach (var p in products)
    //  {
    //    ObjectDumper.Write(p);
    //  }
    //}

  }
}

