using System;
using System.Linq;
using System.Linq.Expressions;
using FinancialThing.Models;
using NHibernate;
using NHibernate.Linq;

namespace FinancialThing.DataAccess.nHibernate
{
    public class DatabaseRepository<T>: IDatabaseRepository<T, Guid>
        where T: class, IEntity
    {
        private readonly ISession _session;

        private ISession Session { get; set; }

        public DatabaseRepository(ISession session)
        {
            Session = session;
            _session = session;
        }

        public T GetById(Guid id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> GetQuery()
        {
            return _session.Query<T>();
        }

        public T FindBy(Expression<Func<T, bool>> expression)
        {
            return GetQuery().Where(expression).SingleOrDefault();
        }

        public T Add(T entity)
        {
            var id = _session.Save(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _session.Update(entity);
        }

        public void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void SaveOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }
    }
}