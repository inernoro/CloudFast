using Abp.AutoMapper;

namespace Cloud.Temp.Template.Dtos{
[AutoMap(typeof(Domain.Template))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}