using FileWatcherService.ConfigElements;
using System.Configuration;

namespace FileWatcherService.ConfigCollections
{
  public class FolderCollection : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new FolderElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((FolderElement)element).Path;
    }

    public FolderElement this[int idx]
    {
      get { return (FolderElement)BaseGet(idx); }
    }
  }
}