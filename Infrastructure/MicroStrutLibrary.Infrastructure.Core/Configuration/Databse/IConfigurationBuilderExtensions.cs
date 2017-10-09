using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration.Database
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddDbSource(this IConfigurationBuilder builder, IOptions<DbSourceConfiguration> options)
        {
            MicroStrutLibraryExceptionHelper.IsNull(options, typeof(IConfigurationBuilderExtensions).FullName, LogLevel.Error, "配置为空！");

            return builder.Add(new DbConfigurationSource(options.Value));
        }
    }
}
