using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Dapper;
using System.Data;
using MakeUpORM.SQLBuilder;
using MakeUp;
using MakeUpORM.Mapping;
using System.Diagnostics;

namespace MakeUpORM
{
    public class DapperQueryProvider<T> : IQueryProvider
    {
        private readonly IDbConnection connection;
        private readonly DatabaseModel model;

        public DapperQueryProvider(IDbConnection connection, DatabaseModel model)
        {
            this.connection = connection;
            this.model = model;
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new Query<TElement>(this, expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            IEntityMapper mapper = this.model.MapperWithType(typeof(T));
            QueryVisitor translator = new QueryVisitor(mapper, new SqlBuilder.WhereVisitor(), new SqlBuilder.ProjectorVisitor());
            var tuple = translator.Translate(expression);

            var query = tuple.Item1;

            var dynParms = new DynamicParameters();
            foreach (KeyValuePair<string, object> entry in tuple.Item2)
            {
                dynParms.Add(entry.Key, entry.Value);
            }

            if (Debugger.IsAttached)
                Debug.WriteLine("Query {0} with parameters {1}", query, dynParms.ToString());

            return (TResult)connection.Query<T>(query, dynParms);
        }

        public object Execute(Expression expression)
        {
            throw new NotSupportedException("A type must be declared");
            //QueryVisitor translator = new QueryVisitor(new DatabaseModel());
            //translator.Translate(expression);

            //return null;
        }
    }
}
