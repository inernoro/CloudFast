using System.Collections.Generic;

namespace Cloud.Temp.Shop.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ShopDto> Items { get; set; }

            }
    }