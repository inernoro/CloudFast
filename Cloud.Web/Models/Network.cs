using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Abp.Dependency;
using Abp.Runtime.Session;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Framework.Redis;
using Cloud.Manager.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace Cloud.Web.Models
{
    public static class Network
    {

        private static Func<Login> _loginFunc;
        private static Func<string> _getguidFunc;
        private static Action<string> _writeCookieAction;
        private static readonly Dictionary<string, CookieContainer> Dictionary = new Dictionary<string, CookieContainer>();

        static Network()
        {
            var redis = IocManager.Instance.Resolve<IRedisHelper>();
            var user = IocManager.Instance.Resolve<ICurrentUser>();
            _loginFunc = () => new Login { UserName = user.UserName, Password = user.Password };
            _getguidFunc = () => user.Token;

        }

        #region Http请求

        public static Root<T> HttpPost<T>(string url, string data)
        {
            return CenterControl<T>(url, data, DoPost);
        }

        public static Root<T> HttpGet<T>(string url)
        {
            return CenterControl<T>(url, null, DoGet);
        }

        private static Root<T> CenterControl<T>(string url, string data, Func<string, string, CookieContainer, string> func)
        {
            if (_getguidFunc == null)
                throw new Exception("抱歉,您并没有初始化获取Guid的方法");
            if (_loginFunc == null)
                throw new Exception("抱歉,您并没有初始化获取账户密码的方法");
            var guid = _getguidFunc();
            string message;
            NeedToLogIn:
            //如果guid等于Null获取键值对空间不存在这个guid则登陆
            if (guid.IsNullOrWhiteSpace() || !Dictionary.ContainsKey(guid))
            {
                var cookie = Login();
                guid = Guid.NewGuid().ToString();
                Dictionary.Add(guid, cookie);
                message = func(url, data, cookie);
                _writeCookieAction(guid);
            }
            else
            {
                var keyValueCookie = Dictionary[guid];
                message = func(url, data, keyValueCookie);
            }

            var result = JsonConvert.DeserializeObject<Root<T>>(message);
            if (!result.success && result.error.message.IndexOf("1001", StringComparison.Ordinal) != -1)
            {
                Dictionary.Remove(guid);
                goto NeedToLogIn;
            }
            return result;
        }



        private static CookieContainer Login()
        {
            var loginView = _loginFunc();
            var cookie = new CookieContainer();
            DoGet(string.Format(loginView.LoginUrl, loginView.UserName, loginView.Password), null, cookie);
            return cookie;
        }


        /// <summary>
        /// 调用前初始化此方法
        /// </summary>
        /// <param name="func"></param>
        public static void InitGetUserNameAndPassword(Func<Login> func)
        {
            _loginFunc = func;
        }

        /// <summary>
        /// 调用前初始化此方法
        /// </summary>
        /// <param name="func"></param>
        public static void InitGetGuid(Func<string> func)
        {
            _getguidFunc = func;
        }

        /// <summary>
        /// 调用写入Cookie的方法
        /// </summary>
        /// <param name="action"></param>
        public static void InitWriteCookie(Action<string> action)
        {
            _writeCookieAction = action;
        }


        #endregion

        #region CookieBase 

        /// <summary>
        /// HttpClient实现Post请求
        /// </summary>
        private static string DoPost(string url, string parament, CookieContainer cookie)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                CookieContainer = cookie
            };
            using (var http = new HttpClient(handler))
            {
                HttpContent content = new StringContent(parament);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = http.PostAsync(url, content).Result;
                response.EnsureSuccessStatusCode();
                cookie = handler.CookieContainer;
                var resultValue = response.Content.ReadAsStringAsync().Result;
                return resultValue;
            }
        }

        /// <summary>
        /// HttpClient实现Get请求
        /// </summary>
        private static string DoGet(string url, string parament, CookieContainer cookie)
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                CookieContainer = cookie
            };
            using (var http = new HttpClient(handler))
            {
                var getMessage = http.GetAsync(url);
                getMessage.Wait();
                var response = getMessage.Result;
                cookie = handler.CookieContainer;
                response.EnsureSuccessStatusCode();
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        #endregion
    }

    public class Login
    {
        public Login()
        {
            var url = IocManager.Instance.Resolve<IManagerUrlStrategy>();
            LoginUrl = url.LoginUrl;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string LoginUrl { get; }
    }
}