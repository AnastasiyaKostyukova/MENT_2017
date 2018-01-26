using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization_Task;

namespace SerializationTest
{
  using System.Collections.Generic;

  [TestClass]
  public class SerializationTest
  {
    [XmlArray("catalog"), XmlArrayItem("book")]
    private Catalog forSerialize;

    [TestInitialize]
    public void Initialize()
    {
      this.forSerialize = new Catalog { CreationDate = DateTime.Now };
      this.forSerialize.Books = new List<Book>();
      this.forSerialize.Books.Add(
        new Book
        {
          Author = "Author1",
          Description = "Description1",
          Genre = Genre.Fantasy,
          Id = "id1",
          ProductKey = "productKey1",
          PublishingDate = DateTime.Now.AddDays(-4),
          Publisher = "PublisherName1",
          RegistrationDate = DateTime.Now.AddDays(-2),
          Title = "title1"
        });
      this.forSerialize.Books.Add(
        new Book
        {
          Author = "Author2",
          Description = "Description2",
          Genre = Genre.Computer,
          Id = "id2",
          ProductKey = "productKey2",
          PublishingDate = DateTime.Now.AddDays(-5),
          Publisher = "PublisherName2",
          RegistrationDate = DateTime.Now.AddDays(-3),
          Title = "title2"
        });
      this.forSerialize.Books.Add(
        new Book
        {
          Author = "Author3",
          Description = "Description3",
          Genre = Genre.Romance,
          Id = "id3",
          ProductKey = "productKey3",
          PublishingDate = DateTime.Now.AddDays(-6),
          Publisher = "PublisherName3",
          RegistrationDate = DateTime.Now.AddDays(-4),
          Title = "title3"
        });
    }

    [TestMethod]
    public void SerializeTest()
    {
      using (var stream = new FileStream(this.GenerateFileName(), FileMode.Create))
      {
        var serializer = new XmlSerializer(typeof(Catalog));
        serializer.Serialize(stream, this.forSerialize);
      }
    }

    private string GenerateFileName()
    {
      return "books" + DateTime.Now.ToString("MMddHHmmss") + ".xml";
    }

    [TestMethod]
    public void DeserializeTest()
    {
      using (var stream = new FileStream("books_1.xml", FileMode.Open))
      {
        var serializer = new XmlSerializer(typeof(Catalog));
        var result = serializer.Deserialize(stream);
        var catalog = result as Catalog;

        Assert.IsNotNull(catalog);
        Debug.WriteLine($"Catalog creation date: {catalog.CreationDate}");
        foreach (var book in catalog.Books)
        {
          Debug.WriteLine($"Book id: {book.Id}");
          Debug.WriteLine($"Book name: {book.Title}");
          Debug.WriteLine($"Book genre: {book.Genre}");
          Debug.WriteLine($"Book publishing date: {book.PublishingDate}");
          Debug.WriteLine(string.Empty);
        }
      }
    }
  }
}