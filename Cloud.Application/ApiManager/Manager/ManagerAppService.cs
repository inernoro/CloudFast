using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.ApiManager.Manager.Dtos;
using Cloud.Domain;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;
using Newtonsoft.Json;

namespace Cloud.ApiManager.Manager
{
    public class ManagerAppService : AbpServiceBase, IManagerAppService
    {
        private readonly IManagerMongoRepositories _managerMongoRepositories;
        private readonly IManagerUrlStrategy _managerUrlStrategy;


        public ManagerAppService(
            IManagerMongoRepositories managerMongoRepositories,
            IManagerUrlStrategy managerUrlStrategy)
        {
            _managerMongoRepositories = managerMongoRepositories;
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

        private ViewDataMongoModel ViewDataMongoModel => _data ?? (_data = (Network.DoGet<ViewDataMongoModel>(_managerUrlStrategy.InitUrl)));

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

        public TestOutput Test(TestInput input)
        {
            input.Url = _managerUrlStrategy.TestHost + input.Url;
            var watch = new Stopwatch();
            var output = new TestOutput();
            watch.Start();
            switch (input.Type)
            {
                case HttpReponse.Post:
                    output.Result = Network.DoPost(input.Url, input.Data);
                    break;
                case HttpReponse.Get:
                    output.Result = Network.DoGet(input.Url, input.Data.Deserialize<Dictionary<string, string>>());
                    break;
                default:
                    throw new UserFriendlyException("没有该类型的地址");
            }
            watch.Stop();
            output.ErrorCode = "200";
            output.Take = watch.ElapsedMilliseconds;
            return output;
        }
    }
}