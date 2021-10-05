using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bounded.DataAccess.Repository.IRepository
{
    ///*******************
    /// CLIM
    /// Generic Repo
    /// Uknown object type so pass in <T> where T is a 'class'
    //*******************
    public interface IRepository<T> where T : class
    {
        // retrieve category based on id
        T Get(int id);

        // retrieve a list of category and evaluate an expression
        // retrieve if you need to 'order by'
        // includeproperties - for eager loading. Helpful for FK references i.e. Product and then Category for product
        IEnumerable<T> GetAll(           
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IQueryable<T>> orderBy = null,
            string includeproperties = null
            );
        
        // same as T Get(int id), but with added use of expression
        T GetFirstOrDefault(
          // If evaluate an expression
          Expression<Func<T, bool>> filter = null,          
          string includeproperties = null
          );

        void Add(T entity);

        void Remove(int id);
        //void Remove(T entity);

        //remove range of entities
        void RemoveRange(IEnumerable<T> entity);
    }

}
