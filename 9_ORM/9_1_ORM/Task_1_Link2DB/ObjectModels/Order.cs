using System;
using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Orders")]
  public class Order
  {
    [PrimaryKey, Identity]
    public int OrderId { get; set; }

    [Column("ShipVia")]
    public int ShipperID { get; set; }
    [Association(ThisKey = "ShipVia", OtherKey = "ShipperID")]
    public Shipper Shipper { get; set; }

    [Column("EmployeeID")]
    public int EmployeeID { get; set; }
    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public Employee Employee { get; set; }

    [Column(Name = "ShippedDate")]
    public DateTime? ShippedDate { get; set; }
  }
}
