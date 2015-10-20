using FinancialThing.Models;
using FinancialThing.Services.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinancialThing.Utilities.Test
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestMethod]
        public void TestConfiguration()
        {
            var inst = DataMappingConfiguration.Instance;
            var section = DataMappingConfiguration.GetDataSection("BalanceSheet", "assets");
            Assert.IsTrue(section.Name == "assets");
            section = DataMappingConfiguration.GetDataSection("CashFlow", "investing");
            Assert.IsTrue(section.Name == "investing");
        }
    }
}