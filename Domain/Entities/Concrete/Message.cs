
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Concrete
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserPhoto { get; set; }
        [ForeignKey("OwnerUser")]
        public int OwnerUserId { get; set; }
        public User OwnerUser { get; set; }
        public int SentTo { get; set; }
    }
}
