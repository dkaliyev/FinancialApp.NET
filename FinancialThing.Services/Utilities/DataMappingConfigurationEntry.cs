using System.Configuration;

namespace FinancialThing.Services.Utilities
{
    public class DataMappingConfigurationEntry: ConfigurationElement
    {
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("code")]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("displayName")]
        public string DisplayName
        {
            get { return (string)this["displayName"]; }
            set { this["displayName"] = value; }
        }

        [ConfigurationProperty("sections")]
        public DataMappingConfigurationCollection Sections
        {
            get { return (DataMappingConfigurationCollection)this["sections"]; }
            set { this["sections"] = value; }
        }

        [ConfigurationProperty("data")]
        public DataMappingConfigurationCollection Data
        {
            get { return (DataMappingConfigurationCollection)this["data"]; }
            set { this["data"] = value; }
        }

    }
}