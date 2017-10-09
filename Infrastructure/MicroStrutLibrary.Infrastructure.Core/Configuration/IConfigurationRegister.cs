using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Configuration
{
    public interface IConfigurationRegister
    {
        void Register(IServiceCollection service, IConfigurationRoot root);
    }
}
