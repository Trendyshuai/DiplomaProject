using MyMovie.Core.Entities;
using MyMovie.Core.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMovie.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly EfDbContext _dbContext;
        public Repository(EfDbContext efDbContext)
        {
            _dbContext = efDbContext;
        }

        public bool Add(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Added;
            return Save();
        }

        public bool Delete(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Deleted;
            return Save();
        }

        public T Insert(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Added;
            _dbContext.SaveChanges();
            return entity;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public IEnumerable<T> List()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbContext
                .Set<T>()
                .Where<T>(predicate)
                .AsEnumerable();
        }

        /// <summary>
        /// 单元工厂
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            return Save();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="s">泛型</typeparam>
        /// <param name="pageIndex">页标</param>
        /// <param name="pageSize">分页数</param>
        /// <param name="count">总数</param>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IEnumerable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int count, Expression<Func<T, bool>> whereLambda, Expression<Func<T, s>> orderByLambda, bool isAsc)
        {
            var temp = _dbContext.Set<T>().Where<T>(whereLambda);
            count = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderByLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }
    }
}
