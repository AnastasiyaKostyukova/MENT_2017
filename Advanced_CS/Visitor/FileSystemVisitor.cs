using System;
using System.Collections.Generic;
using System.Linq;

namespace Visitor
{
  public class FileSystemVisitor
  {
    private Func<string, bool> _filter;

    public FileSystemVisitor ()
    {
    }

    public FileSystemVisitor (Func<string, bool> filter)
    {
      this._filter = filter;
    }

    public IEnumerable<string> TestOfFilter(List<string> files)
    {
      var filtred = files.Where(this._filter);
      return filtred;
    }
  }
}
