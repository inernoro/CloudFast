using Abp.Web.Mvc.Views;

namespace Cloud.Web.Views
{
    public abstract class CloudWebViewPageBase : CloudWebViewPageBase<dynamic>
    {

    }

    public abstract class CloudWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CloudWebViewPageBase()
        {
            LocalizationSourceName = CloudConsts.LocalizationSourceName;
        }
    }
}