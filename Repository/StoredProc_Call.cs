using Bounded.DataAccess.Data;
using Bounded.DataAccess.Repository.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bounded.DataAccess.Repository
{
    public class StoredProc_Call : IStoredProcedure_Call
    {
        private readonly ApplicationDbContext _ctx;
        private static string ConnectionString = "";
        public StoredProc_Call(ApplicationDbContext ctx)
        {
            _ctx = ctx;
            // set connection string in order to call stored procs
            ConnectionString = _ctx.Database.GetDbConnection().ConnectionString;
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }

        public void Execute(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                conn.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<T> List<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                return conn.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Tuple<IEnumerable<T1>, IEnumerable<T2>> List<T1, T2>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var result = SqlMapper.QueryMultiple(conn, procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                var tblA= result.Read<T1>().ToList();
                var tblB = result.Read<T2>().ToList();

                if(tblA != null && tblB != null)
                {
                    return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(tblA,tblB);
                }                
            }
            return new Tuple<IEnumerable<T1>, IEnumerable<T2>>(new List<T1>(), new List<T2>());
        }

        public T OneRecord<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var rowResult =  conn.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(rowResult.FirstOrDefault(), typeof(T));
            }            
        }

        public T Scalar<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var scalarValue = conn.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
                return (T)Convert.ChangeType(scalarValue,typeof(T));
            }            
        }
    }
}
