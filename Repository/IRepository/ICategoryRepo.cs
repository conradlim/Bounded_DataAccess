using Bounded.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bounded.DataAccess.Repository.IRepository
{
    public interface  ICategoryRepo : IRepository<Category>
    {
        void Update(Category category);        
    }
}
