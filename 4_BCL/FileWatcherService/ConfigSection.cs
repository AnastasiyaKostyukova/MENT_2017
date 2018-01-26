using FileWatcherService.ConfigCollections;
using System.Configuration;
using System.Globalization;

namespace FileWatcherService
{
  public class ConfigSection : ConfigurationSection
  {
    [ConfigurationProperty("culture")]
    public CultureInfo Culture
    {
      get
      {
        return (CultureInfo)(this["culture"]);
      }
      set { this["culture"] = value;
      }
      }

      [ConfigurationProperty("Folders")]
    [ConfigurationCollection(typeof(FolderCollection), AddItemName = "Folder")]
    public FolderCollection FolderItems
    {
      get
      {
        return (FolderCollection) base["Folders"];
      }
    }

    [ConfigurationProperty("Rules")]
    [ConfigurationCollection(typeof(FolderCollection), AddItemName = "Rule")]
    public RuleCollections RulesItems
    {
      get
      {
        return (RuleCollections)(this["Rules"]);
      }
      set { this["Rules"] = value; }
    }
  }
}