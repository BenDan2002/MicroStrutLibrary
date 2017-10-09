using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace MicroStrutLibrary.Infrastructure.Core.Exception
{
    public class MicroStrutLibraryException : System.Exception
    {
        /// <summary>
        /// 分类
        /// </summary>
        public string Catalog { get; private set; }

        /// <summary>
        /// 日志消息类别
        /// </summary>
        public LogLevel LogLevel { get; private set; }

        #region 基本构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="catalog">分类，一般是类的命名空间</param>
        /// <param name="logType">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public MicroStrutLibraryException(string catalog, LogLevel logType, string message, System.Exception innerException) : base(message, innerException)
        {
            Catalog = catalog;
            LogLevel = logType;
        }
        #endregion
    }

    public static class MicroStrutLibraryExceptionHelper
    {
        /// <summary>
        /// 抛出异常
        /// </summary>
        /// <param name="catalog">异常分类，一般是类的命名空间</param>
        /// <param name="logLevel">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public static void Throw(string catalog, LogLevel logLevel, string message, System.Exception innerException = null)
        {
            throw new MicroStrutLibraryException(catalog, logLevel, message, innerException);
        }

        /// <summary>
        /// 表达式为true时抛出异常
        /// </summary>
        /// <param name="catalog">异常分类，一般是类的命名空间</param>
        /// <param name="expression">表达式</param>
        /// <param name="logLevel">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public static void TrueThrow(bool expression, string catalog, LogLevel logLevel, string message, System.Exception innerException = null)
        {
            if (expression)
            {
                Throw(catalog, logLevel, message, innerException);
            }
        }

        /// <summary>
        /// 表达式为False时抛出异常
        /// </summary>
        /// <param name="catalog">异常分类，一般是类的命名空间</param>
        /// <param name="expression">表达式</param>
        /// <param name="logLevel">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public static void FalseThrow(bool expression, string catalog, LogLevel logLevel, string message, System.Exception innerException = null)
            => TrueThrow(!expression, catalog, logLevel, message, innerException);

        /// <summary>
        /// Object为null时抛出异常
        /// </summary>
        /// <param name="obj">要检查的实体</param>
        /// <param name="catalog">异常分类，一般是类的命名空间</param>
        /// <param name="logLevel">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public static void IsNull(object obj, string catalog, LogLevel logLevel, string message, System.Exception innerException = null)
            => TrueThrow(obj == null, catalog, logLevel, message, innerException);

        /// <summary>
        /// 字符串为空时抛出异常
        /// </summary>
        /// <param name="stringObj">要检查的字符串</param>
        /// <param name="catalog">异常分类，一般是类的命名空间</param>
        /// <param name="logLevel">消息类型</param>
        /// <param name="message">消息</param>
        /// <param name="innerException">内部异常</param>
        public static void IsNullOrEmpty(string stringObj, string catalog, LogLevel logLevel, string message, System.Exception innerException = null)
            => TrueThrow(string.IsNullOrWhiteSpace(stringObj), catalog, logLevel, message, innerException);

        /// <summary>
        /// 获取真正的异常信息
        /// </summary>
        /// <param name="ex">外层异常</param>
        /// <returns>真正的异常信息</returns>
        public static System.Exception GetRealException(System.Exception ex)
        {
            System.Exception exception = ex;
            while (ex != null)
            {
                exception = ex.InnerException ?? ex;
                ex = ex.InnerException;
            }
            return exception;
        }

        /// <summary>
        /// 获取InnerException中的MicroStrutLibraryException的异常信息
        /// </summary>
        /// <param name="ex">外层异常</param>
        /// <returns>真正的异常信息</returns>
        public static MicroStrutLibraryException GetInnerMicroStrutLibraryException(System.Exception ex)
        {
            MicroStrutLibraryException exception = null;
            while (ex != null)
            {
                if (ex is MicroStrutLibraryException)
                    exception = ex as MicroStrutLibraryException;
                ex = ex.InnerException;
            }
            return exception;
        }

        /// <summary>
        /// 得到异常的消息串，主要获得了StackTrace属性
        /// </summary>
        /// <param name="ex">异常</param>
        /// <param name="showFullMessage">是否忽略所有错误信息</param>
        /// <returns>异常的消息串</returns>
        public static string GetExceptionMessage(this System.Exception ex, bool showFullMessage = false)
        {
            string msg = ex.Message;
            if (!string.IsNullOrWhiteSpace(ex.StackTrace))
            {
                List<string> list = ex.StackTrace.Split(new char[] { '\r', '\n' }).ToList();
                list = list.Where(s => s.Contains("\\"))
                    .Select(s => s.Substring(s.LastIndexOf('\\') + 1))
                    .Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                if (!showFullMessage && list.Count > 0)
                    msg = string.Concat(msg, ",", list.Aggregate((s1, s2) => string.Concat(s1, "，", s2)));
                else
                    msg = string.Concat(msg, ex.StackTrace);
            }
            if (ex.InnerException != null && !string.IsNullOrWhiteSpace(ex.InnerException.Message) && ex.StackTrace != ex.InnerException.StackTrace)
                msg = string.Concat(msg, "【内部异常】", GetExceptionMessage(ex.InnerException, showFullMessage));
            return msg;
        }
    }
}
