using System;
using System.Linq;
using System.Linq.Expressions;
using FinancialThing.Models;
using NHibernate;
using NHibernate.Linq;

namespace FinancialThing.DataAccess.nHibernate
{
    public class Repository<T>: IRepository<T, Guid>
        where T: class, IEntity
    {
        private readonly ISession _session;

        public ISession Session { get; set; }

        public Repository(ISession session)
        {
            Session = session;
            _session = session;
        }

        public virtual T GetById(Guid id)
        {
            return _session.Get<T>(id);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return _session.Query<T>();
        }

        public virtual T FindBy(Expression<Func<T, bool>> expression)
        {
            return GetQuery().Where(expression).SingleOrDefault();
        }

        public virtual Guid Add(T entity)
        {
            var id = _session.Save(entity);
            return (Guid)id;
        }

        public virtual void Update(T entity)
        {
            _session.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _session.Delete(entity);
        }

        public void SaveOrUpdate(T entity)
        {
            _session.SaveOrUpdate(entity);
        }
    }
}