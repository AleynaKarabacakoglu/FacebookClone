using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dtos
{
    public class PostDto
    {
       
        public int PostId { get; set; }

        public int UserId { get; set; }
        
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
      
        public int? CommentTo { get; set; }

        public bool? IsDeleted { get; set; }
        public int? CommentCount { get; set; }
        public int? LikeCount { get; set; }
        public UserDto User { get; set; }
        public string ProfilPicture { get; set; }
        public string BgPicture { get; set; }
        public string UserName { get; set; }
        public string  UserSurname { get; set; }
        public bool LikeButton { get; set; }


    }
}
