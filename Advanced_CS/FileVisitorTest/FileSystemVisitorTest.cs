using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FileVisitor;

namespace FileVisitorTest
{
  using System;

  public class FileSystemVisitorTest
    {

    [Test]
    public void FileSystemVisitor_WithEmptyRoot()
    {
      try
      {
        var fsv1 = new FileSystemVisitor("");
        var RealFileStructure = new List<string>();
        foreach (var item in fsv1)
        {
          RealFileStructure.Add(item);
        }
        Assert.Fail("An exception should have been thrown");
      }
      catch (ArgumentException ae)
      {
        Assert.AreEqual("Start point must be not empty", ae.Message);
      }
      catch (Exception e)
      {
        Assert.Fail(string.Format("Unexpected exception of type {0} caught: {1}", e.GetType(), e.Message));
      }
    }

    [Test]
      public void FileSystemVisitor_WithRoot()
      {
        var fsv2 = new FileSystemVisitor(@"TEST");

        var expected = new List<string>
                         {
                           @"TEST\test1",
                           @"TEST\test1\test3",
                           @"TEST\test1\test3\intest3_1.txt",
                           @"TEST\test1\intest1_1.txt",
                           @"TEST\test1\intest1_2.txt",
                           @"TEST\test2",
                           @"TEST\test2\intest2_1.txt"
                         };

        var RealFileStructure = new List<string>();
        foreach (var item in fsv2)
        {
          RealFileStructure.Add(item);
        }

        Assert.IsTrue(expected.SequenceEqual(RealFileStructure));
      }

    [Test]
    public void FileSystemVisitor_WithRootAndFilter()
    {
      var fsv3 = new FileSystemVisitor(@"TEST", s => s.Contains("2"));

      var expected = new List<string>
                         {
                           @"TEST\test1",
                           @"TEST\test1\test3",
                           @"TEST\test1\test3\intest3_1.txt",
                           @"TEST\test1\intest1_1.txt"
                         };

      var realFileStructure = new List<string>();
      foreach (var item in fsv3)
      {
        realFileStructure.Add(item);
      }

      Assert.IsTrue(expected.SequenceEqual(realFileStructure));
    }

    //[Test]
    //public void FileSystemVisitor_StopSearch()
    //{
    //  var fsv4 = new FileSystemVisitor(@"TEST");
    //  fsv4.FileFound += (object sender, VisitorEventArgs e) =>
    //  {
    //    if (e.Info == @"TEST\test1\test3\intest3_1.txt")
    //    {
    //      e.Stop();
    //    }
    //  };

    //  var expected = new List<string>
    //                     {
    //                       @"TEST\test1",
    //                       @"TEST\test1\test3"
    //                     };

    //  var actualArray = new List<string>();
    //  foreach (var item in fsv4)
    //  {
    //    actualArray.Add(item);
    //  }

    //  Assert.IsTrue(expected.SequenceEqual(actualArray));
    //}
  }
}
