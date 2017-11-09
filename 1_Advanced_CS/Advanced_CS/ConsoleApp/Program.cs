using FileVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      Func<string, bool> func0 = r => r.Contains("2");
      Func<string, bool> func1 = r => r.EndsWith(".cat");
      Func<string, bool> func2 = r => r.Length > 8;
      Func<string, bool> func3 = r => r.Contains("folder");
      Func<string, bool> func4 = r => { r = r.Substring(0, 3); return r == "fil"; };
      
      var fsv = new FileSystemVisitor(@"TEST", s => s.Contains("2"));

      //fsv.VisitStarted += (object sender, VisitorEventArgs e) =>
      //{
      //  Console.WriteLine("\nEvent Filtered search started!\n");

      //  Console.WriteLine("AAAAAAAAAAAAAAAA");
      //  fsv.StopHere = true;
      //};

      //fsv.FileFound += (object sender, VisitorEventArgs e) =>
      //{
      //  if (e.Info == (@"TEST\test1\test3\intest3_1.txt"))
      //  {
      //    Console.WriteLine("AAAAAAAAAAAAAAAA");
      //    fsv.StopHere = true;
      //  }
      //};

      fsv.VisitStarted += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent Filtered search started!\n");
      fsv.VisitEnded += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent Filtered search finished\n");

      fsv.FileFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent FileFound " + e.Info);
      //fsv.FileFound += OnFind;
      fsv.DirectoryFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent DirectoryFound " + e.Info);
      fsv.FilteredDirectoryFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent FilteredDirectoryFound " + e.Info);
      fsv.FilteredFileFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nEvent FilteredDirectoryFound " + e.Info);

      foreach (var r in fsv)
      {
        Console.WriteLine(r);
      }
    }

    static void OnFind(object sender, VisitorEventArgs args)
    {
      Console.WriteLine("ON FIND: " + args.Info);
    }
  }
}
