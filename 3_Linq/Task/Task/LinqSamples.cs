// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

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

    [Category("Join Operators")]
    [Title("Join - Task 2")]
    [Description("Gets all suplpliers for customers int his city")]
    public void Linq_02()
    {
      var all_Customers = dataSource.Customers;
      var all_Suppliers = dataSource.Suppliers;
      int i = 0;
      var pairs_1 = all_Customers.GroupJoin(all_Suppliers, c => c.Country + c.City, s => s.Country + s.City, (c, s) => new { Customer = c, Suppliers = s });

      foreach (var p in pairs_1)
      {
        Console.Write($"{i}: Customer: {p.Customer.CustomerID}; suppliers: ");
        if (p.Suppliers.Any()) Console.Write(String.Join(" | ", p.Suppliers.Select(s => s.SupplierName)));
        Console.WriteLine();
        i++;
      }

      i = 0;
      var pairs_2 =
        all_Customers.Select(c => new { Customer = c, Suppliers = all_Suppliers.Where(s => s.Country == c.Country && s.City == c.City) });
      Console.WriteLine();
      foreach (var p in pairs_2)
      {
        Console.Write($"{i}: Customer: {p.Customer.CustomerID}; suppliers: ");
        if (p.Suppliers.Any()) Console.Write(String.Join(" | ", p.Suppliers.Select(s => s.SupplierName)));
        Console.WriteLine();
        i++;
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
