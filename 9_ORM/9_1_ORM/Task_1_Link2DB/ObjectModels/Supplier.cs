﻿using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Suppliers")]
  public class Supplier
  {
    [PrimaryKey, Identity]
    public int SupplierID { get; set; }

    [Column(Name = "CompanyName")]
    public string Name { get; set; }
  }
}
