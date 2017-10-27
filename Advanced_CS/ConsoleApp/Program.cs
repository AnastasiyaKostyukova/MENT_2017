using FileVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{

  class Button
  {
    public event EventHandler Click;

    public void Start ()
    {
      // magic

      if (Click != null)
      {
        Console.WriteLine("raise event");
        Click(null, new EventArgs());
      }
    }
  }



  class Program
  {


    //static void Main(string[] args)
    //{
    //  var btn = new Button();
    //  btn.Click += OnClick;
    //  //Console.WriteLine("subscribe");

    //  //Thread.Sleep(2000);
    //  //Console.WriteLine("Engine starting");
    //  ////btn.Start();
    //}

    //public static void OnClick(object sender, EventArgs args)
    //{
    //  Console.WriteLine("Open new window");
    //}





    static void Main(string[] args)
    {
      var files = new List<string>() { "file.txt", "file.exe.txt", "solutiom0.exe", "solutiom1.exe", "file.avi", "file" };

      //Func<string, int, double, bool> func1 = (r, i, d) => r.EndsWith(".exe");
      Func<string, bool> func1 = r => r.EndsWith(".exe");
      Func<string, bool> func2 = r => r.Length > 8;
      Func<string, bool> func3 = r => r.Contains("file");
      Func<string, bool> func4 = r => { r = r.Substring(0, 3); return r == "fil"; };

      var fsv = new FileSystemVisitor(@"TEST", s => s.EndsWith(".txt"));
      //var fsv = new FileSystemVisitor(@"TEST", s => s.Contains("3"));

      fsv.VisitStarted += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFiltered search started!\n");
      fsv.VisitEnded += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFiltered search finished\n");
      fsv.FilteredDirectoryFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nIS Filtered Directory \n" + e.Info);
      fsv.FilteredFileFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nIS Filtered File \n" + e.Info);

      var list = new List<string>();
      foreach (var r in fsv)
      {
        list.Add(r);
      }

      foreach (var l in list)
      {
        Console.WriteLine("HH " + l);
      }

      //Action<string, bool> func5 = (r, b) => { r.Substring(0, 3); };

      //var visitor = new FileSystemVisitor(func1);
      ////visitor.FileFound += OnFind;
      //visitor.FilesFound += (object sender, VisitorEventArgs e) => Console.WriteLine("File found: " + e.Info);
      ////visitor.VisitStarted += OnFind;
      //var result = visitor.TestOfFilter(files);

      //foreach (var r in result)
      //{
      //  Console.WriteLine(r);
      //}
    }

    static void OnFind(object sender, VisitorEventArgs args)
    {
      Console.WriteLine("ON FIND: " + args.Info);
    }
  }
}
