using Abp.AutoMapper;
namespace Cloud.CivilizedConstruction.Dtos{
[AutoMap(typeof(Domain.CivilizedConstruction))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}