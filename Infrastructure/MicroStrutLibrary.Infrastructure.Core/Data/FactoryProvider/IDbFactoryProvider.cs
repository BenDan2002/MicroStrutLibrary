using System.Data.Common;

namespace MicroStrutLibrary.Infrastructure.Core.Data
{
    public interface IDbFactoryProvider
    {
        /// <summary>
        /// 获得工厂
        /// </summary>
        /// <param name="providerParameter"></param>
        /// <returns></returns>
        DbProviderFactory GetFactory(string providerParameter);
    }
}
