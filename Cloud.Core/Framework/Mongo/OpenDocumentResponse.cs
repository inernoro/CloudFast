using System.Collections.Generic;

namespace Cloud.Framework.Mongo
{
    public sealed class OpenDocumentResponse
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

        //public Type ReturnParament { get; set; }
    }

    public enum HttpReponse
    {
        NotDefined,
        Post,
        Get,
        Put,
        Delete
    }

    public class ParamentType
    {
        [ContentDisplay("名称")]
        public string Name { get; set; }

        [ContentDisplay("类型")]
        public string Type { get; set; }

        //[ContentDisplay("范围")]
        //public string Range { get; set; }

        [ContentDisplay("是否必填")]
        public bool IsNotNull { get; set; }

        [ContentDisplay("描述")]
        public string Description { get; set; }

        //[ContentDisplay("子元素")]
        //public List<ParamentType> ChildrenType { get; set; }
    }
}