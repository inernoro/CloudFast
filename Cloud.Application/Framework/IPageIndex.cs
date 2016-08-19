namespace Cloud.Application
{
    public interface IPageIndex
    {
        int CurrentIndex { get; set; }

        int PageSize { get; set; }

    }
}