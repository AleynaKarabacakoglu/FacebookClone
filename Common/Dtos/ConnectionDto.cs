using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class ConnectionDto
    {
        public int UserId { get; set; }
        
        public int FriendId { get; set; }
        public string FriendName { get; set; }
        public string FriendSurname { get; set; }
        public string FriendImage { get; set; }

    }
}
