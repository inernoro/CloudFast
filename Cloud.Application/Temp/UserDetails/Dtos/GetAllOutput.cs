using System.Collections.Generic;
namespace Cloud.UserDetails.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<UserDetailsDto> Items { get; set; }

            }
    }