using System.Reflection;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using FinancialThing.DataAccess;
using FinancialThing.DataAccess.nHibernate;
using NHibernate;
using NHibernate.Cfg;

namespace FinancialThing.IoC.AutoFac
{
    public class AutoFacConfiguration: IContainerConfiguration
    {
        public IDependencyResolver BuildResolver()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var config = new Configuration();
            config.Configure();
            config.AddAssembly("FinancialThing.DataAccess");
            var sessionFactory = config.BuildSessionFactory();

            builder.RegisterInstance(sessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>().InstancePerRequest();
            builder.Register(x => new UnitOfWork(x.Resolve<ISession>())).As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<,>));

            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            return resolver;
        }
    }
}
