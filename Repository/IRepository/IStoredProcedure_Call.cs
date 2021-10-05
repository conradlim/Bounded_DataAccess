using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bounded.DataAccess.Repository.IRepository
{
    public interface IStoredProcedure_Call : IDisposable
    {
        // return a single value
        // using dapper to pass in parameters into storedProc
        T Scalar<T>(string procedureName, DynamicParameters param = null);

        // execute to db i.e. save/insert/delete sql commands
        void Execute(string procedureName, DynamicParameters param = null);

        // return only one row of data
        T OneRecord<T>(string procedureName, DynamicParameters param = null); 

        // return all rows
        IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null);

        // storedProc returning 2 tables
        Tuple<IEnumerable<T1>,IEnumerable<T2>> List<T1,T2>(string procedureName, DynamicParameters param = null);
    }
}
