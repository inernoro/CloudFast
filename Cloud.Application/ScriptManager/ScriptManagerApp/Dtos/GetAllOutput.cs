using System.Collections.Generic;

namespace Cloud.ScriptManager.ScriptManagerApp.Dtos
{
    public class GetAllOutput
    { 
            public IEnumerable<ScriptManagerDto> Items { get; set; }

            }
    }