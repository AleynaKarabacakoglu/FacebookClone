using Common.Dtos;
using Core.Services.Interfaces;
using Domain.Context;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.Services.Concrete
{
    public class PostService:IPostService
    {
        private FbContext context;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        public PostService(FbContext fbContext, UserManager<User> _userManager,IUserService _userservice)
        {
            context = fbContext;
            userManager = _userManager;
            userService = _userservice;
        }
        public bool Post(PostDto postdto)
        {
            Post post = new Post();
            post.Text = postdto.Text;
            post.UserId = postdto.UserId;
            post.LikeCount = 0;
            post.IsDeleted = false;
            post.Image= postdto.Image;
            post.LikeCount = 0;
            post.CreatedDate = DateTime.Now;
            context.Posts.Add(post);
            var result = context.SaveChanges();
            if (result > default(int))
                return true;
            return false;
        }
        public bool Comment(CommentDto commentDto)
        {
            Comment comment = new Comment();
            comment.Text = commentDto.Text;
            comment.UserId = commentDto.UserId;
            comment.LikeCount = 0;
            comment.IsDeleted = false;
            comment.LikeCount = 0;
            comment.PostId = commentDto.PostId;
            context.Comments.Add(comment);
            var result = context.SaveChanges();
            if (result > default(int))
                return true;
            return false;
        }
        public List<CommentDto> GetCommentList(int postId)
        {
            List<Comment> comments =  context.Comments.Where(x => x.PostId == postId).ToList();
            List<CommentDto> commentDtos = new List<CommentDto>();
            foreach (var item in comments)
            {
                CommentDto c = new CommentDto();
                c.PostId = item.PostId;
                c.Text = item.Text;
                c.UserId = item.UserId;
                c.UserName = item.User.Name;             
                c.LikeCount = item.LikeCount;
                c.IsDeleted = false;
                c.ProfilPic=item.User.ProfilePhoto;
          
                commentDtos.Add(c);

            }
            return commentDtos;

        }

        public List<PostDto> GetPostList(string name)
        {
            User user = context.Users.Where(x => x.Email == name).FirstOrDefault();           
          
            List<PostDto> listdto = new List<PostDto>();
            List<Post> Friendlist = context.Posts.Where(x => x.UserId == user.Id).ToList();
            
            foreach(var item in Friendlist)
            {
                PostDto post = new PostDto();
                post.Text = item.Text;
                post.PostId = item.PostId;
                post.CreatedDate = item.CreatedDate;
                post.Image = item.Image;
                post.UserName = item.User.Name;
                post.UserSurname = item.User.Surname;
                post.BgPicture = item.User.BackGroundImage;
                post.ProfilPicture = item.User.ProfilePhoto;
                post.LikeCount = item.LikeCount;
                post.CommentCount = item.CommentCount;
                listdto.Add(post);
            }
            listdto = listdto.OrderByDescending(x => x.CreatedDate).ToList();

            return listdto;
        }
        //Anasayfadaki postlar
        public List<PostDto> GetFriendsPostList(string name)
        {
            User user = context.Users.Where(x => x.Email == name).FirstOrDefault();

         
            List<PostDto> listdto = new List<PostDto>();
           
            List<Connection> Friendlist = context.Connections.Where(x => x.UserId == user.Id ).ToList(); //ispending: beklemede
            foreach (var friend in Friendlist)
            {
                foreach (var item in GetListById(friend.FriendId))
                {
                    PostDto post = new PostDto();
                    post.Text = item.Text;
                    post.CreatedDate = item.CreatedDate;
                    post.Image = item.Image;
                    post.LikeCount = item.LikeCount;
                    post.IsDeleted = item.IsDeleted;
                    post.PostId = item.PostId;
                    post.ProfilPicture = item.ProfilPicture;
                    post.BgPicture = item.BgPicture;
                    post.CommentCount = item.CommentCount;
                    post.PostId = item.PostId;
                    post.UserName = item.UserName;
                    post.UserSurname = item.UserSurname;
                    listdto.Add(post);
                }
            }

            List<Post> userspost = context.Posts.Where(x => x.UserId == user.Id).ToList();

            foreach (var item in userspost)
            {
                PostDto post = new PostDto();
                post.Text = item.Text;
                post.PostId = item.PostId;
                post.CreatedDate = item.CreatedDate;
                post.Image = item.Image;
                post.UserName = item.User.Name;
                post.UserSurname = item.User.Surname;
                post.BgPicture = item.User.BackGroundImage;
                post.ProfilPicture = item.User.ProfilePhoto;
                post.LikeCount = item.LikeCount;
                post.CommentCount = item.CommentCount;
                listdto.Add(post);
            }
            listdto = listdto.OrderByDescending(x => x.CreatedDate).ToList();
            return listdto;
        }

        public string ImageUrltoName(string url)
        {
            Uri uri = new Uri(url);
            string filename = Path.GetFileName(uri.LocalPath);
            return filename;
        }
        public List<PostDto> GetListById(int id)
        {
            
            var posts = (from u in context.Users
                         join p in context.Posts
                         on
                          u.Id equals p.UserId
                         select new { u.UserName, u.Id, u.Name, u.ProfilePhoto,
                             u.Surname, p.LikeCount, u.BackGroundImage, p.CommentCount,
                             p.Text, p.CreatedDate,p.CommentTo,p.Comments,p.PostId,p.Image }).OrderByDescending(x => x.CreatedDate);

            var list = posts.Where(x => x.Id == id).ToList();
            List<PostDto> listdto = new List<PostDto>();
            foreach (var item in list)
            {
                PostDto post = new PostDto();
                post.Text = item.Text;
                post.PostId = item.PostId;
                post.CreatedDate = item.CreatedDate;
                post.Image = item.Image;
                post.UserName = item.UserName;
                post.UserSurname = item.Surname;
                post.BgPicture = item.BackGroundImage;
                post.ProfilPicture = item.ProfilePhoto;
                post.LikeCount = item.LikeCount;
                post.CommentCount = item.CommentCount;
                post.UserName = item.Name;
                post.UserSurname = item.Surname;
                listdto.Add(post);
            }
            return listdto;
        }
        public void Like(int userid, int postid)
        {
            Post post = context.Posts.Find(postid);
            LikePost like = context.LikePosts.Where(x => x.UserId == userid && x.PostId == postid).FirstOrDefault();
            if (like == null)
            {

                LikePost like1 = new LikePost();
                like1.UserId = userid;
                like1.PostId = postid;
                post.LikeCount += 1;
                context.LikePosts.Add(like1);

            }
            else
            {
                context.LikePosts.Remove(like);
                post.LikeCount -= 1;
                context.SaveChanges();
            }
            context.SaveChanges();

        }

    }
}
