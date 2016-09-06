using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Abp.Dependency;
using Abp.UI;
using Castle.Core.Internal;
using Cloud.Framework.Assembly;
using Cloud.Framework.Script;

namespace Cloud.Strategy.Framework
{
    public abstract class StrategyBase<T>
    {
        public virtual T[] Declare { get; }

        public virtual void BoundaryJudgment(int value, string message = null)
        {
            if (value > Declare.Length || value < 0)
            {
                if (message != null)
                    throw new UserFriendlyException(message);
                throw new UserFriendlyException("您指定参数越界了!");
            }
        }

        public virtual T GetName(int id)
        {
            BoundaryJudgment(id);
            return Declare[id];
        }

        public virtual T GetInfo(Predicate<T> match)
        {
            return Declare.FirstOrDefault(node => match(node));
        }

        public virtual T[] GetArray(Predicate<T> match)
        {
            var list = new List<T>();
            return Declare.FindAll(match);
        }



        #region dynamic Lua

        private static ILuaAssembly DynamicSql => IocManager.Instance.Resolve<ILuaAssembly>(); 

        public dynamic Physics
        {
            get
            {
                var basetype = GetType();
                var name = basetype.FullName;
                if (basetype != null) return DynamicSql.NamespaceGetValue(name);
                throw new UserFriendlyException("BaseType Is Null");
            }
        }

        public dynamic Current
        {
            get
            {
                var names = new StackTrace().GetFrame(1).GetMethod().Name;
                var name = Physics[names];
                return name;
            }
        }

        #endregion
    }

    public abstract class StrategyBase : StrategyBase<string>
    {

    }
}