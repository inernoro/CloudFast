using System.Collections.Generic;
namespace Cloud.ShopAudit.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ShopAuditDto> Items { get; set; }

            }
    }