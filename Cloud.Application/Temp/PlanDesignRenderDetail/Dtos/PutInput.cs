using Abp.AutoMapper;
namespace Cloud.PlanDesignRenderDetail.Dtos{
[AutoMap(typeof(Domain.PlanDesignRenderDetail))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}