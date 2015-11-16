using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using FinancialThing.DataAccess;
using FinancialThing.IoC.AutoFac;
using FinancialThing.Models;
using FinancialThing.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace FinancialThing.Utilities.Test
{
    [TestClass]
    public class FTHAPParserTest
    {
        private Mock<IDataGrabber> mockGrabber;

        private IDependencyResolver resolver;
        private IRepository<Dictionary, Guid> dicRepository;
        private IRepository<StockExchange, Guid> sexRepo;
        private IList<Dictionary> dictionaries;
        private ISessionFactory sessionFactory;
            
        [TestInitialize]
        public void Setup()
        {
            resolver = new TestingConfiguration().BuildResolver();
            dicRepository = (IRepository<Dictionary, Guid>)resolver.GetService(typeof(IRepository<Dictionary, Guid>));
            sexRepo = (IRepository<StockExchange, Guid>)resolver.GetService(typeof(IRepository<StockExchange, Guid>));
            dictionaries = dicRepository.GetQuery().ToList();
            string BSbody = System.IO.File.ReadAllText("BS.html").Replace("\"", "");
            string CFBody = System.IO.File.ReadAllText("CF.html").Replace("\"", "");
            string ISBody = System.IO.File.ReadAllText("IS.html").Replace("\"", "");
            string IMTName = System.IO.File.ReadAllText("IMT.html");

            mockGrabber = new Mock<IDataGrabber>();
            mockGrabber.Setup<string>(g => g.Grab(It.IsAny<string>())).Returns<string>(s =>
            {
                if (
                    s.Equals(
                        "http://markets.ft.com/research/Markets/Tearsheets/Financials?s=IMT:LSE&subview=BalanceSheet"))
                    return BSbody;
                if (s.Equals("http://markets.ft.com/research/Markets/Tearsheets/Financials?s=IMT:LSE&subview=CashFlow"))
                    return CFBody;
                if (
                    s.Equals(
                        "http://markets.ft.com/research/Markets/Tearsheets/Financials?s=IMT:LSE&subview=IncomeStatement"))
                    return ISBody;
                if (s.Equals("http://markets.ft.com/research/Markets/Tearsheets/Financials?s=IMT:LSE"))
                    return IMTName;
                return "";
            });
        }

        [TestMethod]
        public void TestParseMock()
        {
            var parser = new FTHAPParser(mockGrabber.Object, dicRepository);
            var company = parser.Parse("IMT", sexRepo.GetQuery().FirstOrDefault(d => d.Marker=="LSE"));
            Assert.AreEqual("Imperial Tobacco Group PLC".ToLower(), company.FullName.ToLower());
        }

        [TestMethod]
        public void TestParseWeb()
        {
            var parser = new FTHAPParser(new FTDataGrabber(), dicRepository);
            var company = parser.Parse("IMT", sexRepo.GetQuery().FirstOrDefault(d => d.Marker == "LSE"));
            Assert.AreEqual("Imperial Tobacco Group PLC".ToLower(), company.FullName.ToLower());
            Assert.AreEqual(7, company.Financials.Pages.Where(p => p.Dictionary.Code == "IncomeStatement").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "NormalizedIncome").FirstOrDefault().Data.Count);
            Assert.AreEqual(9, company.Financials.Pages.Where(p => p.Dictionary.Code == "IncomeStatement").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "IncomeTaxes").FirstOrDefault().Data.Count);
            Assert.AreEqual(13, company.Financials.Pages.Where(p => p.Dictionary.Code == "BalanceSh").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "Assets").FirstOrDefault().Data.Count);
        }
    }
}
