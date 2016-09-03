using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cloud.Web.Areas.ApiManager.Controllers
{
    public class TestController : Controller
    {
        /// <summary>
        /// 测试首页,一般显示所有自动异常情况
        /// </summary>
        /// <returns></returns>
        // GET: ApiManager/Test
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 数据页,可以查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query()
        {
            return View();
        }

        /// <summary>
        /// sql监控和查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Monitor()
        {
            return View();
        }

        /// <summary>
        /// 功能测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Function()
        {
            return View();
        }
        /// <summary>
        /// 功能测试
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }
        /// <summary>
        /// 功能测试
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
    }
}