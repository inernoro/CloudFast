using Abp.AutoMapper;
namespace Cloud.FloorPlanDetail.Dtos{
[AutoMap(typeof(Domain.FloorPlanDetail))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}