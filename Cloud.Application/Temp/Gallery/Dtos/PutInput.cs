using Abp.AutoMapper;
namespace Cloud.Gallery.Dtos{
[AutoMap(typeof(Domain.Gallery))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}