using Abp.AutoMapper;
namespace Cloud.Favorites.Dtos{
[AutoMap(typeof(Domain.Favorites))]
    public class PutInput
    {
        public int Id { get; set; }
    }
}