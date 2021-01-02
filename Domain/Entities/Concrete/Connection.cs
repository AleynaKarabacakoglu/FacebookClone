using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Concrete
{
   public class Connection
    {

        [Key]
        [ForeignKey("User")]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [Key]
        [ForeignKey("Friend")]
        [Column(Order = 2)]
        public int FriendId { get; set; }

        public User Friend { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsPending { get; set; }//true:istek atılmış bekliyor false:arkadaş oldular
        public bool isDeleted { get; set; }//true:bağlantı yok bağlantıyı kopart// false:default
    }
}
