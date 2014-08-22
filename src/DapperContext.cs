using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MakeUpORM.Mapping;
using System.Data;
using MakeUpORM.SQLBuilder;
using MakeUpORM;
using MakeUpORM.Database;

namespace MakeUp
{
	public class DapperContext : IDisposable
	{
        private DatabaseFactory database;
        private Lazy<IDbConnection> connection;

        /// <summary>
        /// Ctor
        /// </summary>
        public DapperContext()
        {
            this.DatabaseModel = new DatabaseModel();
        }

		/// <summary>
		/// Ctor
		/// </summary>
		public DapperContext(string connectionStringName)
            : this()
		{
            this.database = new DatabaseFactory(connectionStringName);
            this.connection = new Lazy<IDbConnection>(() => this.database.Create());
		}

		public DatabaseModel DatabaseModel
		{
			get;
			private set;
		}

		public IQueryable<T> Query<T>()
		{
            return new Query<T>(new DapperQueryProvider<T>(this.connection.Value, this.DatabaseModel));
		}

		public bool Update<T>(T obj)
		{
			throw new NotImplementedException("Update not implemented");
		}

		public void Insert<T>(T obj)
		{
			throw new NotImplementedException("Update not implemented");
		}

		public void Delete<T>(T obj)
		{
			throw new NotImplementedException("Update not implemented");
		}

        public void Dispose()
        {
            if (connection.IsValueCreated && connection.Value != null)
            {
                connection.Value.Dispose();
            }
        }
    }
}
