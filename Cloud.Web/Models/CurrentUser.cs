using Cloud.Framework.Assembly;
using Cloud.Web.Framework;

namespace Cloud.Web.Models
{
    public class CurrentUser : ICurrentUser
    {
        public int Id => 1;
        public string UserName => "13681555395";
        public string Password => "123456";

        public string Token
        {
            get
            {
                var token = HtmlHelper.GetCookieValue("token"); 
                return token;
            }
        }
    }
}