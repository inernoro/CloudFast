using System;
using System.Web;

namespace Cloud.Web.Framework
{
    /// <summary> 
    /// 通用类库，提供Html级别的处理函数
    /// </summary>
   public  class HtmlHelper
    {
        #region cookie操作
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                cookie.Value = string.Empty;
                //cookie.Domain = Cloud.Manager.Tools.Utils.GetAppValueByName("Domain");
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        /// <summary>
        /// 清除所有站点cookie
        /// </summary>
        public static void ClearCookie()
        {
            var cookies = HttpContext.Current.Request.Cookies.AllKeys;
            foreach (var item in cookies)
            {
                SetCookie(item, string.Empty, -10 );
            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = HttpUtility.UrlDecode(cookie.Value);
            }
            return str;
        }
        
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expDay">几天后过期</param>
        public static void SetCookie(string cookiename, string cookievalue, double expDay)
        { 
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = HttpUtility.UrlEncode(cookievalue),
                Expires =  DateTime.Now.AddDays(expDay),
                //Domain = Cloud.Manager.Tools.Utils.GetAppValueByName("Domain")
            }; 
            HttpContext.Current.Response.Cookies.Add(cookie);
        } 
        #endregion

        /// <summary>
        /// 解码页面传来的encodeURIComponent编码字符串
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string DecodeURI(string html)
        {
            return HttpUtility.UrlDecode(html);
        }

        /// <summary>
        /// 添加一个Cookie（7*24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            //cookie 设置为一年 2013-12-05
            SetCookie(cookiename, cookievalue, DateTime.Now.AddYears(1));
        }

        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = HttpUtility.UrlEncode(cookievalue),
                Expires = expires,
                //Domain = Cloud.Manager.Tools.Utils.GetAppValueByName("Domain")
            };
            //cookie.Domain = "fao.com";
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
