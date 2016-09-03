using Abp.AutoMapper;

namespace Cloud.Template.Dtos
{
    [AutoMap(typeof(Domain.Template))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}