using System.Data.Common;
using Microsoft.Data.Sqlite;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public class SqliteDbFactoryProvider : IDbFactoryProvider
    {
        public DbProviderFactory GetFactory(string providerParameter)
        {
            return SqliteFactory.Instance;
        }
    }
}
