using System.Diagnostics;
using Abp.Dependency;
using Abp.Domain.Services;
using Abp.UI;

namespace Cloud.Framework.Assembly
{
    public class CloudCoreBase
    {
        private static ILuaAssembly Dynamic => IocManager.Instance.Resolve<ILuaAssembly>();

        public dynamic Physics
        {
            get
            {
                var basetype = GetType().BaseType;
                if (basetype != null) return Dynamic.NamespaceGetValue(basetype.FullName);
                throw new UserFriendlyException("BaseType Is Null");
            }
        }

        public dynamic Pointer
        {
            get
            {
                var dy = new StackTrace().GetFrame(1).GetMethod().Name;
                return Physics[dy];
            }
        }
    }
}