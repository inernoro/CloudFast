using System.Collections.Generic;
using System.Linq;
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

        public Dictionary<string, string> BuilDictionary(string tableName)
        {
            var str = Current();
            string sql = str.tables.ToString();
            var tableObject = _dapperRepositorie.Query<BuildTable>(sql, new { name = tableName });
            return _buildCodeExcuteStrategy.SigleDictionary(tableObject);
        }

        public void BuildCode(string tableName)
        {
            var str = Current();
            string sql = str.tables.ToString();
            var tableObject = _dapperRepositorie.Query<BuildTable>(sql, new { name = tableName });
            var newObj = tableObject.Where(x => x.Name == tableName).ToList();
            _buildCodeExcuteStrategy.ExcuteCode(newObj);
        }
    }
}