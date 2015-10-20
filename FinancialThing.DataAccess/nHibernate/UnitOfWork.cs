using System;
using System.Data;
using NHibernate;

namespace FinancialThing.DataAccess.nHibernate
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;


        public UnitOfWork(ISession session)
        {
            _session = session;
            _session.FlushMode = FlushMode.Auto;
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void Dispose()
        {
            if (_session.IsOpen)
            {
                _session.Close();
            }
        }

        public void Commit()
        {
            if (!_transaction.IsActive)
            {
                throw new InvalidOperationException("Oops! We don't have an active transaction");
            }
            _transaction.Commit();
        }

        public void RollBack()
        {
            if (_transaction.IsActive)
            {
                _transaction.Rollback();
            }
        }
    }
}