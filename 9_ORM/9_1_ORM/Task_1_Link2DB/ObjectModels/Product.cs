﻿using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Products")]
  public class Product
  {
    [PrimaryKey, Identity]
    public int ProductID { get; set; }

    [Column(Name = "ProductName")]
    public string Name { get; set; }

    [Column(Name = "CategoryID")]
    public int? CategoryID { get; set; }
    [Association(ThisKey = "CategoryID", OtherKey = "CategoryID")]
    public Category Category { get; set; }

    [Column(Name = "SupplierID")]
    public int? SupplierID { get; set; }
    [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
    public Supplier Supplier { get; set; }
  }
}
