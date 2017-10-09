using System.Data.Common;
using System.Data.SqlClient;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public class SqlServerDbFactoryProvider : IDbFactoryProvider
    {
        public DbProviderFactory GetFactory(string providerParameter)
        {
            return SqlClientFactory.Instance;
        }
    }
}
