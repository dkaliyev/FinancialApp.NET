using System;
using System.Security.Cryptography.X509Certificates;
using NHibernate;

namespace FinancialThing.DataAccess
{
    public interface IUnitOfWork: IDisposable
    {
        void Commit();
        void RollBack();
    }
}