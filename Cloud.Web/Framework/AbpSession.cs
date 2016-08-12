using System;
using System.Threading;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{

    public class AbpSession : IAbpSession, ISingletonDependency
    {
        public long? UserId
        {
            get
            {
                var userIdAsString = Thread.CurrentPrincipal.Identity.GetUserId();
                if (string.IsNullOrEmpty(userIdAsString))
                {
                    return null;
                }
                return Convert.ToInt64(userIdAsString);
            }
        }


        public int? TenantId { get; }
        public MultiTenancySides MultiTenancySide { get; }
        public long? ImpersonatorUserId { get; }
        public int? ImpersonatorTenantId { get; }

    }
}