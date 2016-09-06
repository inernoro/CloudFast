using Abp.AutoMapper;

namespace Cloud.Temp.PlanDesignRenderDetail.Dtos{
[AutoMap(typeof(Domain.PlanDesignRenderDetail))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}