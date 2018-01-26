using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table("Shippers")]
  public class Shipper
  {
    [PrimaryKey, Identity]
    public int ShipperID { get; set; }

    [Column]
    public string CompanyName { get; set; }

    [Association(ThisKey = "ShipperID", OtherKey = "ShipVia", CanBeNull = false)]
    public IEnumerable<Order> Orders { get; set; }
  }
}
