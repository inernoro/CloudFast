using System.Collections.Generic;
namespace Cloud.ProductProtection.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ProductProtectionDto> Items { get; set; }

            }
    }