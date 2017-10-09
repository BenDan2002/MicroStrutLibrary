using Microsoft.Extensions.DependencyInjection;

namespace MicroStrutLibrary.Infrastructure.Core.Dependency
{
    public interface IServiceRegister
    {
        void Register(IServiceCollection services);
    }
}
