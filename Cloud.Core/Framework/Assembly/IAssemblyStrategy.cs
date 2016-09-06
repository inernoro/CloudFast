using System.Collections.Generic;
using Cloud.Framework.Mongo;
using Cloud.Framework.Strategy;

namespace Cloud.Framework.Assembly
{
    public interface IAssemblyStrategy : IStrategy
    {
        IEnumerable<OpenDocumentResponse> Build(System.Reflection.Assembly assembly);

        string BuildCloudHelper(System.Reflection.Assembly assembly, bool isInputData = false);
    }
}