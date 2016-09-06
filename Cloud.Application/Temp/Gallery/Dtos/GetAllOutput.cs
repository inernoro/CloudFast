using System.Collections.Generic;

namespace Cloud.Temp.Gallery.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<GalleryDto> Items { get; set; }

            }
    }