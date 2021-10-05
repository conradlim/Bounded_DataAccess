using Bounded.DataAccess.Data;
using Bounded.DataAccess.Repository.IRepository;
using Bounded.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bounded.DataAccess.Repository
{
    //inherit from Repository using Category as the Generic Class
    public class CategoryRepo : Repository<Category>,ICategoryRepo
    {
        private readonly ApplicationDbContext _ctx;
        public CategoryRepo(ApplicationDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public void Update(Category category)
        {
            var catObj = _ctx.Categories.FirstOrDefault(s => s.ID == category.ID);
            if(catObj != null)
            {
               catObj.Name = category.Name;
                _ctx.SaveChanges();
            }                        
        }
    }
}
