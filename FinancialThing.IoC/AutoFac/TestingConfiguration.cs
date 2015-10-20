using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace FinancialThing.IoC.AutoFac
{
    public class TestingConfiguration: IContainerConfiguration
    {
        public IDependencyResolver BuildResolver()
        {
            var builder = new ContainerBuilder();

            var config = new Configuration();
            config.Configure();
            config.AddAssembly("FinancialThing.DataAccess");
            var sessionFactory = config.BuildSessionFactory();

            builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>();
            builder.Register(x => new UnitOfWork(x.Resolve<ISession>())).As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<,>));

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            return resolver;
        }
    }
}
