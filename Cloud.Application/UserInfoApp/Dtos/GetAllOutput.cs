using System.Collections.Generic;
namespace Cloud.UserInfoApp.Dtos
{
    public class GetAllOutput
    { 
            public IEnumerable<UserInfoDto> Items { get; set; }

            }
    }