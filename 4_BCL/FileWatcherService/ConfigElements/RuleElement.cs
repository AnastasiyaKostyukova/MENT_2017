using System.Configuration;

namespace FileWatcherService.ConfigElements
{
   class RuleElement : ConfigurationElement
  {
    [ConfigurationProperty("ruleTemplate", IsKey = true)]
    public string RuleTemplate
    {
      get
      {
        return (string)this["ruleTemplate"];
      }
      set { this["ruleTemplate"] = value; }
    }

    [ConfigurationProperty("destinationFolder")]
    public string DestinationFolder
    {
      get
      {
        return (string)this["destinationFolder"];
      }
      set { this["destinationFolder"] = value; }
    }

    [ConfigurationProperty("addIndex")]
    public bool AddIndex
    {
      get
      {
        return (bool)this["addIndex"];
      }
      set { this["addIndex"] = value; }
    }

    [ConfigurationProperty("addDate")]
    public bool AddDate
    {
      get
      {
        return (bool)this["addDate"];
      }
      set { this["addDate"] = value; }
    }
  }
}
