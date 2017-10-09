using System;
using Microsoft.Extensions.Configuration;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration.Database
{
    public class DbConfigurationSource : IConfigurationSource
    {
        private DbSourceConfiguration config;

        public DbConfigurationSource(DbSourceConfiguration config)
        {
            this.config = config;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if (this.config == null)
            {
                throw new ArgumentNullException(nameof(DbSourceConfiguration));
            }

            return new DbConfigurationProvider(this.config);
        }
    }
}
