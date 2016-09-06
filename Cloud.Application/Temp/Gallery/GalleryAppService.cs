using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.UI;
using Cloud.Domain;
using Cloud.Framework;
using Cloud.Temp.Gallery.Dtos;

namespace Cloud.Temp.Gallery
{
    public class GalleryAppService : CloudAppServiceBase, IGalleryAppService
    {
        private readonly IGalleryRepositories _galleryRepositories;
        public GalleryAppService(IGalleryRepositories galleryRepositories)
        {
            _galleryRepositories = galleryRepositories;
        }
        public Task Post(PostInput input)
        {
            var model = input.MapTo<Domain.Gallery>();
            return _galleryRepositories.InsertAsync(model);
        }
        public Task Delete(DeletetInput input)
        {
            return _galleryRepositories.DeleteAsync(input.Id);
        }
        public Task Put(PutInput input)
        {
            var oldData = _galleryRepositories.Get(input.Id);
            if (oldData == null)
                throw new UserFriendlyException("该数据为空，不能修改");
            var newData = input.MapTo(oldData);
            return _galleryRepositories.UpdateAsync(newData);
        }
        public Task<GetOutput> Get(GetInput input)
        {
            return Task.Run(() => _galleryRepositories.Get(input.Id).MapTo<GetOutput>());
        }
        public async Task<GetAllOutput> GetAll(GetAllInput input)
        {
            var page = await Task.Run(() => _galleryRepositories.ToPaging("Gallery", input, "*", "Id", new { }));
            return new GetAllOutput() { Items = page.MapTo<IEnumerable<GalleryDto>>() };
        }
    }
}
                    