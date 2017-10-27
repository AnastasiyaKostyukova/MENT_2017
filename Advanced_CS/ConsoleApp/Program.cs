﻿using FileVisitor;
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

      //var fsv = new FileSystemVisitor(@"TEST");
      //fsv.FileFound += (object sender, VisitorEventArgs e) =>
      //{
      //  if (e.Info == (@"TEST\test1\test3\intest3_1.txt"))
      //  {
      //    Console.WriteLine("AAAAAAAAAAAAAAAAAAAAA");
      //    e.Stop();
      //  }
      //};
      

      var fsv = new FileSystemVisitor(@"TEST");//, s => s.Contains("2"));
      //var fsv = new FileSystemVisitor(@"TEST", s => s.Contains("3"));

      fsv.FileFound += (object sender, VisitorEventArgs e) =>
      {
        if (e.Info == (@"TEST\test1\test3\intest3_1.txt"))
        {
          Console.WriteLine("AAAAAAAAAAAAAAAA");
          e.Stop();
        }
      };

      fsv.VisitStarted += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFiltered search started!\n");
      fsv.VisitEnded += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFiltered search finished\n");

      //fsv.FileFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFileFound " + e.Info);
      //fsv.DirectoryFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nDirectoryFound " + e.Info);
      //fsv.FilteredDirectoryFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFilteredDirectoryFound " + e.Info);
      //fsv.FilteredFileFound += (object sender, VisitorEventArgs e) => Console.WriteLine("\nFilteredDirectoryFound " + e.Info);

      //var list = new List<string>();
      foreach (var r in fsv)
      {
        Console.WriteLine(r);
        //list.Add(r);
      }

      //foreach (var l in list)
      //{
      //  Console.WriteLine("HH " + l);
      //}

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
