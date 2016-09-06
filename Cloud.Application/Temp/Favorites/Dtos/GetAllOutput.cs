using System.Collections.Generic;

namespace Cloud.Temp.Favorites.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<FavoritesDto> Items { get; set; }

            }
    }