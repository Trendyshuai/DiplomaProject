using MyMovie.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Core.RepositoryInterface
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int id);

        IEnumerable<T> List();

        IEnumerable<T> List(Expression<Func<T, bool>> predicate);

        bool Add(T entity);

        bool Update(T entity);

        T Insert(T entity);

        bool Delete(T entity);

        bool Save();

        IEnumerable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int count, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc);
    }
}
