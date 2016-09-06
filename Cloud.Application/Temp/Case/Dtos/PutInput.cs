using Abp.AutoMapper;
namespace Cloud.Case.Dtos{
[AutoMap(typeof(Domain.Case))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}