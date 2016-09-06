using Abp.AutoMapper;
namespace Cloud.ConstContract.Dtos{
[AutoMap(typeof(Domain.ConstContract))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}