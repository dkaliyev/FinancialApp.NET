using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.Models;
using FinancialThing.Utilities;
using NHibernate;
using NHibernate.Cfg;

namespace FinancialThing.IoC.AutoFac
{
    public class AutoFacConfiguration: IContainerConfiguration
    {
        public IDependencyResolver BuildResolver()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetCallingAssembly());
            //builder.Register(x => new CompanyRepository(x.Resolve<IDataGrabber>())).As<ICompanyServiceRepository>().SingleInstance();
            var config = new Configuration();
            
            //Local MySQL instance
            //config.Configure(Assembly.GetCallingAssembly(), "FinancialThing.Services.nhibernate.mysql.cfg.xml");

            //Local MSSQL instance
            //config.Configure(Assembly.GetCallingAssembly(), "FinancialThing.Services.nhibernate.cfg.xml");

            //Azure instance
            config.Configure(Assembly.GetCallingAssembly(), "FinancialThing.Services.nhibernate.cfg.xml");

            config.AddAssembly("FinancialThing.DataAccess");
            var sessionFactory = config.BuildSessionFactory();
            //Data Access
            builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerRequest();
            builder.Register(x => new UnitOfWork(x.Resolve<ISession>())).As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(DatabaseRepository<>)).As(typeof(IDatabaseRepository<,>));
            

            //Parser and grabber
            builder.Register(x => new FTDataGrabber()).As<IDataGrabber>().InstancePerRequest();
            builder.Register(x => new FTHAPParser(x.Resolve<IDataGrabber>(), x.Resolve<IDatabaseRepository<Dictionary, Guid>>()))
                .As<IParser<Company,StockExchange>>()
                .InstancePerRequest();

            //Data merger
            builder.Register(x => new CompanyDataMerger()).As<IDataMerger<Company>>().InstancePerLifetimeScope();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            return resolver;
        }
    }
}
