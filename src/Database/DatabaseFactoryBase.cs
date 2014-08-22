using System.Configuration;
using System.Data;
using System.Data.Common;
namespace MakeUpORM.Database
{
    public abstract class DatabaseFactoryBase
    {
        protected DbProviderFactory Provider { get; private set; }
        protected string ConnectionString { get; private set; }

        protected DatabaseFactoryBase(string connectionStrigName)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            if (settings.Count == 0)
            {
                throw new ConfigurationErrorsException("Connection string collection is empty");
            }

            ConnectionStringSettings setting = settings[connectionStrigName];
            if (setting == null)
            {
                throw new ConfigurationErrorsException(string.Format("ConnectionString {0} not found", connectionStrigName));
            }

            this.Provider = DbProviderFactories.GetFactory(setting.ProviderName);
            this.ConnectionString = setting.ConnectionString;
        }

        /// <summary>
        /// Returns a new instante of DbConnection based on the provider and connection string 
        /// passed as argument on the ctor.
        /// </summary>
        /// <returns>A new instance of System.Data.Common.DbConnection.</returns>
        public abstract IDbConnection Create();
    }
}
