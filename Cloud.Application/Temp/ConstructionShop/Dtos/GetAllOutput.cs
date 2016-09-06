using System.Collections.Generic;

namespace Cloud.Temp.ConstructionShop.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ConstructionShopDto> Items { get; set; }

            }
    }