using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Context
{
    public class FbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {

        public FbContext(DbContextOptions<FbContext> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Connection>().HasKey(c => new { c.UserId, c.FriendId });
            modelBuilder.Entity<LikeComment>().HasKey(c => new { c.CommentId, c.UserId });
            modelBuilder.Entity<LikePost>().HasKey(c => new { c.PostId, c.UserId });
            modelBuilder.Entity<Share>().HasKey(c => new { c.PostId, c.UserId });
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikeComment> LikeComment { get; set; }
        public DbSet<LikePost> LikePosts { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
