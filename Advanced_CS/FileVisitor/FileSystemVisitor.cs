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

    public event EventHandler<VisitorEventArgs> VisitStarted;
    public event EventHandler<VisitorEventArgs> VisitEnded;
    public event EventHandler<VisitorEventArgs> DirectoryFound;
    public event EventHandler<VisitorEventArgs> FilteredDirectoryFound;
    public event EventHandler<VisitorEventArgs> FileFound;
    public event EventHandler<VisitorEventArgs> FilteredFileFound;

    public event EventHandler<VisitorEventArgs> FilesFound;


    public FileSystemVisitor() { }

    public FileSystemVisitor(Func<string, bool> filter)
    {
      this._filter = filter;
    }

    public FileSystemVisitor(string rootFolder)
    {
      this._rootFolder = rootFolder;
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
        DirectoryFound?.Invoke(this, new VisitorEventArgs(dirName));

        if (ReturnThisItem(dirName))
        {
          yield return dirName;


          FileSystemVisitor subVisitor;
          if (_filter == null)
            subVisitor = new FileSystemVisitor(dirName);
          else
            subVisitor = new FileSystemVisitor(dirName, _filter);

          //Searching in subdirectories
          foreach (var item in subVisitor)
          {
            yield return item;
          }
        }
      }

      var files = Directory.GetFiles(_rootFolder);

      //Return files
      foreach (var fileName in files)
      {
        FileFound?.Invoke(this, new VisitorEventArgs(fileName));

        if (ReturnThisItem(fileName))
        {
            yield return fileName;
        }
      }

      VisitEnded?.Invoke(this, new VisitorEventArgs("Our travel is started"));
    }

    private bool ReturnThisItem(string item)
    {
      if (this._filter != null && this._filter(item))
      {
        FilteredDirectoryFound?.Invoke(this, new VisitorEventArgs(item));
      }
      return (this._filter == null || !this._filter(item));
    }


    //public IEnumerable<string> TestOfFilter(List<string> files)
    //{
    //  if (this._filter == null)
    //  {
    //    return files;
    //  }

    //  var filtred = files.Where(this._filter);

    //  if (FilesFound != null)
    //  {
    //    FilesFound(this, new VisitorEventArgs { Info = "My file Found...." + string.Join(", ", filtred) });
    //  }
    //  return filtred;
    //}
  }
}
