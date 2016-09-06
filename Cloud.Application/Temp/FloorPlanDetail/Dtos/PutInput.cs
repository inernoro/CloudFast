using Abp.AutoMapper;

namespace Cloud.Temp.FloorPlanDetail.Dtos{
[AutoMap(typeof(Domain.FloorPlanDetail))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}