using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpORM.Mapping
{
    public class BasePropertyConfiguration
    {
        public string PropertyName { get; private set; }
        public string ColumnName { get; private set; }
        public bool IsIgnored { get; private set; }
        public bool IsPrimaryKey { get; private set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="propertyName"></param>
        public BasePropertyConfiguration(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        public BasePropertyConfiguration ToColumn(string columnName)
        {
            this.ColumnName = columnName;
            return this;
        }

        public BasePropertyConfiguration Ignore()
        {
            this.IsIgnored = true;
            return this;
        }
    }
}
