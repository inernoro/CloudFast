namespace Cloud.Framework.Cache.Redis
{
    public struct KeyValueStruct
    {
        public string Name;

        public string Value;

        public KeyValueStruct(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}