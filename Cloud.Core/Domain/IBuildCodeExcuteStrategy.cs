using System.Collections.Generic;
using Cloud.Framework.Strategy;

namespace Cloud.Domain
{
    public interface IBuildCodeExcuteStrategy : IStrategy
    {
        /// <summary>
        /// 执行生成文件操作
        /// </summary> 
        void ExcuteBuild(IEnumerable<BuildTable> buildTables);

        void ExcuteCode();

    }
}