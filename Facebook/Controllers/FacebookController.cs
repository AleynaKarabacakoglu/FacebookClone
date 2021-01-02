using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos;
using Core.Services.Interfaces;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Facebook.Controllers
{
    public class FacebookController : Controller
    {
        public UserManager<User> userManager { get; set; }
        private readonly ILogger<FacebookController> logger;        
        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly IConnectionService connectionService;
        private readonly IMessageService messageService;
        public FacebookController(ILogger<FacebookController> _logger, IUserService _userService,IPostService _postService,IConnectionService _connectionService, IMessageService _messageService)
        {
            logger = _logger;
            userService = _userService;
            postService = _postService;
            connectionService = _connectionService;
            messageService = _messageService;
        }
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Home()
        {
//;            ConnectionDto dto = new ConnectionDto();
//            List<ConnectionDto> connection = connectionService.getFriendList(5);
            return View();
        }
        public IActionResult GetPosts()
        {
            string email = User.Identity.Name;
            List<PostDto> posts = postService.GetFriendsPostList(email);

            return PartialView("PostPartialView", posts);
        }
        public IActionResult Post(PostDto post)
        {
            UserDto user = userService.GetUserByName(User.Identity.Name);
            post.UserId = user.Id;
            string email = User.Identity.Name;
            string image = null;
           
            
                image=HttpContext.Session.GetString("path");
                post.Image = image;
            
           
            postService.Post(post);
            List<PostDto> posts = postService.GetPostList(email);
            HttpContext.Session.Clear();
            return PartialView("PostPartialView", posts);
        }
        //public IActionResult Comment(CommentDto commentDto)
        //{
        //    commentDto.PostId = (int)HttpContext.Session.GetInt32("PostId");
        //    commentDto.UserId =userService.GetUserByName(User.Identity.Name).Id;
        //    postService.Comment(commentDto);




        //}
        public IActionResult ShowChat(string username)
        {
            string CurrentUser = User.Identity.Name;
            UserMessageDto UserMessages = messageService.GetMessages(CurrentUser, username);
            return PartialView("Chat", UserMessages);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);//uzantı

                string imageName = Guid.NewGuid() + imageExtension;//unique filename

                HttpContext.Session.SetString("path",imageName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/{imageName}");
                
                using var stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);
            }

            return RedirectToAction("Home","Facebook");
        }
        public IActionResult Like(PostDto post)
        {
            UserDto user = userService.GetUserByName(User.Identity.Name);
            HttpContext.Session.SetString("chatUser", user.Name);
            postService.Like(user.Id, post.PostId);
            return Json(true);
        }

       

    }
}
