using Abp.AutoMapper;

namespace Cloud.Temp.Gallery.Dtos{
[AutoMap(typeof(Domain.Gallery))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}