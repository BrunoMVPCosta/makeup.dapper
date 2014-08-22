using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperORM.Mapping;

namespace DapperORM.SQLBuilder
{
    public class SelectOperatorBuilder : IQueryBuilder
    {
        private const string selectPattern = "SELECT {0}";

        private readonly IEntityMapper mapper;
        private readonly StringBuilder sb;

        private bool builded = false;

        public SelectOperatorBuilder(IEntityMapper mapper, StringBuilder sb)
        {
            this.mapper = mapper;
            this.sb = sb;
        }

        public StringBuilder Build()
        {
            if (builded) return sb;

            sb.AppendFormat(selectPattern, mapper.GetColumnsName());
            builded = true;

            return sb;
        }

        public string Compile()
        {
            Build();
            return sb.ToString();
        }
    }
}
