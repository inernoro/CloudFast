using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.Application.Services;
using Abp.Dependency;
using Abp.Domain.Services;
using Abp.Extensions;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    /// <summary>
    /// 核心程序及解析驱动
    /// </summary>
    public class AssemblyStrategy : StrategyBase, IAssemblyStrategy
    {
        private readonly ICloudBuildStrategy _cloudBuildStrategy;

        public AssemblyStrategy(ICloudBuildStrategy cloudBuildStrategy)
        {
            _cloudBuildStrategy = cloudBuildStrategy;
        }

        private const string LocalUrl = "";

        public IEnumerable<OpenDocumentResponse> Build(Assembly assembly)
        {

            var result = assembly.GetTypes().Where(type =>
            {
                if (type.IsPublic && type.IsInterface && typeof(IApplicationService).IsAssignableFrom(type))
                    return IocManager.Instance.IsRegistered(type);
                return false;
            });

            return BuildMains(result);
        }


        public static List<OpenDocumentResponse> BuildMains(IEnumerable<Type> source)
        {
            var list = new List<OpenDocumentResponse>();
            foreach (var type in source)
            {
                //命名空间
                var namespacestr = GetConventionalServiceName(type);
                var methods = type.GetMethods();
                var members = type.FindMembers(
                    MemberTypes.Method, BindingFlags.Public
                    | BindingFlags.Static | BindingFlags.NonPublic
                    | BindingFlags.Instance | BindingFlags.DeclaredOnly, Type.FilterName, "*"
                    );

                for (var index = 0; index < members.Length; index++)
                {

                    var response = new OpenDocumentResponse();
                    //控制器消息
                    var controllerDesplay = type.GetCustomAttribute(typeof(ContentDisplayAttribute), true);
                    if (controllerDesplay != null)
                    {
                        var attribute = ((ContentDisplayAttribute)controllerDesplay);
                        if (!attribute.IsShow)
                            continue;
                        response.ControllerDisplay = attribute.DisplayInfo;
                    }
                    //控制器名字
                    response.ControllerName = namespacestr;
                    //方法名字
                    response.ActionName = methods[index].Name;

                    //获取请求方式
                    response.Reponse = HttpReponse.Post;

                    foreach (var name in methods[index].GetCustomAttributes().Select(node => node.GetType().Name))
                    {
                        if (name.Equals("HttpPostAttribute"))
                        {
                            response.Reponse = HttpReponse.Post;
                        }
                        else if (name.Equals("HttpGetAttribute"))
                        {
                            response.Reponse = HttpReponse.Get;
                        }
                    }
                    //调用地址
                    response.CallUrl = $"{LocalUrl}/api/services/app/{response.ControllerName}/{response.ActionName}";
                    response.CallUrl = response.CallUrl.Replace("//", "/").Replace("http:/", "http://");
                    //action描述
                    var actionAttribute = methods[index].GetCustomAttribute(typeof(ContentDisplayAttribute), true);
                    if (actionAttribute != null)
                    {
                        var attribute = ((ContentDisplayAttribute)actionAttribute);
                        if (!attribute.IsShow)
                            continue;
                        //方法描述
                        response.ActionDisplay = ((ContentDisplayAttribute)actionAttribute).DisplayInfo;


                    }
                    //input参数集合
                    response.InputParantrens = GetParamentTypeForListOnes(methods[index].GetParameters(), false);
                    //返回值参数集合
                    response.ReturnParament = GetParamentTypeForListOnes(new[] { methods[index].ReturnParameter }, true);
                    list.Add(response);
                }
            }
            return list;
        }

        private static string GetConventionalServiceName(Type type)
        {
            var str = type.Name;
            if (str.EndsWith("ApplicationService"))
                str = str.Substring(0, str.Length - "ApplicationService".Length);
            else if (str.EndsWith("AppService"))
                str = str.Substring(0, str.Length - "AppService".Length);
            else if (str.EndsWith("Service"))
                str = str.Substring(0, str.Length - "Service".Length);
            if (str.Length > 1 && str.StartsWith("I") && char.IsUpper(str, 1))
                str = str.Substring(1);
            return str.ToCamelCase();
        }

        private static List<ParamentType> GetParamentTypeForListOnes(IReadOnlyList<ParameterInfo> parameters, bool isReturnParament)
        {
            var list = new List<ParamentType>();
            //一个参数
            if (parameters.Count == 1)
            {
                //是对象
                if (
                      !parameters[0].ParameterType.IsPrimitive
                    && parameters[0].ParameterType != typeof(bool?)
                    && parameters[0].ParameterType != typeof(double?)
                    && parameters[0].ParameterType != typeof(decimal?)
                    && parameters[0].ParameterType != typeof(int?)
                    && parameters[0].ParameterType != typeof(string)
                    )
                {
                    //如果是返回对象则多执行一层
                    if (isReturnParament)
                    {
                        try
                        {
                            if (parameters[0].ParameterType.GetGenericArguments().Length == 0)
                            {
                                return null;
                            }
                            var dy = parameters[0].ParameterType.GetGenericArguments().First();
                            //是数组
                            var item = dy.FullName.IndexOf("List`1", StringComparison.Ordinal) != -1;
                            if (item)
                            {
                                var arr = dy.GetGenericArguments()[0].GetProperties();
                                list = GetParamentTypeForLis(arr);
                                return list;
                            }
                            var count = dy.GetProperties();
                            //单值
                            if (count.Length == 0 || dy.FullName == "System.String")
                            {
                                list.Add(GetParamentForType(dy));
                                return list;
                            }
                            //多组类型
                            list = GetParamentTypeForLis(count);
                            return list;
                        }
                        catch (Exception exception)
                        {

                            throw new Exception(exception.ToString());
                        }

                    }
                    list = GetParamentTypeForLis(parameters[0].ParameterType.GetProperties());
                    //是对象循环这个对象
                    return list;
                }
                //不是对象并且为一个参数则循环当前列表
                var p = new ParamentType
                {
                    Name = parameters[0].Name,
                    IsNotNull = parameters[0].ParameterType.Name == "Nullable`1",
                    Description =
                        ((parameters[0].ParameterType.GetCustomAttribute(typeof(ContentDisplayAttribute)) != null)
                            ? ((ContentDisplayAttribute)
                                parameters[0].ParameterType.GetCustomAttribute(typeof(ContentDisplayAttribute)))
                                .DisplayInfo
                            : ""),
                    Type =
                        parameters[0].ParameterType.Name == "Nullable`1"
                            ? parameters[0].ParameterType.FullName.Split(',')[0].Substring(26) + "(NULL)"
                            : parameters[0].ParameterType.Name
                };
                list.Add(p);
                return list;
            }
            //若是多个一级分类的参数
            list.AddRange(parameters.Select(node => new ParamentType
            {
                Name = node.Name,
                IsNotNull = node.ParameterType.Name == "Nullable`1",
                Description = ((node.GetCustomAttribute(typeof(ContentDisplayAttribute)) != null)
                    ? ((ContentDisplayAttribute)node.GetCustomAttribute(typeof(ContentDisplayAttribute))).DisplayInfo : ""),
                Type = node.ParameterType.Name == "Nullable`1"
                    ? node.ParameterType.FullName.Split(',')[0].Substring(26) + "(NULL)"
                    : node.ParameterType.Name
            }));
            return list;
        }

        private static ParamentType GetParamentForType(Type type)
        {
            var p = new ParamentType
            {
                Name = type.Name,
                IsNotNull = type.Name == "Nullable`1",
                Description = ((type.GetCustomAttribute(typeof(ContentDisplayAttribute)) != null)
                    ? ((ContentDisplayAttribute)type.GetCustomAttribute(typeof(ContentDisplayAttribute)))
                        .DisplayInfo
                    : ""),
                Type = type.Name == "Nullable`1" ? type.FullName.Split(',')[0].Substring(26) + "(NULL)" : type.Name
            };
            return p;
        }

        /// <summary>
        /// 打包多个参数
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private static List<ParamentType> GetParamentTypeForLis(IEnumerable<PropertyInfo> parameter)
        {
            return parameter.Select(GetParamentTypeForOnes).ToList();
        }

        /// <summary>
        /// 打包一个参数，返回一个对象
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static ParamentType GetParamentTypeForOnes(PropertyInfo node)
        {
            var p = new ParamentType
            {
                Name = node.Name,
                IsNotNull = node.PropertyType.Name == "Nullable`1",
                Description = ((node.GetCustomAttribute(typeof(ContentDisplayAttribute)) != null)
                    ? ((ContentDisplayAttribute)node.GetCustomAttribute(typeof(ContentDisplayAttribute)))
                        .DisplayInfo
                    : ""),
                Type =
                    node.PropertyType.Name == "Nullable`1"
                        ? node.PropertyType.FullName.Split(',')[0].Substring(26) + "(NULL)"
                        : node.PropertyType.Name
            };
            return p;
        }



        public string BuildCloudHelper(System.Reflection.Assembly assembly, bool isInputData = false)
        {
            var open = Build(assembly);
            return _cloudBuildStrategy.BuildCloudHelper(open);
        }
    }

}