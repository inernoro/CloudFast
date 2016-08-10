using System.Web.Optimization;

namespace Cloud.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear(); 
        }
    }
}
