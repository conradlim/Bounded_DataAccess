using Bounded.DataAccess.Data;
using Bounded.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bounded.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // when modifying database, add dbContext
        private readonly ApplicationDbContext _ctx;
        internal DbSet<T> dbSet;
        
        public Repository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            this.dbSet = _ctx.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);            
        }

        public T Get(int id)
        {
            return dbSet.Find(id);            
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IQueryable<T>> orderBy = null, string includeproperties = null)
        {
            IQueryable<T> query = dbSet;
            //check if filter is null 
            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeproperties != null)
            {
                //add eager loading
                // linking entities with a Product FK by appending each separated 
                foreach (var includeProp in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeproperties = null)
        {
            IQueryable<T> query = dbSet;
            //check if filter is null 
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeproperties != null)
            {
                //add eager loading
                foreach (var includeProp in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
        
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entity = dbSet.Find(id);
            dbSet.Remove(entity);
        }

        //public void Remove(T entity)
        //{
        //    dbSet.Remove(entity);
        //}
        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
