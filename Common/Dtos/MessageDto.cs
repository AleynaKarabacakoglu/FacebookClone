using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string OwnerUserName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserPhoto { get; set; }
        public int OwnerUserId { get; set; }
        public int SentTo { get; set; }
    }
}
