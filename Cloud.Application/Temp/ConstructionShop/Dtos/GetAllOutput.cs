using System.Collections.Generic;
namespace Cloud.ConstructionShop.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ConstructionShopDto> Items { get; set; }

            }
    }