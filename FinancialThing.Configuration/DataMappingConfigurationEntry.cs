using System.Configuration;

namespace FinancialThing.Configuration
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

        [ConfigurationProperty("ftName")]
        public string FtName
        {
            get { return (string)this["ftName"]; }
            set { this["ftName"] = value; }
        }

        [ConfigurationProperty("order")]
        public string Order
        {
            get { return (string) this["order"]; }
            set { this["order"] = value; }
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

        [ConfigurationProperty("count")]
        public string Count
        {
            get { return (string)this["count"]; }
            set { this["count"] = value; }
        }

    }
}