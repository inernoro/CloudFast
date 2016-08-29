using System.Collections.Generic;
using Cloud.Framework.Strategy;

namespace Cloud.Domain
{
    public interface IManagerUrlStrategy : IStrategy
    {
        string AllInterface { get; }

        string Interface { get; }

        string GetNamespace { get; }

        void Init(Dictionary<string, string> dictionary);
    }
}