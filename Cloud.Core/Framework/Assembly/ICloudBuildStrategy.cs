using System.Collections.Generic;
using Cloud.Framework.Mongo;
using Cloud.Framework.Strategy;

namespace Cloud.Framework.Assembly
{
    public interface ICloudBuildStrategy : IStrategy
    {
        string BuildCloudHelper(IEnumerable<OpenDocumentResponse> input, bool isInputData = false);
    }
}