using Microsoft.AspNet.Identity;

namespace Cloud.Web.Models
{
    public class User : IUser<long>
    {
        public long Id { get; set; }

        public string UserName { get; set; }

    }
}