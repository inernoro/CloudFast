using System.Collections.Generic;

namespace Cloud.Temp.ShopAudit.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ShopAuditDto> Items { get; set; }

            }
    }