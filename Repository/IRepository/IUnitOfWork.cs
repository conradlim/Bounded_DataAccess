using System;
using System.Collections.Generic;
using System.Text;

namespace Bounded.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepo Category { get; }
        IStoredProcedure_Call StoredProcedure_Call { get; }
    }
}
