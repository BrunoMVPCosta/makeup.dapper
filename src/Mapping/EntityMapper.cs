using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpORM.Mapping
{
    public interface IEntityMapper
    {
        /// <summary>
        /// Table name
        /// </summary>
        string TableName { get; }

        ICollection<BasePropertyConfiguration> Properties { get; }
    }

    public class EntityMapper<TEntity> : IEntityMapper where TEntity : class
    {
        public string TableName { get; private set; }

        public ICollection<BasePropertyConfiguration> Properties { get; private set; }

        protected EntityMapper()
        {
            Properties = new List<BasePropertyConfiguration>();
        }

        public void ToTable(string tableName)
        {
            this.TableName = tableName;
        }

        public BasePropertyConfiguration Property(Expression<Func<TEntity, int>> expression)
        {
            string propertyName = string.Empty;
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                propertyName = member.Member.Name;
            }

            BasePropertyConfiguration property = new BasePropertyConfiguration(propertyName);
            Properties.Add(property);

            return property;
        }

        public BasePropertyConfiguration Property(Expression<Func<TEntity, string>> expression)
        {
            string propertyName = string.Empty;
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                propertyName = member.Member.Name;
            }

            BasePropertyConfiguration property = new BasePropertyConfiguration(propertyName);
            Properties.Add(property);

            return property;
        }

        public BasePropertyConfiguration Property(Expression<Func<TEntity, object>> expression)
        {
            string propertyName = string.Empty;
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                propertyName = member.Member.Name;
            }

            BasePropertyConfiguration property = new BasePropertyConfiguration(propertyName);
            Properties.Add(property);

            return property;
        }
    }

    public static class  EntityMapperExtensions
    {
        public static StringBuilder GetColumnsName(this IEntityMapper mapper, StringBuilder sb)
        {
            if (mapper.Properties.Count == 0) 
                return sb;

            var columns = mapper.Properties.Where(p => p.IsIgnored == false);

            int countMembers = columns.Count();
            for (int i = 1; i <= countMembers; i++)
            {
                var column = columns.ElementAt(i - 1);

                sb.AppendFormat("[{0}] AS [{1}]", column.ColumnName, column.PropertyName);

                if (i != countMembers)
                {
                    sb.Append(", ");
                }
            }
            
            return sb;
        }

        public static BasePropertyConfiguration GetColumn(this IEntityMapper mapper, string propertyName)
        {
           return mapper
               .Properties
               .FirstOrDefault(p => p.PropertyName == propertyName);
        }
    }
}
