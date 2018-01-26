using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Employees")]
  public class Employee
  {
    [PrimaryKey, Identity]
    public int EmployeeID { get; set; }

    [Column(Name = "FirstName")]
    public string FirstName { get; set; }
    [Column(Name = "LastName")]
    public string LastName { get; set; }

    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public IEnumerable<EmployeeTerritories> EmployeeTerritories { get; set; }
    [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID")]
    public IEnumerable<Order> Orders { get; set; }
  }
}
