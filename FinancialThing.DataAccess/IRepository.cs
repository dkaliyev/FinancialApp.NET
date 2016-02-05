using System;
using System.Linq;
using System.Linq.Expressions;
using FinancialThing.Models;
using NHibernate;

namespace FinancialThing.DataAccess
{
    public interface IRepository<T, K>
        where T: class, IEntity
    {
        //ISession Session { get; set; }
        T GetById(K id);
        IQueryable<T> GetQuery();
        T FindBy(Expression<Func<T, bool>> expression);
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveOrUpdate(T entity);
    }
}