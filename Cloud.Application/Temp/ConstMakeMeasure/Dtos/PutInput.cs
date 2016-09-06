using Abp.AutoMapper;
namespace Cloud.ConstMakeMeasure.Dtos{
[AutoMap(typeof(Domain.ConstMakeMeasure))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}