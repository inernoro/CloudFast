using System.Collections.Generic;
namespace Cloud.Favorites.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<FavoritesDto> Items { get; set; }

            }
    }