using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class UserMessageDto
    {
        public UserDto TargetUser { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
