using Abp.AutoMapper;

namespace Cloud.Temp.ConstAcceptance.Dtos{
[AutoMap(typeof(Domain.ConstAcceptance))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}