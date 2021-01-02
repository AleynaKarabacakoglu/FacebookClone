using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Concrete
{
   public class Comment
    {

        [Key]
        public int CommentId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        [Required]
        public string Text { get; set; }
        public bool IsDeleted { get; set; }
        public int? ReplyTo { get; set; }
        List<Comment> Comments { get; set; }
    }
}
