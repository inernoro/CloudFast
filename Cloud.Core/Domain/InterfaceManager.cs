using System.Collections.Generic;
using Abp.Domain.Entities;

namespace Cloud.Domain
{
    public class InterfaceManager : Entity<string>
    {  

        public IEnumerable<TestManager> TestManager { get; set; }

    }
}