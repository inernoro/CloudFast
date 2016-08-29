using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper; 
using Cloud.ApiManager.Manager.Dtos;
using Cloud.Domain;

namespace Cloud.ApiManager.Manager
{
    public class ManagerAppService : AbpServiceBase, IManagerAppService
    {
        private readonly IManagerMongoRepositories _managerMongoRepositories;

        public ManagerAppService(IManagerMongoRepositories managerMongoRepositories)
        {
            _managerMongoRepositories = managerMongoRepositories;
        }

        public GetOutput Get(GetInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Post(PostInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Put(PutInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(DeleteInput input)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 获取多条
        /// </summary> 
        /// <returns></returns>
        public async Task<ListResultOutput<GetOutput>> GetBatch()
        {
            return new ListResultOutput<GetOutput>((await _managerMongoRepositories.GetAllListAsync()).MapTo<IReadOnlyList<GetOutput>>());
        }
    }
}