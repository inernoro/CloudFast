using System.Threading.Tasks;
using Abp.Dependency;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public interface ISignin : ITransientDependency
    {
        Task SignInAsync(IUser<long> user, bool rememberMe = false);
    }
}