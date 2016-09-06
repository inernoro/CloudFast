using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Favorites.Dtos;

namespace Cloud.Temp.Favorites
{
    public class FavoritesAppService : CloudAppServiceBase, IFavoritesAppService
    {
        private readonly IFavoritesRepositories _favoritesRepositories;
        public FavoritesAppService(IFavoritesRepositories favoritesRepositories)
        {
            _favoritesRepositories = favoritesRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Favorites>();
            return _favoritesRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _favoritesRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _favoritesRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _favoritesRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _favoritesRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _favoritesRepositories.ToPaging("Favorites", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<FavoritesDto>>() };
        }
    }
}
                    