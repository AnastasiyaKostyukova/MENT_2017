using LinqToDB.Mapping;
using System.Collections.Generic;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Region")]
  public class Region
  {
    [PrimaryKey, Identity]
    public int RegionID { get; set; }

    [Column(Name = "RegionDescription"), NotNull]
    public string Description { get; set; }


    [Association(ThisKey = "RegionID", OtherKey = "RegionID")]
    public IEnumerable<Territory> Territories { get; set; }
  }
}
