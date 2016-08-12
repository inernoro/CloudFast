namespace Cloud.Framework.Mongo
{
    public class DbNameAttribute : System.Attribute
    {
        public string Name { get; set; }
        public bool Show { get; set; }
        public DisplayState State { get; set; }
        public DbNameAttribute(string vUserinfo)
        {
            Show = true;
            Name = vUserinfo;
        }
        /// <summary>
        /// 设置显示或隐藏
        /// </summary>
        /// <param name="show">状态</param>
        public DbNameAttribute(bool show)
        {
            Show = show;
        }
        /// <summary>
        /// 设置显示状态
        /// </summary>
        /// <param name="show">全部显示或隐藏</param>
        /// <param name="state">此处例外</param>
        public DbNameAttribute(bool show, DisplayState state)
            : this(show)
        {
            State = state;
        }
    }
    public enum DisplayState
    {
        Insert = 1,
        Update = 2,
        Select = 3
    }
}