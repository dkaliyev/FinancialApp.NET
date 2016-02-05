using System;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using FinancialThing.Models;
using FinancialThing.Services.Utilities;
using FinancialThing.Utilities;
//using FinancialThing.Web.DataAccess;
using Newtonsoft.Json;
using NHibernate;

namespace FinancialThing.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var config = new NHibernate.Cfg.Configuration();

            //Local MySQL instance
            //config.Configure(Assembly.GetCallingAssembly(), "FinancialThing.Services.nhibernate.mysql.cfg.xml");

            //Local MSSQL instance
            config.Configure(Assembly.GetExecutingAssembly(), "FinancialThing.Services.nhibernate.cfg.xml");

            //Production instance
            //config.Configure(Assembly.GetExecutingAssembly(), "FinancialThing.Services.nhibernate.prod.cfg.xml");

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
                .As<IParser<Company, StockExchange>>()
                .InstancePerRequest();

            //Data merger
            builder.Register(x => new CompanyDataMerger()).As<IDataMerger<Company>>().InstancePerLifetimeScope();

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            //log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize(); 

        }
    }
}
