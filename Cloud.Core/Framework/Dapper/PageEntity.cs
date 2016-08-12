using System.Collections.Generic;

namespace Cloud.Framework.Dapper
{
    public class PageEntity<T>
    {
        public List<T> EntityList { get; set; }

        public int Count { get; set; }


    }
}