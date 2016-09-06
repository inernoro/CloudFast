using System.Collections.Generic;
using System.Threading.Tasks;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Framework.Dapper;

namespace Cloud.ApiManager.CodeBuild
{
    public class CodeBuildAppService : CloudAppServiceBase, ICodeBuildAppService
    {
        private readonly IDapperRepositories _dapperRepositorie;
        private readonly IBuildCodeExcuteStrategy _buildCodeExcuteStrategy;

        public CodeBuildAppService(
            IDapperRepositories dapperRepositorie,
            IBuildCodeExcuteStrategy buildCodeExcuteStrategy
            )
        {
            _dapperRepositorie = dapperRepositorie;
            _buildCodeExcuteStrategy = buildCodeExcuteStrategy;
        }

        public void BuildAllCode()
        {
            var sql = Current();
            var tableName = sql.tables.ToString();
            var tableObject = _dapperRepositorie.Query<BuildTable>(tableName);
            _buildCodeExcuteStrategy.ExcuteCode();

        }

        public void BuildCode(string tableName)
        { 
        }
    }
}