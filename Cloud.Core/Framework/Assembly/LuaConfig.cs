using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;

namespace Cloud.Framework.Assembly
{
    public class LuaConfig : ITransientDependency
    {
        public dynamic ConfigResult { get; }

        public LuaConfig(dynamic configResult)
        {
            ConfigResult = configResult;
        }

        public string Url => ConfigResult.url;

        public string Name => ConfigResult.name;

        public object Data => ConfigResult.data;

        public string Type => ConfigResult.type;

        public string DataType => ConfigResult.dataType;

        public string ContentType => ConfigResult.contentType;

        public dynamic Excute => ConfigResult.excute;

        public dynamic Success => ConfigResult.success;

        public dynamic Error => ConfigResult.error;


    }
}
