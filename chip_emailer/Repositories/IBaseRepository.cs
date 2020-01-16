using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ChipEmailer.Repositories
{
    public interface IBaseRepository
    {
        IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        IEnumerable<TReturn> Query<TReturn>(
            string sql,
            Type[] types,
            Func<object[], TReturn> map,
            object param = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        void QueryMultiple(
            string sql,
            Action<GridReader> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        T QueryMultiple<T>(
            string sql,
            Func<GridReader, T> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        T QueryFirstOrDefault<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        T QuerySingleOrDefault<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        int Execute(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        T ExecuteScalar<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<IEnumerable<TReturn>> QueryAsync<TReturn>(
            string sql,
            Type[] types,
            Func<object[], TReturn> map,
            object param = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task QueryMultipleAsync(
            string sql,
            Action<GridReader> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<T> QueryMultipleAsync<T>(
            string sql,
            Func<GridReader, T> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<int> ExecuteAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
        Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            );
    }
}
