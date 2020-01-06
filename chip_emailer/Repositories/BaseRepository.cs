using Dapper;
using SnpCaller;
using SnpCaller.Contexts.Interfaces;
using SnpCaller.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SnpCaller.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public IDbContext DbContext { get; }

        public BaseRepository(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        // Synchronous
        public IEnumerable<T> Query<T>(
            string sql,
            object param = null,
            bool buffered = true,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.Query<T>(
                    sql,
                    param,
                    transaction,
                    buffered,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.Query<T>(
                        sql,
                        param,
                        transaction,
                        buffered,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public IEnumerable<TReturn> Query<TReturn>(
            string sql,
            Type[] types,
            Func<object[], TReturn> map,
            object param = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.Query(
                    sql,
                    types,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.Query(
                        sql,
                        types,
                        map,
                        param, 
                        transaction,
                        buffered,
                        splitOn,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public void QueryMultiple(
            string sql,
            Action<GridReader> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                map(
                    transaction.Connection.QueryMultiple(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        ));
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    map(
                        connection.QueryMultiple(
                            sql,
                            param,
                            transaction,
                            commandTimeout,
                            commandType
                            ));
                }
            }
        }

        public T QueryMultiple<T>(
            string sql,
            Func<GridReader, T> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return map(
                    transaction.Connection.QueryMultiple(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        ));
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return map(
                        connection.QueryMultiple(
                            sql,
                            param,
                            transaction,
                            commandTimeout,
                            commandType
                            ));
                }
            }
        }

        public T QueryFirstOrDefault<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.QueryFirstOrDefault<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.QueryFirstOrDefault<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public T QuerySingleOrDefault<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.QuerySingleOrDefault<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.QuerySingleOrDefault<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public int Execute(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.Execute(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.Execute(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public T ExecuteScalar<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return transaction.Connection.ExecuteScalar<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return connection.ExecuteScalar<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        // Asynchronous
        public async Task<IEnumerable<T>> QueryAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.QueryAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.QueryAsync<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(
            string sql,
            Type[] types,
            Func<object[], TReturn> map,
            object param = null,
            bool buffered = true,
            string splitOn = "Id",
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.QueryAsync(
                    sql,
                    types,
                    map,
                    param,
                    transaction,
                    buffered,
                    splitOn,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.QueryAsync(
                        sql,
                        types,
                        map,
                        param,
                        transaction,
                        buffered,
                        splitOn,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public async Task QueryMultipleAsync(
            string sql,
            Action<GridReader> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                map(
                    await transaction.Connection.QueryMultipleAsync(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        ));
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    map(
                        await connection.QueryMultipleAsync(
                            sql,
                            param,
                            transaction,
                            commandTimeout,
                            commandType
                            ));
                }
            }
        }

        public async Task<T> QueryMultipleAsync<T>(
            string sql,
            Func<GridReader, T> map,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return map(
                    await transaction.Connection.QueryMultipleAsync(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        ));
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return map(
                        await connection.QueryMultipleAsync(
                            sql,
                            param,
                            transaction,
                            commandTimeout,
                            commandType
                            ));
                }
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.QueryFirstOrDefaultAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.QueryFirstOrDefaultAsync<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.QuerySingleOrDefaultAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.QuerySingleOrDefaultAsync<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public async Task<int> ExecuteAsync(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.ExecuteAsync(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.ExecuteAsync(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(
            string sql,
            object param = null,
            int? commandTimeout = null,
            CommandType? commandType = null,
            IDbTransaction transaction = null
            )
        {
            if (transaction != null)
            {
                return await transaction.Connection.ExecuteScalarAsync<T>(
                    sql,
                    param,
                    transaction,
                    commandTimeout,
                    commandType
                    );
            }
            else
            {
                using (var connection = DbContext.CreateConnection())
                {
                    connection.Open();
                    return await connection.ExecuteScalarAsync<T>(
                        sql,
                        param,
                        transaction,
                        commandTimeout,
                        commandType
                        );
                }
            }
        }

        protected SqlInClauseObject GenerateSqlInClauseObject<T>(
            string parameterSeed, 
            T[] filterValues, 
            DynamicParameters paramsList
            )
        {
            var result = new SqlInClauseObject();
            int count = 1;
            string paramNames = "";
            foreach (var value in filterValues)
            {
                var parameterName = parameterSeed + count;
                paramNames += "@" + parameterName + ",";
                paramsList.Add(parameterName, value);
                count++;
            }

            result.InClause = paramNames.TrimEnd(',');
            result.Parameters = paramsList;
            return result;
        }

        protected Dictionary<int, IList<T>> GetInsertGroups<T>(
            IList<T> items, 
            int groupSize = 1000
            )
        {
            var results = new Dictionary<int, IList<T>>();
            int totoalGroup = (int)Math.Ceiling((double)items.Count / (double)groupSize);
            for (int currentGroup = 0; currentGroup < totoalGroup; currentGroup++)
            {
                results.Add(currentGroup, items.Skip(currentGroup * groupSize).Take(groupSize).ToList());
            }

            return results;
        }

        /// <summary>
        /// parse sql where clause from filter operation
        /// </summary>
        /// <param name="partialSQL">in format: AND tableA.ColumnA {{opetator}} {{parameter}}</param>
        /// <param name="paramName"></param>
        /// <param name="fieldName"></param>
        /// <param name="queryinfo">QueryInfo</param>
        /// <returns></returns>
        protected string ParseQueryOperation(string partialSQL, string paramName, string fieldName, QueryInfo queryinfo, DynamicParameters paramsList)
        {
            string parsedSQl = "";
            var fieldOperation = queryinfo.filters.FirstOrDefault(x => x.filterOperation.fieldName == fieldName
                && (IsUnaryOperator(x.filterOperation.valueOperator) || x.filterOperation.value != null));
            if (fieldOperation != null)
            {
                parsedSQl = ProcessFilterOperation(fieldOperation.filterOperation, partialSQL, paramName, fieldOperation.isRange, paramsList);
                if (fieldOperation.isRange)
                {
                    parsedSQl += ProcessFilterOperation(fieldOperation.filterOperation2, partialSQL, paramName, fieldOperation.isRange, paramsList);
                }
            }
            return parsedSQl;
        }

        private bool IsUnaryOperator(OperatorType operatorType)
        {
            return operatorType == OperatorType.HasValue || operatorType == OperatorType.HasNoValue;
        }

        private string ProcessFilterOperation(FilterOperation filterOp, string sql, string paramName, bool isRange, DynamicParameters paramsList)
        {
            if (filterOp.value == null && !IsUnaryOperator(filterOp.valueOperator)) return "";

            var parsedSQl = sql.Replace("{{operator}}", QueryInfoHelper.GetSQLOperator(filterOp.valueOperator));
            parsedSQl = parsedSQl.Replace("{{parameter}}",
                IsUnaryOperator(filterOp.valueOperator) ? "" : filterOp.ProcessParamName(paramName, isRange)
                );

            var paramNamesList = filterOp.GetParamNames(paramName, isRange);

            if (filterOp.valueOperator == OperatorType.In)
            {
                int indexAt = 0;
                var valueCommaSeperated = filterOp.GetValue<string>();
                var valueList = valueCommaSeperated.Split(',');
                foreach (var p in paramNamesList)
                {
                    ProcessParameterValueForInOperator(paramsList, filterOp, p, valueList[indexAt]);
                    indexAt++;
                }
            }
            else
            {
                ProcessParameterValue(paramsList, filterOp, paramNamesList[0]);
            }


            return parsedSQl;
        }

        private void ProcessParameterValue(DynamicParameters paramsList, FilterOperation filterOp, string paramName)
        {
            switch (filterOp.valueType)
            {
                case ValueDataType.String:
                    paramsList.Add(paramName, filterOp.GetValue<string>());
                    break;
                case ValueDataType.Number:
                    paramsList.Add(paramName, filterOp.GetValue<int>());
                    break;
                case ValueDataType.Date:
                    var dateValue = filterOp.GetValue<DateTime>();
                    dateValue = new DateTime(dateValue.Year, dateValue.Month, dateValue.Day);
                    paramsList.Add(paramName, dateValue);
                    break;
                case ValueDataType.Boolean:
                    var boolValue = filterOp.GetValue<bool>();
                    paramsList.Add(paramName, boolValue);
                    break;
                case ValueDataType.NullableBoolean:
                    var value = filterOp.GetValue<string>();
                    var newValue = value == "False" || value == "false" || value == "No" || value == "no" || value == "0" || string.IsNullOrEmpty(value) ? 0 : 1;
                    paramsList.Add(paramName, newValue);
                    break;
                default:
                    paramsList.Add(paramName, filterOp.GetValue<string>());
                    break;
            }
        }

        private void ProcessParameterValueForInOperator(DynamicParameters paramsList, FilterOperation filterOp, string paramName, string value)
        {
            value = value.Trim();
            switch (filterOp.valueType)
            {
                case ValueDataType.String:
                    paramsList.Add(paramName, value);
                    break;
                case ValueDataType.Number:
                    paramsList.Add(paramName, Convert.ChangeType(value, typeof(double)));
                    break;
                case ValueDataType.Date:
                    var dateValue = DateTime.Parse(value);
                    dateValue = new DateTime(dateValue.Year, dateValue.Month, dateValue.Day);
                    paramsList.Add(paramName, dateValue);
                    break;
                default:
                    paramsList.Add(paramName, value);
                    break;
            }
        }

        /// <summary>
        /// generate sor by clause from sort list
        /// </summary>
        /// <param name="queryInfo">QueryInfo</param>
        /// <param name="sortFieldMap">Dictionary<key, value>: Key by name from client side, value is table.column</param>
        /// <returns>Order By clause</returns>
        protected string CreateSortBy(QueryInfo queryInfo, Dictionary<string, string> sortFieldMap, string defaultOrderBy = "")
        {
            string sortBy = "";

            foreach (var s in queryInfo.sorts)
            {
                if (s.priority == 0) break;  //expect client side order the sorts list as 1, 2, 3, 4, .., 0, 0 , 0
                if (sortFieldMap.ContainsKey(s.fieldName))
                {
                    sortBy += sortFieldMap[s.fieldName] + GetSortDirection(s.sortDirection) + ", ";
                }
            }

            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = defaultOrderBy;
            }
            else
            {
                sortBy = sortBy.TrimEnd(' ', ',');
            }
            return sortBy;
        }

        private string GetSortDirection(SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Descending)
            {
                return " DESC ";
            }

            return " ASC ";
        }
    }
}
