using Abp.AutoMapper;

namespace Cloud.Temp.ConstApply.Dtos{
[AutoMap(typeof(Domain.ConstApply))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}