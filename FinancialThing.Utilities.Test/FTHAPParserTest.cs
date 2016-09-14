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
using FinancialThing.DataAccess.nHibernate;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using HtmlAgilityPack;

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
        //private ISessionFactory sessionFactory;

        [TestInitialize]
        public void Setup()
        {
            var builder = new ContainerBuilder();

            var config = new NHibernate.Cfg.Configuration();
            config.Configure(Assembly.GetExecutingAssembly(), "FinancialThing.Utilities.Test.nhibernate.cfg.xml");
            config.AddAssembly("FinancialThing.DataAccess");
            var sessionFactory = config.BuildSessionFactory();

            builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>();
            builder.Register(x => new UnitOfWork(x.Resolve<ISession>())).As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(DatabaseRepository<>)).As(typeof(IRepository<,>));

            var container = builder.Build();
            resolver = new AutofacWebApiDependencyResolver(container);
            dicRepository = (IRepository<Dictionary, Guid>)resolver.GetService(typeof(IRepository<Dictionary, Guid>));
            sexRepo = (IRepository<StockExchange, Guid>)resolver.GetService(typeof(IRepository<StockExchange, Guid>));
            dictionaries = dicRepository.GetQuery().ToList();
            string BSbody = System.IO.File.ReadAllText("BS.html").Replace("\"", "");
            string CFBody = System.IO.File.ReadAllText("CF.html").Replace("\"", "");
            string ISBody = System.IO.File.ReadAllText("IS.html").Replace("\"", "");
            string IMTName = System.IO.File.ReadAllText("IMT.html");

            mockGrabber = new Mock<IDataGrabber>();
            mockGrabber.Setup(g => g.Get(It.IsAny<string>())).Returns<string>(s =>
              {
                  if (
                      s.Equals(
                          "http://markets.ft.com/data/equities/tearsheets/financials?s=FB:NSQ&subview=BalanceSheet"))
                      return Task.FromResult<string>(BSbody);
                  if (s.Equals("http://markets.ft.com/data/equities/tearsheets/financials?s=FB:NSQ&subview=CashFlow"))
                      return Task.FromResult<string>(CFBody);
                  if (
                      s.Equals(
                          "http://markets.ft.com/data/equities/tearsheets/financials?s=FB:NSQ&subview=IncomeStatement"))
                      return Task.FromResult<string>(ISBody);
                  if (s.Equals("http://markets.ft.com/data/equities/tearsheets/financials?s=FB:NSQ"))
                      return Task.FromResult<string>(IMTName);
                  return Task.FromResult<string>(string.Empty);
              });
        }

        [TestMethod]
        public void TestGetCompanyName()
        {
            var parser = new FTHAPParser(mockGrabber.Object, dicRepository);
            var name = parser.GetCompanyName("FB", new StockExchange { Marker = "NSQ" });
            Assert.AreEqual("Facebook Inc", name.Result);
        }

        [TestMethod]
        public void TestGetYears()
        {
            var parser = new FTHAPParser(mockGrabber.Object, dicRepository);
            var years = parser.GetYears("http://markets.ft.com/data/equities/tearsheets/financials?s=FB:NSQ&subview=IncomeStatement");
            var res = years.Result;
            Assert.AreEqual(3, res.Count);
        }

        [TestMethod]
        [Ignore]
        public void TestParseMock()
        {
            //var dicRepository = (IRepository<Dictionary, Guid>)resolver.GetService(typeof(IRepository<Dictionary, Guid>));
            var parser = new FTHAPParser(mockGrabber.Object, dicRepository);
            //var company = parser.Parse("IMT", sexRepo.GetQuery().FirstOrDefault(d => d.Marker=="LSE"));
            //Assert.AreEqual("Imperial Tobacco Group PLC".ToLower(), company.FullName.ToLower());
        }

        [TestMethod]
        [Ignore]
        public void TestParseWeb()
        {
            //    var parser = new FTHAPParser(new FTDataGrabber(), dicRepository);
            //    var company = parser.Parse("IMT", sexRepo.GetQuery().FirstOrDefault(d => d.Marker == "LSE"));
            //    Assert.AreEqual("Imperial Tobacco Group PLC".ToLower(), company.FullName.ToLower());
            //    Assert.AreEqual(7, company.Financials.Pages.Where(p => p.Dictionary.Code == "IncomeStatement").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "NormalizedIncome").FirstOrDefault().Data.Count);
            //    Assert.AreEqual(9, company.Financials.Pages.Where(p => p.Dictionary.Code == "IncomeStatement").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "IncomeTaxes").FirstOrDefault().Data.Count);
            //    Assert.AreEqual(13, company.Financials.Pages.Where(p => p.Dictionary.Code == "BalanceSh").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "Assets").FirstOrDefault().Data.Count);
            //    Assert.AreEqual(7, company.Financials.Pages.Where(p => p.Dictionary.Code == "CashFlow").FirstOrDefault().Statements.Where(s => s.Dictionary.Code == "operations").FirstOrDefault().Data.Count);
        }
    }
}
