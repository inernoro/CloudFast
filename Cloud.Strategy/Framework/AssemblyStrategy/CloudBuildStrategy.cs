using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cloud.Framework.Assembly;
using Cloud.Framework.Mongo;
using Newtonsoft.Json;

namespace Cloud.Strategy.Framework.AssemblyStrategy
{
    public class CloudBuildStrategy : StrategyBase<string>, ICloudBuildStrategy
    {
        /// <summary>
        /// 自动JS控制器
        /// </summary>
        /// <param name="openDocumentResponses"></param>
        /// <param name="isInputData">是否序列化参数输出</param>
        public string BuildCloudHelper(IEnumerable<OpenDocumentResponse> openDocumentResponses, bool isInputData = false)
        {
            var sb = new StringBuilder();
            sb.Append("var cloud = {};");
            sb.Append("\r\ncloud.send = apiHelper.ajax;");
            sb.Append("\r\napiHelper.urlPath = \"\";\r\n");
            var document = openDocumentResponses.ToList();
            var controlNamelist = document.GroupBy(x => x.ControllerName).ToList();
            var value = controlNamelist.ToDictionary(model => model.Key, model => document.FindAll(x => x.ControllerName == model.Key).ToArray());

            foreach (var node in value)
            {
                sb.Append("\r\ncloud." + node.Key + " = {}");

                foreach (var responsese in node.Value)
                {
                    if (isInputData)
                    {
                        sb.Append("\r\n\r\n//" + responsese.ActionDisplay + "");
                        sb.Append("\r\n//参数\r\n/*");
                        sb.Append(
                            JsonConvert.SerializeObject(
                                responsese.InputParantrens.Select(x => new { x.Name, x.Type, x.Description }),
                                Formatting.Indented));
                        sb.Append("*/\r\n");
                    }
                    sb.Append("\r\n cloud." + node.Key + "." + responsese.ActionName + " = function ( " + GetParament(responsese) + "  callback , errorCallback ){");

                    GetBody(responsese, ref sb, false);
                    sb.Append("\r\n cloud." + node.Key + "." + responsese.ActionName + "Ep = function ( " + GetParament(responsese, true) + "  callback , errorCallback ){");

                    GetBody(responsese, ref sb, true);
                }
            }
            return sb.ToString();
        }

        public void GetBody(OpenDocumentResponse responsese, ref StringBuilder sb, bool isExistence)
        {
            sb.Append("\r\n\t cloud.send({");
            if (!isExistence)
            {
                sb.Append("\r\n\t\t data : parameter,");
            }
            else
            {
                sb.Append("\r\n\t\t data : ");
                sb.Append("{" + responsese.InputParantrens.Aggregate(new StringBuilder(), (x, y) => x.Append(y.Name + " : " + y.Name + ",")).ToString().TrimEnd(',') + "},");
            }
            sb.Append("\r\n\t\t type : \"" + responsese.Reponse + "\",");
            sb.Append("\r\n\t\t url : \"" + responsese.CallUrl + "\",");
            sb.Append("\r\n\t\t success : callback,");
            sb.Append("\r\n\t\t error : errorCallback,");
            sb.Append("\r\n\t  }) ");
            sb.Append("\r\n };");
        }

        public string GetParament(OpenDocumentResponse responsese, bool isEp = false)
        {
            var data = !isEp ? "parameter," : responsese.InputParantrens.Aggregate(new StringBuilder(), (x, y) => x.Append(y.Name + " ,")).ToString();
            return data;
        }


        public override string[] Declare { get; }
    }
}