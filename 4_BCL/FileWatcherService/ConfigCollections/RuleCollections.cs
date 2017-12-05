using FileWatcherService.ConfigElements;
using System.Configuration;

namespace FileWatcherService.ConfigCollections
{
  public class RuleCollections : ConfigurationElementCollection
  {
    protected override ConfigurationElement CreateNewElement()
    {
      return new RuleElement();
    }

    protected override object GetElementKey(ConfigurationElement element)
    {
      return (RuleElement)element;
    }
  }
}
