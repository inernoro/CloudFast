using System.Collections.Generic;

namespace Cloud.Temp.ProductProtection.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ProductProtectionDto> Items { get; set; }

            }
    }