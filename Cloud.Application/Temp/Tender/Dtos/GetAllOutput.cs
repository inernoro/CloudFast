using System.Collections.Generic;

namespace Cloud.Temp.Tender.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<TenderDto> Items { get; set; }

            }
    }