using Cloud.Framework.Strategy;

namespace Cloud.Framework.Assembly
{
    public interface IEcmaScriptPacker : IStrategy
    {
        string Pack(string script);

    }
}