using Abp.AutoMapper;

namespace Cloud.Temp.StandardProcedure.Dtos{
[AutoMap(typeof(Domain.StandardProcedure))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}