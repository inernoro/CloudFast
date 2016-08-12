namespace Cloud.Framework.Assembly
{
    public class ContentDisplayAttribute : System.Attribute
    {
        public string DisplayInfo { get; set; }
        public bool IsShow { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        /// <param name="info">描述信息</param>
        public ContentDisplayAttribute(string info)
        {
            IsShow = true;
            DisplayInfo = info;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="isShow">是否显示</param>
        public ContentDisplayAttribute(bool isShow)
        {
            IsShow = isShow;
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        /// <param name="info">描述信息</param>
        /// <param name="isShow">是否显示</param>
        /// <param name="login"></param> 
        public ContentDisplayAttribute(string info, bool isShow)
            : this(info)
        {
            IsShow = isShow;
        }


    }


}