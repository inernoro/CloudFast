using Abp.AutoMapper;

namespace Cloud.Temp.Favorites.Dtos{
[AutoMap(typeof(Domain.Favorites))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}