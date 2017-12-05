using System.Configuration;

namespace FileWatcherService.ConfigElements
{
  public class FolderElement : ConfigurationElement
  {

    [ConfigurationProperty("path", DefaultValue = "", IsKey = true, IsRequired = false)]
    public string Path
    {
      get { return (string)this["path"]; }
      set { base["path"] = value; }
    }
  }
}
