using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChipEmailer.Models
{
    public class SqlInClauseObject
    {
        public string InClause { get; set; }
        public DynamicParameters Parameters { get; set; }
    }
}
