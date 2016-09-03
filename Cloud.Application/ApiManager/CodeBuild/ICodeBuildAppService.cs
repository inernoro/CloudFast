using System.Threading.Tasks;
using Abp.Application.Services;

namespace Cloud.ApiManager.CodeBuild
{
    public interface ICodeBuildAppService : IApplicationService
    {
        void BuildAllCode();

        void BuildCode(string tableName);


    }
}