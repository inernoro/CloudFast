using System.Collections.Generic;
namespace Cloud.Tender.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<TenderDto> Items { get; set; }

            }
    }