using System.ComponentModel;
using System.Web.Http.Dependencies;

namespace FinancialThing.IoC
{
    public interface IContainerConfiguration
    {
        IDependencyResolver BuildResolver();

    }
}