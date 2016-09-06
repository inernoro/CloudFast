using System.Collections.Generic;
namespace Cloud.ChatMessage.Dtos
{
    public class GetAllOutput
    {
            public IEnumerable<ChatMessageDto> Items { get; set; }

            }
    }