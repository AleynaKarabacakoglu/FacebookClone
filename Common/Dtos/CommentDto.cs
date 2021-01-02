using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public bool? IsDeleted { get; set; }
        public int? LikeCount { get; set; }
        public UserDto User { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public bool LikeButton { get; set; }
        public string ProfilPic { get; set; }
    }
}
