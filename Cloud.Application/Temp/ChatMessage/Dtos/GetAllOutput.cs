using System.Collections.Generic;

namespace Cloud.Temp.ChatMessage.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ChatMessageDto> Items { get; set; }

            }
    }