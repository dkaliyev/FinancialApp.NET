using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.IoC.AutoFac;
using FinancialThing.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace FinancialThing.DataAccess.Test
{
    [TestClass]
    public class TestRepository
    {
        private IDependencyResolver resolver;
        private IRepository<Dictionary, Guid> dicRepository;
        private IList<Dictionary> dictionaries;
        private IRepository<Company, Guid> companyRepo;
        private ISessionFactory sessionFactory;
        private IUnitOfWork uow;

        [TestInitialize]
        public void Setup()
        {
            resolver = new TestingConfiguration().BuildResolver();
            dicRepository = (IRepository<Dictionary, Guid>)resolver.GetService(typeof(IRepository<Dictionary, Guid>));
            dictionaries = dicRepository.GetQuery().ToList();
            //companyRepo = (IRepository<Company>) resolver.GetService(typeof (IRepository<Company>));
            sessionFactory = (ISessionFactory)resolver.GetService(typeof(ISessionFactory));
        }

        [TestMethod]
        public void TestDataRepositoryAddCompany()
        {
            #region Arrange



            var datum = new Data()
            { 
                Dictionary = dictionaries.Where(d => d.Code == "TI").SingleOrDefault()
            };

            var Values = new HashSet<Value>()
            {
                new Value()
                {
                    DataValue = "123",
                    Year = "2014",
                    Data = datum
                },
                new Value()
                {
                    DataValue = "124",
                    Year = "2013",
                    Data = datum
                },
                new Value()
                {
                    DataValue = "125",
                    Year = "2012",
                    Data = datum
                }
            };

            datum.Values = Values;

            var data = new HashSet<Data>()
            {
                datum
            };

            var statement = new StatementClass()
            {
                Data = data,
                Dictionary = dictionaries.Where(d => d.Code == "Assets").FirstOrDefault()
            };

            var statements = new HashSet<StatementClass>()
            {
                statement
            };

            var page = new Page()
            {
                Dictionary = dictionaries.Where(d => d.Code == "BalanceSh").FirstOrDefault()
            };

            statement.Page = page;

            page.Statements = statements;

            datum.StatementClass = statement;

            var pages = new HashSet<Page>()
            {
                page
            };

            var financial = new Financials()
            {
                Pages = pages
            };



            page.Financials = financial;

            var company = new Company()
            {
                FullName = "Test company",
                DateAdded = DateTime.Now,
                StockName = "LSE",
                Financials = financial
            };

            financial.Company = company;

            #endregion

            #region Act



            var session = sessionFactory.OpenSession();
            uow = new UnitOfWork(session);
            companyRepo = new DatabaseRepository<Company>(session);
            var company2 = new Company()
            {
                FullName = "Test Company",
                DateAdded = DateTime.Now,
                StockName = "LSE"
            };

            var financial2 = new Financials()
            {
                Company = company2
            };
            //var res = companyRepo.Add(company);
            //var statementsClassRepo = (IRepository<StatementClass>)resolver.GetService(typeof(IRepository<StatementClass>));
            //var st = statementsClassRepo.GetById(new Guid("86912737-70d0-4ddf-8c56-c4cc191d96bd"));
            //st.Data.Add(new Data()
            //{
            //    Value = "443",
            //    Dictionary = dictionaries.Where(d=>d.Code=="").FirstOrDefault(),
            //    StatementClass = st
            //});

            //statementsClassRepo.Update(st);

            var res = companyRepo.Add(company);

            uow.Commit();
            
            var rescompany = companyRepo.GetById(res.Id);

            #endregion

            #region Assert
            Assert.IsNotNull(rescompany);
            Assert.AreEqual("Test company", rescompany.FullName);
            #endregion
        }


        public void TestGetAndUpdatedPartial()
        {
            var session = sessionFactory.OpenSession();
            uow = new UnitOfWork(session);
            var statementsClassRepo = new DatabaseRepository<StatementClass>(session);
            var statement = statementsClassRepo.GetById(new Guid("01fb145e-c65e-4de3-8dfe-7b20eecdcf29"));
            
            statementsClassRepo.Update(statement);
            uow.Commit();
            Assert.IsNotNull(statement);
        }
    }
}
