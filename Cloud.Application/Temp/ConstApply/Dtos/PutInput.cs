using Abp.AutoMapper;
namespace Cloud.ConstApply.Dtos{
[AutoMap(typeof(Domain.ConstApply))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}