using Abp.AutoMapper;
namespace Cloud.ConstMake.Dtos{
[AutoMap(typeof(Domain.ConstMake))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}