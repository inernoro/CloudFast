using System.Diagnostics;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.UI;
using Cloud.Framework.Assembly;
namespace Cloud.Framework
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CloudAppServiceBase : ApplicationService
    {
        /// <summary>
        /// 中断并执行
        /// </summary>
        /// <param name="message"></param>
        public void SystemInfo(string message)
        {
            throw new UserFriendlyException(message);
        }

    }
}