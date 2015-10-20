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
        private IList<Dictionary> dictionaries;
        private ISessionFactory sessionFactory;
            
        [TestInitialize]
        public void Setup()
        {
            resolver = new TestingConfiguration().BuildResolver();
            dicRepository = (IRepository<Dictionary, Guid>)resolver.GetService(typeof(IRepository<Dictionary, Guid>));
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
        public void TestParseBS()
        {
            var parser = new FTHAPParser(mockGrabber.Object, dictionaries);
            var company = parser.Parse("IMT", "LSE");
            Assert.AreEqual("Imperial Tobacco Group PLC".ToLower(), company.FullName.ToLower());
        }
    }
}
