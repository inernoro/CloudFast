using Abp.AutoMapper;
namespace Cloud.StandardProcedure.Dtos{
[AutoMap(typeof(Domain.StandardProcedure))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}