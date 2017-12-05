using FileWatcherService.ConfigCollections;
using System.Configuration;

namespace FileWatcherService
{
  public class ConfigSection : ConfigurationSection
  {
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
      get => (RuleCollections)(this["Rules"]);
      set { this["Rules"] = value; }
    }
  }
}