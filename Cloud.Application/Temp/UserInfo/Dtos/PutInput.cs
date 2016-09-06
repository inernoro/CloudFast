using Abp.AutoMapper;
namespace Cloud.UserInfo.Dtos{
[AutoMap(typeof(Domain.UserInfo))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}