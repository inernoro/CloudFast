using System.Collections.Generic;
namespace Cloud.Gallery.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<GalleryDto> Items { get; set; }

            }
    }