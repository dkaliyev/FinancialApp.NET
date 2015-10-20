using System.Configuration;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace FinancialThing.Services.Utilities
{
    public class DataMappingConfiguration: ConfigurationSection
    {
        [ConfigurationProperty("pages")]
        public DataMappingConfigurationCollection Pages
        {
            get { return (DataMappingConfigurationCollection)this["pages"]; }
            set { this["pages"] = value; }
        }

        private static DataMappingConfiguration instance;

        public static DataMappingConfiguration Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = (DataMappingConfiguration) ConfigurationManager.GetSection("DataMapping");
                }
                return instance;
            }
        }

        public static DataMappingConfigurationEntry GetPage(string pageName)
        {
            return Instance.Pages.FirstOrDefault(p => p.Name.ToLower() == pageName.ToLower());
        }

        public static DataMappingConfigurationEntry GetDataSection(string pageName, string sectionName)
        {
            var page = GetPage(pageName);
            if (page != null)
            {
                return page.Sections.FirstOrDefault(p => p.Name.ToLower() == sectionName.ToLower());
            }
            return null;
        }
    }
}