using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using FileWatcherService.ConfigElements;
using messages = FileWatcherService.Resources.Messages;

namespace FileWatcherService
{
  internal class Program
  {
    private static int count;
    private static List<RuleElement> rules = new List<RuleElement>();
    private static List<string> folders = new List<string>();

    private static bool stopApp = false;

    static void Main()
    {
      ConfigurateApp();
      foreach (var folder in folders)
      {
        FileSystemWatcher watcher = new FileSystemWatcher();
        try
        {
          watcher.Path = folder;
          watcher.Created += OnFileCreated;       // Add event handler
          watcher.EnableRaisingEvents = true; // Begin watching
        }
        catch (ArgumentException e)
        {
          Console.WriteLine($"{messages.ExceptionMessage} {e.Message}");
        }
      }

      Console.WriteLine($"\n{messages.FinishMessage}\n");
      Console.CancelKeyPress += new ConsoleCancelEventHandler(OnCntrlCPressingHandler);

      while (!stopApp)
      {
        ConsoleKeyInfo cki = Console.ReadKey(true);       // Start a console read operation. Do not display the input.
        Console.WriteLine("  Key pressed: {0}", cki.Key); // Announce the name of the key that was pressed
      }
    }

    private static void OnFileCreated(object source, FileSystemEventArgs e)
    {
      if (!e.Name.Contains('.'))
      {
        return;
      } // To igrore folders

      Console.WriteLine($"{messages.FileFound} {e.FullPath}");
      count++;
      var ruleFound = false;
      StringBuilder newFileName = null;
      StringBuilder newPath = null;
      string newPathString = null;

      foreach (var rule in rules)
      {
        if (new Regex(rule.RuleTemplate).IsMatch(e.Name))
        {
          ruleFound = true;
          Console.WriteLine($"{messages.RuleFound} {rule.RuleTemplate}");
          newPath = new StringBuilder(Path.Combine(Path.GetDirectoryName(e.FullPath), rule.DestinationFolder));
          newFileName = new StringBuilder(Path.GetFileNameWithoutExtension(e.Name));
          newFileName = rule.AddIndex ? newFileName.Insert(0, $"_{count}") : newFileName;
          newFileName = rule.AddDate
                          ? newFileName.Append($"_{DateTime.Now.ToString("m", messages.Culture.DateTimeFormat)}")
                          : newFileName;
          newFileName.Append(Path.GetExtension(e.Name));
          newPathString = Path.Combine(newPath.ToString(), newFileName.ToString());

          Moving(e.FullPath, newPathString);
          Console.WriteLine($"{messages.FileReplaced} {newPathString} \n");
        }
      }

      if (!ruleFound)
      {
        Console.WriteLine(messages.RuleNotFound);
        newPathString = Path.Combine(Path.GetDirectoryName(e.FullPath), "garbage", e.Name);

        Moving(e.FullPath, newPathString);
        Console.WriteLine($"{messages.FileReplaced} {newPathString} \n");
      }
    }

    protected static void OnCntrlCPressingHandler(object sender, ConsoleCancelEventArgs args)
    {
      Console.WriteLine("\nThe watch operation has been interrupted..");
      stopApp = true;
    }

    private static void Moving(string oldPath, string newPath)
    {
      var tryCount = 0;
      new FileInfo(newPath).Directory.Create();
      while (true)
      {
        try
        {
          if (File.Exists(newPath))
          {
            Console.WriteLine($"{messages.SameFileDeleting}");
            File.Delete(newPath);
          }
          Console.WriteLine($"TRY {tryCount}");
          File.Move(oldPath, newPath);
          break;
        }
        catch (Exception e)
        {
          Thread.Sleep(500);
          if (++tryCount < 10) continue;
          throw;
        }
      }
    }

    private static void ConfigurateApp()
    {
      Console.OutputEncoding = Encoding.Unicode;
      var section = (ConfigSection)ConfigurationManager.GetSection("WatcherSettings");

      messages.Culture = section.Culture;
      Thread.CurrentThread.CurrentCulture = section.Culture;
      Thread.CurrentThread.CurrentUICulture = section.Culture;

      if (section != null)
      {
        foreach (RuleElement rule in section.RulesItems)
        {
          rules.Add(rule as RuleElement);
          Console.WriteLine($"{messages.CustomSectionElement} {rule.RuleTemplate} {rule.DestinationFolder}");
        }

        foreach (FolderElement folder in section.FolderItems)
        {
          folders.Add(folder?.Path);
          Console.WriteLine($"{messages.CustomSectionElement} {folder.Path}");
        }
      }
    }
  }
}