using System.Collections.Generic;
using Cloud.Framework.Strategy;

namespace Cloud.Domain
{
    public interface IManagerUrlStrategy : IStrategy
    {
        string AllInterface { get; }

        string Interface { get; }

        string GetNamespace { get; }

        string LoginUrl { get; } 
        string InitUrl { get; } 
        string TestHost { get; } 

        void Init(Dictionary<string, string> dictionary);
    }
}