using Bounded.DataAccess.Data;
using Bounded.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bounded.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _ctx;
        public UnitOfWork(ApplicationDbContext ctx)
        {
            _ctx = ctx;           
            Category = new CategoryRepo(_ctx); //uow will be able to access categoryRepo
            StoredProcedure_Call = new StoredProc_Call(_ctx);
        }

        public ICategoryRepo Category { get; private set; }
        public IStoredProcedure_Call StoredProcedure_Call{ get; private set; }

        public void Dispose()
        {
            _ctx.Dispose();
        }
         
        public void Save()
        {
            // method in repository class will call on uow.Save to execute final save to db
            _ctx.SaveChanges();
        }
    }
}
