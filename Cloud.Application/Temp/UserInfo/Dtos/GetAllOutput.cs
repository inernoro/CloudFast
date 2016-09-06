using System.Collections.Generic;

namespace Cloud.Temp.UserInfo.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<UserInfoDto> Items { get; set; }

            }
    }