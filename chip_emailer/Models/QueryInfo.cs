using System;
using System.Collections.Generic;

namespace ChipEmailer.Models
{
    public class QueryInfo
    {
        public QueryInfo()
        {
            filters = new List<FieldOperations>();
            sorts = new List<Sort>();
        }
        public int itemsPerPage { get; set; }
        public int pageNumber { get; set; }

        public IList<FieldOperations> filters { get; set; }
        public IList<Sort> sorts { get; set; }
    }

    public enum OperatorType
    {
        Equal = 0,
        LessThan,
        LessAndEqualThen,
        GreaterThan,
        GreaterAndEqualThan,
        NotEqual,
        In,
        Like,
        HasValue,
        HasNoValue
    }

    public enum ValueDataType
    {
        String = 0,
        Number,
        Date,
        Boolean,
        NullableBoolean,
    }

    public enum SortDirection
    {
        Ascending = 0,
        Descending,
        None
    }

    public class Sort
    {
        public string fieldName { get; set; }
        public int priority { get; set; }
        public SortDirection sortDirection { get; set; }

        public Sort()
        {
            sortDirection = SortDirection.None;
            priority = 0;
        }
    }

    public class FieldOperations
    {
        public FieldOperations()
        {
            filterOperation = new FilterOperation();
            filterOperation2 = new FilterOperation();
            isRange = false;
        }
        public FilterOperation filterOperation { get; set; }
        public FilterOperation filterOperation2 { get; set; }
        public bool isRange { get; set; }
    }

    public class FilterOperation
    {
        public string fieldName { get; set; }
        public OperatorType valueOperator { get; set; }
        public object value { get; set; }
        public ValueDataType valueType { get; set; }
        public bool isSecondary { get; set; }   //range filter'ss send operation

        public string ProcessParamName(string paramSeed, bool isRange)
        {
            var paramList = GetParamNames(paramSeed, isRange);

            if (valueOperator == OperatorType.In)
            {
                string inParams = "(";
                int count = 1;
                foreach (var v in paramList)
                {
                    inParams += "@" + paramSeed + count + ",";
                    count++;
                }
                return inParams.TrimEnd(',') + ")";
            }
            else if (valueOperator == OperatorType.Like)
            {
                return "'%' + @" + paramList[0] + "+ '%'";
            }
            else
            {
                return "@" + paramList[0];
            }
        }

        public IList<string> GetParamNames(string paramSeed, bool isRange)
        {
            var paramNames = new List<string>();

            if (valueOperator == OperatorType.In)
            {
                var valueList = value.ToString().Split(',');
                int count = 1;
                foreach (var v in valueList)
                {
                    paramNames.Add(paramSeed + count);
                    count++;
                }
            }
            else
            {
                paramNames.Add(paramSeed + (isRange ? (isSecondary ? "Max" : "Min") : ""));
            }

            return paramNames;
        }
        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }

    public static class QueryInfoHelper
    {
        public static string GetSQLOperator(OperatorType operatorType)
        {
            return operatorMap[operatorType];
        }

        private static Dictionary<OperatorType, string> operatorMap = new Dictionary<OperatorType, string>()
        {
            {OperatorType.Equal, " = " },
            {OperatorType.GreaterAndEqualThan, " >= " },
            {OperatorType.GreaterThan, " > " },
            {OperatorType.In, " in " },
            {OperatorType.LessAndEqualThen, " <= " },
            {OperatorType.LessThan, " < " },
            {OperatorType.Like, " like " },
            {OperatorType.NotEqual, " != " },
            {OperatorType.HasValue, " is not null " },
            {OperatorType.HasNoValue, " is null " }
        };
    }
}
