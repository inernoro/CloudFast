using System.Collections.Generic;
using System.Linq;
using Abp;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.ApiManager.TestManager.Dtos;
using Cloud.Domain;

namespace Cloud.ApiManager.TestManager
{
    public class TestManagerAppService : AbpServiceBase, ITestManagerAppService
    {
        private readonly IManagerMongoRepositories _managerMongoRepositories;

        public TestManagerAppService(IManagerMongoRepositories managerMongoRepositories)
        {
            _managerMongoRepositories = managerMongoRepositories;
        }

        public TestManagerDto Get(IdInput<string> input)
        {
            var exists = _managerMongoRepositories.Queryable().FirstOrDefault(x => x.Id == input.Id);
            if (exists == null)
                throw new UserFriendlyException("没有对该接口进行测试");
            var data = exists.TestManager.OrderByDescending(x => x.CreateTime)
                  .FirstOrDefault(x => true);
            if (data == null)
                throw new UserFriendlyException("没有生成成功的案例！");
            return data.MapTo<TestManagerDto>();
        }


        public ListResultOutput<TestManagerDto> GetAll(GetAllInput input)
        {
            var exists = _managerMongoRepositories.Queryable().FirstOrDefault(x => x.Id == input.Id);
            if (exists == null)
                throw new UserFriendlyException("没有对该接口进行测试");
            var data = exists.TestManager.OrderByDescending(x => x.CreateTime)
                  .Where(x => input.State).Skip(input.CurrentIndex * input.PageSize).Take(input.PageSize);
            if (data == null)
                throw new UserFriendlyException("没有生成成功的案例！");
            return new ListResultOutput<TestManagerDto>(data.MapTo<IReadOnlyList<TestManagerDto>>());
        }


    }
}