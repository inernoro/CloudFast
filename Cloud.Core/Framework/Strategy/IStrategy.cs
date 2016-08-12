using System;
using Abp.Dependency;

namespace Cloud.Framework.Strategy
{
    public interface IStrategy : IStrategy<string>
    {

    }

    public interface IStrategy<out T> : ISingletonDependency
    {
        T GetName(int id);

        T GetInfo(Predicate<T> match);

        T[] GetArray(Predicate<T> match);
    }
}