using System;
using System.Xml.Serialization;

namespace Serialization_Task
{
  [XmlType(TypeName = "book")]
  public class Book
  {
    [XmlAttribute("id")]
    public string Id { get; set; }

    [XmlElement("isbn")]
    public string ProductKey { get; set; }

    [XmlElement("author")]
    public string Author { get; set; }

    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("genre")]
    public Genre Genre { get; set; }

    [XmlElement("publisher")]
    public string Publisher { get; set; }

    [XmlElement("publish_date")]
    public DateTime PublishingDate { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }

    [XmlElement("registration_date")]
    public DateTime RegistrationDate { get; set; }
  }
}