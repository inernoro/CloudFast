﻿using System.Web.Mvc;
using Abp.Dependency;
using Cloud.ApiManager.CodeBuild;
using Cloud.ApiManager.TestManager;
using Cloud.ApiManager.TestManager.Dtos;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Framework.Dapper;
using Cloud.Framework.Mongo;

namespace Cloud.Web.Controllers
{
    public class HomeController : CloudControllerBase
    {
        public ActionResult Index()
        { 
            return View("~/Areas/ApiManager/Views/Manager/List.cshtml");
        }
    }
}