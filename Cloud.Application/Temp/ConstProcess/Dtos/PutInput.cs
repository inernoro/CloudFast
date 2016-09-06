using Abp.AutoMapper;
namespace Cloud.ConstProcess.Dtos{
[AutoMap(typeof(Domain.ConstProcess))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}