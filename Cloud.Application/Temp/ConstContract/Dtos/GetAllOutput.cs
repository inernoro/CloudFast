using System.Collections.Generic;

namespace Cloud.Temp.ConstContract.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ConstContractDto> Items { get; set; }

            }
    }