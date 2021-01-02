using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Interfaces
{
    public interface IConnectionService
    {
        List<ConnectionDto> getFriendList(int userid);
    }
}
