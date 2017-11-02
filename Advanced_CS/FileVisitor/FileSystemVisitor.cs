using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileVisitor
{
  public class FileSystemVisitor
  {
    private Func<string, bool> _filter;
    private string _rootFolder;
    public bool StopHere { get; set; }

    public event EventHandler<VisitorEventArgs> VisitStarted;
    public event EventHandler<VisitorEventArgs> VisitEnded;
    public event EventHandler<VisitorEventArgs> DirectoryFound;
    public event EventHandler<VisitorEventArgs> FilteredDirectoryFound;
    public event EventHandler<VisitorEventArgs> FileFound;
    public event EventHandler<VisitorEventArgs> FilteredFileFound;

    public event EventHandler<VisitorEventArgs> FilesFound;


    public FileSystemVisitor() { }

    public FileSystemVisitor(string rootFolder)
    {
      this._rootFolder = rootFolder;
    }

    public FileSystemVisitor(Func<string, bool> filter)
    {
      this._filter = filter;
    }
    
    public FileSystemVisitor(string rootFolder, Func<string, bool> filter)
    {
      this._rootFolder = rootFolder;
      this._filter = filter;
    }

    public IEnumerator<string> GetEnumerator()
    {
      if (String.IsNullOrEmpty(_rootFolder))
      {
        if (_rootFolder == null)
          throw new ArgumentNullException();
        else
          throw new ArgumentException("Start point must be not empty");
      }

      VisitStarted?.Invoke(this, new VisitorEventArgs("Our travel is started"));

      var directories = Directory.GetDirectories(_rootFolder);

      foreach (var dirName in directories)
      {
        if (StopHere)
        {
          VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
          yield break;
        }

        DirectoryFound?.Invoke(this, new VisitorEventArgs(dirName));

        if (StopHere)
        {
          VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
          yield break;
        }

        if (ReturnThisItem(dirName))
        {
          if (StopHere)
          {
            VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
            yield break;
          }
          yield return dirName;
        }

        FileSystemVisitor subVisitor;
        if (_filter == null)
          subVisitor = new FileSystemVisitor(dirName);
        else
          subVisitor = new FileSystemVisitor(dirName, _filter);

        this.SubscribeToSubFsvEvents(subVisitor);

        //Searching in subdirectories
        foreach (var item in subVisitor)
        {
          if (StopHere)
          {
            VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
            yield break;
          }
          yield return item;
        }
      }

      var files = Directory.GetFiles(_rootFolder);

      //Return files
      foreach (var fileName in files)
      {
        if (StopHere)
        {
          VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
          yield break;
        }

        FileFound?.Invoke(this, new VisitorEventArgs(fileName));

        if (StopHere)
        {
          VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
          yield break;
        }

        if (ReturnThisItem(fileName))
        {
          if (StopHere)
          {
            VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
            yield break;
          }
          yield return fileName;
        }
      }

      VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is ended"));
    }

    private bool ReturnThisItem(string item)
    {
      if (this._filter != null && this._filter(item))
      {
        FilteredDirectoryFound?.Invoke(this, new VisitorEventArgs(item));
      }
      return (this._filter == null || this._filter(item));
    }

    private void SubscribeToSubFsvEvents(FileSystemVisitor subFsv)
    {
      subFsv.DirectoryFound += (object s, VisitorEventArgs e) => this.DirectoryFound?.Invoke(this, new VisitorEventArgs(e.Info));
      subFsv.FileFound += (object s, VisitorEventArgs e) => this.FileFound?.Invoke(this, new VisitorEventArgs(e.Info));
      subFsv.FilteredDirectoryFound += (object s, VisitorEventArgs e) => this.FilteredDirectoryFound?.Invoke(this, new VisitorEventArgs(e.Info));
      subFsv.FilteredFileFound += (object s, VisitorEventArgs e) => this.FilteredFileFound?.Invoke(this, new VisitorEventArgs(e.Info));
    }
  }
}
