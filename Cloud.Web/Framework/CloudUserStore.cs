using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Cloud.Web.Framework
{
    public class CloudUserStore : ICloudUserStore
    {
        public void Dispose()
        {

        } 

        public Task CreateAsync(IUser<long> user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IUser<long> user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IUser<long> user)
        {
            throw new NotImplementedException();
        }

        public Task<IUser<long>> FindByIdAsync(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IUser<long>> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}