using System.Collections.Generic;

namespace Cloud.Temp.FloorPlanDetail.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<FloorPlanDetailDto> Items { get; set; }

            }
    }