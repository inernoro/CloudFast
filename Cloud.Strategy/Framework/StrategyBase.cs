using System;
using System.Collections.Generic;
using System.Linq;
using Abp.UI;
using Castle.Core.Internal;

namespace Cloud.Strategy.Framework
{
    public abstract class StrategyBase<T> 
    {
        public abstract T[] Declare { get; }

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

    }
}