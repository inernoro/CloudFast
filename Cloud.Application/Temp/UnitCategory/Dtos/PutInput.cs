using Abp.AutoMapper;

namespace Cloud.Temp.UnitCategory.Dtos{
[AutoMap(typeof(Domain.UnitCategory))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}