using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Interfaces
{
    public interface IMessageService
    {
        bool AddMessage(int OwnerId, int SentTo, string message, string photo);

        UserMessageDto GetMessages(string CurrentUser, string username);
    }
}
