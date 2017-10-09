namespace MicroStrutLibrary.Infrastructure.Core.Caching
{
    /// <summary>
    /// 缓存清理器接口
    /// </summary>
    public interface ICacheClear
    {
        /// <summary>
        /// 清理
        /// </summary>
        void Clear(string appCode);
    }
}
