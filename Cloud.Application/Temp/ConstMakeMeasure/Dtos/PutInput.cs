using Abp.AutoMapper;

namespace Cloud.Temp.ConstMakeMeasure.Dtos{
[AutoMap(typeof(Domain.ConstMakeMeasure))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}