using FileWatcherService.ConfigElements;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FileWatcherService
{
  class Program
  {
    private static int count;
    private static List<RuleElement> rules = new List<RuleElement>();
    static void Main()
    {
      var section = (ConfigSection) ConfigurationManager.GetSection("WatcherSettings");

      if (section != null)
      {
        foreach (RuleElement re in section.RulesItems)
        {
          rules.Add(re as RuleElement);
          Console.WriteLine($"{re.RuleTemplate} {re.DestinationFolder}");
        }
          foreach (FolderElement folder in section.FolderItems)
        {
          FileSystemWatcher watcher = new FileSystemWatcher();
          try
          {
            watcher.Path = folder.Path;
            watcher.Created += new FileSystemEventHandler(OnCreated); // Add event handler
            watcher.EnableRaisingEvents = true;                       // Begin watching
          }
          catch (ArgumentException e)
          {
            Console.WriteLine($"Watching of the {folder.Path} directory is stopped. Reason: {e.Message}");
          }
        }
      }

      Console.WriteLine("Press \'Q\' to exit:\n");
      while (Console.Read() != 'q');
    }

    private static void OnCreated(object source, FileSystemEventArgs e)
    {
      if (!e.Name.Contains('.')) { return; } // To igrore folders

      Console.WriteLine($"File Found: {e.FullPath}");
      count++;
      bool ruleFound = false;
      foreach (var rule in rules)
      {
        if (new Regex(rule.RuleTemplate).IsMatch(e.Name))//fileNameWithoutExtension.ToString()))
        {
          ruleFound = true;
          Console.WriteLine($"Rule Found: {rule.RuleTemplate}");
          var date = DateTime.Now;
          var newPath = new StringBuilder(Path.Combine(Path.GetDirectoryName(e.FullPath), rule.DestinationFolder));
          var newFileName = new StringBuilder(Path.GetFileNameWithoutExtension(e.Name));
          newFileName = rule.AddDate ? newFileName.Append($"_{DateTime.Now.ToString("m", CultureInfo.GetCultureInfo(9))}") : newFileName;
          newFileName = rule.AddIndex ? newFileName.Append($"_{count}") : newFileName;
          newFileName.Append(Path.GetExtension(e.Name));
          var newPathStr = Path.Combine(newPath.ToString(), newFileName.ToString());
          
          new FileInfo(newPathStr).Directory.Create();
          File.Move(e.FullPath, newPathStr);
          Console.WriteLine($"File Replaced: {newPathStr}");
        }
      }

      if (!ruleFound)
      {
        Console.WriteLine("Rule Not Found");
      }

      //Console.WriteLine($"{e.ChangeType}: File: {e.FullPath} {e.ChangeType}");
    }

    //private static void OnRenamed(object source, RenamedEventArgs e)
    //{
    //  Console.WriteLine($"{e.ChangeType}: File: {e.OldFullPath} renamed to {e.FullPath}");
    //}
  }
}