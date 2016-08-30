using System.Collections.Generic;
using Abp.AutoMapper;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;

namespace Cloud.ApiManager.Manager.Dtos
{
    [AutoMap(typeof(OpenDocumentResponse))]
    public class GetInfoOutput
    {
        [ContentDisplay("方法名称")]
        public string ActionName { get; set; }

        [ContentDisplay("方法解释")]
        public string ActionDisplay { get; set; }

        [ContentDisplay("控制器名称")]
        public string ControllerName { get; set; }

        [ContentDisplay("控制器解释")]
        public string ControllerDisplay { get; set; }

        [ContentDisplay("输入参数列表")]
        public List<ParamentType> InputParantrens { set; get; }

        [ContentDisplay("请求方式")]
        public HttpReponse Reponse { get; set; }

        [ContentDisplay("调用地址")]
        public string CallUrl { get; set; }

        [ContentDisplay("返回值类型")]
        public List<ParamentType> ReturnParament { get; set; }
    }
}