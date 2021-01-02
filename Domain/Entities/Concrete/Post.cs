using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities.Concrete
{
   public class Post
    {

        [Key]
        public int PostId { get; set; }

        public int UserId { get; set; }
        [Required]
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Post2")]
        public int? CommentTo { get; set; }

        public Post Post2 { get; set; }

        public bool? IsDeleted { get; set; }
        public int? CommentCount { get; set; }
        public int? LikeCount { get; set; }

        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<LikePost> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }

    }
}
