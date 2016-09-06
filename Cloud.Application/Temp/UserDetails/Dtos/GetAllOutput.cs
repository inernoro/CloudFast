using System.Collections.Generic;

namespace Cloud.Temp.UserDetails.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<UserDetailsDto> Items { get; set; }

            }
    }