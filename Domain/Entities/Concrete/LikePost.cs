using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Concrete
{
   public  class LikePost
    {

        public int PostId { get; set; }

        public int UserId { get; set; }

        public Post Post { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
