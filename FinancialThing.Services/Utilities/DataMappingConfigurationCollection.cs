using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FinancialThing.Services.Utilities
{
    public class DataMappingConfigurationCollection: ConfigurationElementCollection, IEnumerable<DataMappingConfigurationEntry>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DataMappingConfigurationEntry();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataMappingConfigurationEntry) element);
        }

        public IEnumerator<DataMappingConfigurationEntry> GetEnumerator()
        {
            return this.OfType<DataMappingConfigurationEntry>().GetEnumerator();
        }
        public DataMappingConfigurationEntry this[int id]
        {
            get { return (DataMappingConfigurationEntry)BaseGet(id); }
        }
    }
}