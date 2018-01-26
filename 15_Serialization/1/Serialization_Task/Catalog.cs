using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Serialization_Task
{
  [XmlType(TypeName = "catalog")]
  public class Catalog //: List<Book>
  {
    [XmlAttribute("date")]
    public DateTime CreationDate { get; set; }

    [XmlElement("book")]
    public List<Book> Books { get; set; }
  }
}