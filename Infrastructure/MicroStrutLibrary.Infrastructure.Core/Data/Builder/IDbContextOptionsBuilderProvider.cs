using Microsoft.EntityFrameworkCore;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public interface IDbContextOptionsBuilderProvider
    {
        DbContextOptionsBuilder UseDb(DbContextOptionsBuilder builder, DbConnectionStringSettings connectionStringSettings, DbConnectionProviderSettings connectionProviderSettings);
    }
}
