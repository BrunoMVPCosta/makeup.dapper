using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeUpORM.Database
{
    public class DatabaseFactory : DatabaseFactoryBase
    {
        public DatabaseFactory(string connectionStrigName)
            : base(connectionStrigName)
        {

        }

        /// <summary>
        /// Returns a new instante of DbConnection based on the provider and connection string 
        /// passed as argument on the ctor.
        /// </summary>
        /// <returns>A new instance of System.Data.Common.DbConnection.</returns>
        public override IDbConnection Create()
        {
            IDbConnection connection = this.Provider.CreateConnection();
            connection.ConnectionString = this.ConnectionString;

            return connection;
        }
    }
}
