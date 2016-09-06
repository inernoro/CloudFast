using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Gallery.Dtos;
namespace Cloud.Gallery
{
    public class GalleryAppService : CloudAppServiceBase, IGalleryAppService
    {
        private readonly IGalleryRepositories _GalleryRepositories;
        public GalleryAppService(IGalleryRepositories GalleryRepositories)
        {
            _GalleryRepositories = GalleryRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Gallery>();
            return _GalleryRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _GalleryRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _GalleryRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _GalleryRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _GalleryRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _GalleryRepositories.ToPaging("Gallery", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<GalleryDto>>() };
        }
    }
}
                    