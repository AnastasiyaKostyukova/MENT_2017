
using System;

namespace FileVisitor
{
  public class VisitorEventArgs : EventArgs
  {
    public VisitorEventArgs(string info = null)
    {
      this.Info = info;
    }
    public string Info { get; set; }

    //public void Stop()
    //{
    //  FileSystemVisitor.StopHere = true;
    //}

  }
}
