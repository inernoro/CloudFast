using Abp.AutoMapper;

namespace Cloud.Temp.UserInfo.Dtos{
[AutoMap(typeof(Domain.UserInfo))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}