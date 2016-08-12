using System.Threading.Tasks;
using Cloud.Framework.Strategy;

namespace Cloud.Framework.Assembly
{
    public interface INetWorkStrategy : IStrategy
    {
        Task Send(string key, object value);

    }
}