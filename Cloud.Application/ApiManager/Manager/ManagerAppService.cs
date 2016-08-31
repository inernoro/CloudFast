using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.ApiManager.Manager.Dtos;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;
using Cloud.Framework.Redis;
using Newtonsoft.Json;

namespace Cloud.ApiManager.Manager
{
    public class ManagerAppService : AbpServiceBase, IManagerAppService
    {
        private readonly string managerKey = "Cloud:Manager:KeyNamespace";
        private readonly IManagerMongoRepositories _managerMongoRepositories;
        private readonly IRedisHelper _redisHelper;
        private readonly IManagerUrlStrategy _managerUrlStrategy;


        public ManagerAppService(IManagerMongoRepositories managerMongoRepositories, IRedisHelper redisHelper, IManagerUrlStrategy managerUrlStrategy)
        {
            _managerMongoRepositories = managerMongoRepositories;
            _redisHelper = redisHelper;
            _managerUrlStrategy = managerUrlStrategy;
        }

        /// <summary>
        /// 获取多条
        /// </summary> 
        /// <returns></returns>
        public async Task<ListResultOutput<GetOutput>> GetBatch()
        {
            return new ListResultOutput<GetOutput>((await _managerMongoRepositories.GetAllListAsync()).MapTo<IReadOnlyList<GetOutput>>());
        }

        private static ViewDataMongoModel _data;

        private ViewDataMongoModel ViewDataMongoModel
        {
            get
            {
                if (_data != null)
                    return _data;
                var value = _redisHelper.StringGet(managerKey);
                if (value == null)
                { 
                    _data = Network.HttpGet<ViewDataMongoModel>(_managerUrlStrategy.InitUrl).result;
                }
                value = _redisHelper.StringGet(managerKey);
                _data = JsonConvert.DeserializeObject<ViewDataMongoModel>(value);
                return _data;
            }
        }

        public ViewDataMongoModel AllInterface()
        {
            return ViewDataMongoModel;
        }

        public ListResultOutput<OpenDocumentResponse> Interface(string actionName)
        {
            var resultList = ViewDataMongoModel.OpenDocument.Where(x => x.ControllerName == actionName);
            return new ListResultOutput<OpenDocumentResponse>(resultList.ToList());
        }

        /// <summary>
        /// 待优化
        /// </summary>
        /// <returns></returns>
        public List<NamespaceDto> GetNamespace()
        {
            var list = new List<NamespaceDto>();
            var open = ViewDataMongoModel.OpenDocument;
            var areaName = open.Select(x => new { x.ControllerName, x.ControllerDisplay }).Distinct();
            foreach (var node in areaName)
            {
                list.Add(new NamespaceDto
                {
                    Name = node.ControllerName,
                    Display = node.ControllerDisplay,
                    Children = ViewDataMongoModel.OpenDocument.Where(x => x.ControllerName == node.ControllerName).Select(x => new NamespaceDto
                    {
                        Name = x.ActionName,
                        Display = x.ActionDisplay,
                        Url = x.CallUrl
                    }).ToList()
                });
            }
            return list;
        }

        public OpenDocumentResponse GetInfo(string input)
        {
            var data = ViewDataMongoModel.OpenDocument.FirstOrDefault(x => x.CallUrl == input);
            if (data == null)
                throw new UserFriendlyException("抱歉,查询不到此接口");
            return data;
        }
    }
}