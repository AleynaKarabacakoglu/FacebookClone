using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Concrete
{
    public class LikeComment
    {

        public int CommentId { get; set; }

        public int UserId { get; set; }

        public Comment Comment { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}
