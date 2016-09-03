using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Cloud.ApiManager.TestManager.Dtos;
using Cloud.Framework.Assembly;

namespace Cloud.ApiManager.TestManager
{
    /// <summary>
    /// 接口测试
    /// </summary>
    public interface ITestManagerAppService : IApplicationService
    {
        [ContentDisplay("根据Id获取最后一次成功的测试案例")]
        TestManagerDto Get(IdInput<string> input);
        
        [ContentDisplay("按分页获取测试案例，带状态")]
        ListResultOutput<TestManagerDto> GetAll(GetAllInput input);
    }
}
