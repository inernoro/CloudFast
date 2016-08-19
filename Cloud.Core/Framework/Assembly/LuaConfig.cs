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

        public string Name => ConfigResult.url;

        public object Data { get; set; }

        public string Type => ConfigResult.url;

        public string DataType => ConfigResult.url;

        public string ContentType => ConfigResult.url;

        public dynamic Excute { get; set; }

        public dynamic Success { get; set; }

        public dynamic Error { get; set; }




    }
}
