using MakeUpORM.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpORM.SQLBuilder.MsSql
{
    public class MsSqlDialect : Dialect
    {
        public override string LeftIdentifier
        {
            get
            {
                return "[";
            }
        }

        public override string RightIdentifier
        {
            get
            {
                return "]";
            }
        }
    }
}
