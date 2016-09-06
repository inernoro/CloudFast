using Abp.AutoMapper;

namespace Cloud.Temp.ConstContract.Dtos{
[AutoMap(typeof(Domain.ConstContract))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}