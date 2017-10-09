using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDbProviders(this IServiceCollection services)
        {
            IServiceProvider provider = services.BuildServiceProvider();
            IOptions<DbConnectionConfiguration> options = provider.GetRequiredService<IOptions<DbConnectionConfiguration>>();

            DatabaseUtility.InitDbProviders(options.Value);

            return services;
        }
    }
}
