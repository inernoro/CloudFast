using Abp.AutoMapper;
namespace Cloud.UserDetails.Dtos{
[AutoMap(typeof(Domain.UserDetails))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}