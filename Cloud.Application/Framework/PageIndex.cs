using System.ComponentModel.DataAnnotations;
using Cloud.Framework.Assembly;

namespace Cloud.Framework
{
    public class PageIndex : IPageIndex
    {
        [Range(1, 1000)]
        [ContentDisplay("当前多少页（默认1）")]
        public int CurrentIndex { get; set; } = 1;

        [Range(1, 500)]
        [ContentDisplay("当前多少条（默认10）")]
        public int PageSize { get; set; } = 10;
    }
}