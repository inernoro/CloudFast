using System.Collections.Generic;
namespace Cloud.FloorPlanDetail.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<FloorPlanDetailDto> Items { get; set; }

            }
    }