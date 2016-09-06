using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Favorites.Dtos;
namespace Cloud.Favorites
{
    public class FavoritesAppService : CloudAppServiceBase, IFavoritesAppService
    {
        private readonly IFavoritesRepositories _FavoritesRepositories;
        public FavoritesAppService(IFavoritesRepositories FavoritesRepositories)
        {
            _FavoritesRepositories = FavoritesRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Favorites>();
            return _FavoritesRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _FavoritesRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _FavoritesRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _FavoritesRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _FavoritesRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _FavoritesRepositories.ToPaging("Favorites", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<FavoritesDto>>() };
        }
    }
}
                    