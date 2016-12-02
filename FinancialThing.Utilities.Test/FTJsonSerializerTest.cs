﻿using FinancialThing.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialThing.Utilities.Test
{
    [TestClass]
    public class FTJsonSerializerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void TestDeserialize()
        {
            var str = "{\"Status\":0,\"Data\":[{\"FullName\":\"BlackRock Inc\",\"DateAdded\":\"2016-06-13T00:00:00\",\"StockName\":\"BLK\",\"StockExchange\":{\"Marker\":\"NYQ\",\"DisplayName\":\"NYSE\",\"DateAdded\":\"0001-01-01T00:00:00\",\"Id\":\"03c9dce5-193f-4982-ac28-900ad9664dc8\"},\"Sector\":{\"DisplayName\":\"Financials\",\"Code\":\"Financials\",\"Id\":\"6a92e255-f09c-475e-90e0-7e942cb8f0b0\"},\"Industry\":{\"DisplayName\":\"Asset management\",\"Code\":\"AsstMngt\",\"Id\":\"dc42054f-b1b1-4283-99f8-bc1b524852e4\",\"Sector\":{\"DisplayName\":\"Financials\",\"Code\":\"Financials\",\"Id\":\"6a92e255-f09c-475e-90e0-7e942cb8f0b0\"}},\"MinYear\":0,\"MaxYear\":0,\"Data\":[{\"Dictionary\":{\"DisplayName\":\"Gross dividend - common stock\",\"Code\":\"GDCS\",\"ParentCode\":\"CommonStockDiv\",\"Order\":\"2\",\"SiteName\":\"Gross dividend - common stock\",\"Id\":\"9379631d-8eb8-413d-b225-2ab0f8e8a670\"},\"Values\":[{\"DataValue\":1338.0,\"Year\":\"2014\"},{\"DataValue\":1476.0,\"Year\":\"2015\"},{\"DataValue\":1168.0,\"Year\":\"2013\"}]},{\"Dictionary\":{\"DisplayName\":\"DPS - common stock primary issue\",\"Code\":\"DCSPI\",\"ParentCode\":\"CommonStockDiv\",\"Order\":\"1\",\"SiteName\":\"DPS - common stock primary issue\",\"Id\":\"d989b1a8-58c7-4f52-9c05-15e4eab81c12\"},\"Values\":[{\"DataValue\":8.0,\"Year\":\"2014\"},{\"DataValue\":7.0,\"Year\":\"2013\"},{\"DataValue\":9.0,\"Year\":\"2015\"}]}],\"Id\":\"0ea26588-b27a-4f59-a631-9405c729bfb4\"},{\"FullName\":\"Barclays PLC\",\"DateAdded\":\"2016-06-12T00:00:00\",\"StockName\":\"BARC\",\"StockExchange\":{\"Marker\":\"LSE\",\"DisplayName\":\"London Stock Exchange\",\"DateAdded\":\"0001-01-01T00:00:00\",\"Id\":\"eacfff89-a0ba-46a7-b340-27b408952e41\"},\"Sector\":{\"DisplayName\":\"Financials\",\"Code\":\"Financials\",\"Id\":\"6a92e255-f09c-475e-90e0-7e942cb8f0b0\"},\"Industry\":{\"DisplayName\":\"Asset management\",\"Code\":\"AsstMngt\",\"Id\":\"dc42054f-b1b1-4283-99f8-bc1b524852e4\",\"Sector\":{\"DisplayName\":\"Financials\",\"Code\":\"Financials\",\"Id\":\"6a92e255-f09c-475e-90e0-7e942cb8f0b0\"}},\"MinYear\":0,\"MaxYear\":0,\"Data\":[{\"Dictionary\":{\"DisplayName\":\"Depreciation, supplemental\",\"Code\":\"DS\",\"ParentCode\":\"SupplementalIncome\",\"Order\":\"1\",\"SiteName\":\"Depreciation, supplemental\",\"Id\":\"2905fc12-a16a-41a5-8ff9-5174850a3bba\"},\"Values\":[{\"DataValue\":757.0,\"Year\":\"2014\"},{\"DataValue\":647.0,\"Year\":\"2013\"},{\"DataValue\":707.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Total special items\",\"Code\":\"TSI\",\"ParentCode\":\"SupplementalIncome\",\"Order\":\"3\",\"SiteName\":\"Total special items\",\"Id\":\"d690ab2e-3cf4-4fbb-91f4-ccf243c08d00\"},\"Values\":[{\"DataValue\":883.0,\"Year\":\"2013\"},{\"DataValue\":1029.0,\"Year\":\"2015\"},{\"DataValue\":1001.0,\"Year\":\"2014\"}]}],\"Id\":\"3283d833-50c5-425f-96ca-8e0f5f46d82d\"},{\"FullName\":\"International Business Machines Corp\",\"DateAdded\":\"2016-06-13T00:00:00\",\"StockName\":\"IBM\",\"StockExchange\":{\"Marker\":\"NYQ\",\"DisplayName\":\"NYSE\",\"DateAdded\":\"0001-01-01T00:00:00\",\"Id\":\"03c9dce5-193f-4982-ac28-900ad9664dc8\"},\"Sector\":{\"DisplayName\":\"Technology\",\"Code\":\"Technology\",\"Id\":\"febcfa97-48d0-418e-95ab-da4c610138c8\"},\"Industry\":{\"DisplayName\":\"Computer Hardware\",\"Code\":\"CompHardware\",\"Id\":\"7980400c-3cce-4f03-930d-3221c726b6c4\",\"Sector\":{\"DisplayName\":\"Technology\",\"Code\":\"Technology\",\"Id\":\"febcfa97-48d0-418e-95ab-da4c610138c8\"}},\"MinYear\":0,\"MaxYear\":0,\"Data\":[{\"Dictionary\":{\"DisplayName\":\"Depreciation/depletion\",\"Code\":\"DD\",\"ParentCode\":\"operations\",\"Order\":\"2\",\"SiteName\":\"Depreciation/depletion\",\"Id\":\"c52da54d-2ec0-4a58-8a4c-f8786c6d0fb5\"},\"Values\":[{\"DataValue\":2662.0,\"Year\":\"2015\"},{\"DataValue\":3327.0,\"Year\":\"2013\"},{\"DataValue\":3145.0,\"Year\":\"2014\"}]},{\"Dictionary\":{\"DisplayName\":\"Net income\",\"Code\":\"NI\",\"ParentCode\":\"operations\",\"Order\":\"1\",\"SiteName\":\"Net income\",\"Id\":\"430ed8d0-b532-42f6-8e7b-70ab2a01e5df\"},\"Values\":[{\"DataValue\":16483.0,\"Year\":\"2013\"},{\"DataValue\":13190.0,\"Year\":\"2015\"},{\"DataValue\":12022.0,\"Year\":\"2014\"}]},{\"Dictionary\":{\"DisplayName\":\"Total cash from operations\",\"Code\":\"TCFO\",\"ParentCode\":\"operations\",\"Order\":\"7\",\"SiteName\":\"Total cash from operations\",\"Id\":\"e486765a-5402-4d21-8d86-7ecee1e762df\"},\"Values\":[{\"DataValue\":16868.0,\"Year\":\"2014\"},{\"DataValue\":17008.0,\"Year\":\"2015\"},{\"DataValue\":17485.0,\"Year\":\"2013\"}]},{\"Dictionary\":{\"DisplayName\":\"Cash interest paid, supplemental\",\"Code\":\"CIPS\",\"ParentCode\":\"operations\",\"Order\":\"5\",\"SiteName\":\"Cash interest paid, supplemental\",\"Id\":\"ec1e580e-ed73-48c5-ace9-1a9c63a8ca06\"},\"Values\":[{\"DataValue\":1061.0,\"Year\":\"2014\"},{\"DataValue\":982.0,\"Year\":\"2013\"},{\"DataValue\":995.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Changes in working capital\",\"Code\":\"CIWC\",\"ParentCode\":\"operations\",\"Order\":\"6\",\"SiteName\":\"Changes in working capital\",\"Id\":\"f27f86c9-6bfd-40b9-94ce-daef1e2d0d52\"},\"Values\":[{\"DataValue\":1767.0,\"Year\":\"2014\"},{\"DataValue\":2444.0,\"Year\":\"2013\"},{\"DataValue\":2444.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Cash taxes paid, supplemental\",\"Code\":\"CTPS\",\"ParentCode\":\"operations\",\"Order\":\"4\",\"SiteName\":\"Cash taxes paid, supplemental\",\"Id\":\"101fb415-a9ce-4e7a-a0c8-5c5eb02d0358\"},\"Values\":[{\"DataValue\":2657.0,\"Year\":\"2015\"},{\"DataValue\":4024.0,\"Year\":\"2013\"},{\"DataValue\":5748.0,\"Year\":\"2014\"}]},{\"Dictionary\":{\"DisplayName\":\"Non-Cash items\",\"Code\":\"NCI\",\"ParentCode\":\"operations\",\"Order\":\"3\",\"SiteName\":\"Non-Cash items\",\"Id\":\"a7751f2b-0610-49af-9b75-2ae08566d4d4\"},\"Values\":[{\"DataValue\":1020.0,\"Year\":\"2015\"},{\"DataValue\":2358.0,\"Year\":\"2014\"},{\"DataValue\":378.0,\"Year\":\"2013\"}]}],\"Id\":\"36fae592-1d83-4c94-b6e6-188d84ff75f5\"},{\"FullName\":\"Tesla Motors Inc\",\"DateAdded\":\"2016-06-13T00:00:00\",\"StockName\":\"TSLA\",\"StockExchange\":{\"Marker\":\"NSQ\",\"DisplayName\":\"NASDAQ\",\"DateAdded\":\"0001-01-01T00:00:00\",\"Id\":\"541d377f-bbd1-41c1-a065-03dbd552b44f\"},\"Sector\":{\"DisplayName\":\"Technology\",\"Code\":\"Technology\",\"Id\":\"febcfa97-48d0-418e-95ab-da4c610138c8\"},\"Industry\":{\"DisplayName\":\"Telecommunications Equipment\",\"Code\":\"TelecomEquip\",\"Id\":\"e855f140-0ca2-4c5c-a20f-badffddc6044\",\"Sector\":{\"DisplayName\":\"Technology\",\"Code\":\"Technology\",\"Id\":\"febcfa97-48d0-418e-95ab-da4c610138c8\"}},\"MinYear\":0,\"MaxYear\":0,\"Data\":[{\"Dictionary\":{\"DisplayName\":\"Changes in working capital\",\"Code\":\"CIWC\",\"ParentCode\":\"operations\",\"Order\":\"6\",\"SiteName\":\"Changes in working capital\",\"Id\":\"f27f86c9-6bfd-40b9-94ce-daef1e2d0d52\"},\"Values\":[{\"DataValue\":493.0,\"Year\":\"2015\"},{\"DataValue\":257.0,\"Year\":\"2014\"},{\"DataValue\":149.0,\"Year\":\"2013\"}]},{\"Dictionary\":{\"DisplayName\":\"Cash taxes paid, supplemental\",\"Code\":\"CTPS\",\"ParentCode\":\"operations\",\"Order\":\"4\",\"SiteName\":\"Cash taxes paid, supplemental\",\"Id\":\"101fb415-a9ce-4e7a-a0c8-5c5eb02d0358\"},\"Values\":[{\"DataValue\":0.0,\"Year\":\"2013\"},{\"DataValue\":3.0,\"Year\":\"2014\"},{\"DataValue\":9.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Cash interest paid, supplemental\",\"Code\":\"CIPS\",\"ParentCode\":\"operations\",\"Order\":\"5\",\"SiteName\":\"Cash interest paid, supplemental\",\"Id\":\"ec1e580e-ed73-48c5-ace9-1a9c63a8ca06\"},\"Values\":[{\"DataValue\":32.0,\"Year\":\"2015\"},{\"DataValue\":9.0,\"Year\":\"2013\"},{\"DataValue\":21.0,\"Year\":\"2014\"}]},{\"Dictionary\":{\"DisplayName\":\"Net income\",\"Code\":\"NI\",\"ParentCode\":\"operations\",\"Order\":\"1\",\"SiteName\":\"Net income\",\"Id\":\"430ed8d0-b532-42f6-8e7b-70ab2a01e5df\"},\"Values\":[{\"DataValue\":74.0,\"Year\":\"2013\"},{\"DataValue\":294.0,\"Year\":\"2014\"},{\"DataValue\":889.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Total cash from operations\",\"Code\":\"TCFO\",\"ParentCode\":\"operations\",\"Order\":\"7\",\"SiteName\":\"Total cash from operations\",\"Id\":\"e486765a-5402-4d21-8d86-7ecee1e762df\"},\"Values\":[{\"DataValue\":265.0,\"Year\":\"2013\"},{\"DataValue\":57.0,\"Year\":\"2014\"},{\"DataValue\":524.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Depreciation/depletion\",\"Code\":\"DD\",\"ParentCode\":\"operations\",\"Order\":\"2\",\"SiteName\":\"Depreciation/depletion\",\"Id\":\"c52da54d-2ec0-4a58-8a4c-f8786c6d0fb5\"},\"Values\":[{\"DataValue\":106.0,\"Year\":\"2013\"},{\"DataValue\":423.0,\"Year\":\"2015\"},{\"DataValue\":232.0,\"Year\":\"2014\"}]},{\"Dictionary\":{\"DisplayName\":\"Non-Cash items\",\"Code\":\"NCI\",\"ParentCode\":\"operations\",\"Order\":\"3\",\"SiteName\":\"Non-Cash items\",\"Id\":\"a7751f2b-0610-49af-9b75-2ae08566d4d4\"},\"Values\":[{\"DataValue\":435.0,\"Year\":\"2015\"},{\"DataValue\":84.0,\"Year\":\"2013\"},{\"DataValue\":262.0,\"Year\":\"2014\"}]}],\"Id\":\"41e18ed0-8910-4ce1-9e07-789b11056cf1\"},{\"FullName\":\"Exxon Mobil Corp\",\"DateAdded\":\"2016-07-30T00:00:00\",\"StockName\":\"XOM\",\"StockExchange\":{\"Marker\":\"NYQ\",\"DisplayName\":\"NYSE\",\"DateAdded\":\"0001-01-01T00:00:00\",\"Id\":\"03c9dce5-193f-4982-ac28-900ad9664dc8\"},\"Sector\":{\"DisplayName\":\"Oil and Gas\",\"Code\":\"OilAndGas\",\"Id\":\"5b475c04-dfaf-46b0-a594-90d4b81394a6\"},\"Industry\":{\"DisplayName\":\"Oil & Gas Producers\",\"Code\":\"OAGP\",\"Id\":\"3c1d326c-15c3-45ac-91e4-d77dfafe8772\",\"Sector\":{\"DisplayName\":\"Oil and Gas\",\"Code\":\"OilAndGas\",\"Id\":\"5b475c04-dfaf-46b0-a594-90d4b81394a6\"}},\"MinYear\":0,\"MaxYear\":0,\"Data\":[{\"Dictionary\":{\"DisplayName\":\"Cash taxes paid, supplemental\",\"Code\":\"CTPS\",\"ParentCode\":\"SupplementalIncome\",\"Order\":\"4\",\"SiteName\":\"Cash taxes paid, supplemental\",\"Id\":\"82dcb029-1bd5-4d1d-81c1-dbfc3d95e4d2\"},\"Values\":[{\"DataValue\":25066.0,\"Year\":\"2014\"},{\"DataValue\":25066.0,\"Year\":\"2013\"},{\"DataValue\":7269.0,\"Year\":\"2015\"}]},{\"Dictionary\":{\"DisplayName\":\"Cash interest paid, supplemental\",\"Code\":\"CIPS2\",\"ParentCode\":\"SupplementalIncome\",\"Order\":\"2\",\"SiteName\":\"Cash interest paid, supplemental\",\"Id\":\"79085a25-5cb3-4a34-b788-d54b5cc31081\"},\"Values\":[{\"DataValue\":426.0,\"Year\":\"2014\"},{\"DataValue\":586.0,\"Year\":\"2015\"},{\"DataValue\":426.0,\"Year\":\"2013\"}]},{\"Dictionary\":{\"DisplayName\":\"Depreciation, supplemental\",\"Code\":\"DS\",\"ParentCode\":\"SupplementalIncome\",\"Order\":\"1\",\"SiteName\":\"Depreciation, supplemental\",\"Id\":\"2905fc12-a16a-41a5-8ff9-5174850a3bba\"},\"Values\":[{\"DataValue\":18048.0,\"Year\":\"2015\"},{\"DataValue\":17182.0,\"Year\":\"2013\"},{\"DataValue\":17297.0,\"Year\":\"2014\"}]}],\"Id\":\"56cc7cea-2b0b-4fb0-bdc6-b113b5ec497f\"}],\"Message\":\"\"}";
            var obj = FTJsonSerializer<Response<IEnumerable<ExpandedCompany>>>.Deserialize(str);
        }
    }
}
