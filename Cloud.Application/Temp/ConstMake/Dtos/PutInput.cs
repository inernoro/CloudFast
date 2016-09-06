using Abp.AutoMapper;

namespace Cloud.Temp.ConstMake.Dtos{
[AutoMap(typeof(Domain.ConstMake))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}