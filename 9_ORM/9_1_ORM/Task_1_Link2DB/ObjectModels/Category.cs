using LinqToDB.Mapping;

namespace Task_1_Link2DB.ObjectModels
{
  [Table(Name = "Categories")]
  public class Category
  {
    [PrimaryKey, Identity]
    public int CategoryID { get; set; }

    [Column(Name = "CategoryName")]
    public string Name { get; set; }
  }
}
