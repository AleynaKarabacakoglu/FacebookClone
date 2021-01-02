using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Facebook.Controllers
{
    public class ProfileController : Controller
    {
      
        private readonly ILogger<ProfileController> logger;

        private readonly IUserService userService;
        private readonly IPostService postService;
        public ProfileController(IUserService _userService, IPostService _postService, ILogger<ProfileController> _logger)
        {
            userService = _userService;
            postService = _postService;
            logger = _logger;

        }
       
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult Get()
        {
            string email = null;
            string key = HttpContext.Session.GetString("friendMail");
            if (key == null) {  email = User.Identity.Name; }
            else { email = key; }
            List<PostDto> posts = postService.GetPostList(email);

            return PartialView("ProfilePostPartialView", posts);
        }
        public IActionResult GetImage()
        {
            string email;
            string key = HttpContext.Session.GetString("friendMail");
            if (key == null) { email = User.Identity.Name; }
            else { email = key; }
            UserDto userDto = userService.GetUserByName(email);
            userDto.button = userService.IsFollowButton(email, User.Identity.Name);
            return PartialView("PicturePartialView",userDto);
        }
        public IActionResult ProfilePost(PostDto post)
        {
            UserDto user = userService.GetUserByName(User.Identity.Name);
            post.UserId = user.Id;
            string email = User.Identity.Name;
            string img = null;
            if (post.Image == null)
            {
                //img = _postService.ImageUrl(post.Image);
                img = HttpContext.Session.GetString("path");
                post.Image = img;
                
            }
            postService.Post(post);

            List<PostDto> posts = postService.GetPostList(email);
            HttpContext.Session.Clear();
            return PartialView("ProfilePostPartialView", posts);
           
        }
        public IActionResult GetFriendProfile(UserDto user)
        {
            UserDto userdto = userService.GetUserByFriendName(user.Name);
            string email = userdto.Email;
            HttpContext.Session.SetString("friendMail", email);

            return Json(true);

        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);//uzantı

                //unique bir path oluşturuyor
                string imageName = Guid.NewGuid() + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/img/{imageName}");
                //HttpContext.Session.SetString("profilepath", imageName);
                using var stream = new FileStream(path, FileMode.Create);
                userService.LoadImage(User.Identity.Name,imageName);
                await file.CopyToAsync(stream);
            }
            //return Json(true);
            return RedirectToAction("Profile", "Profile");
            //return PartialView(file);
        }

        public ActionResult AddFriend(string email)
        {

            userService.AddFriend(email, User.Identity.Name);
            
            return Json(true);
        }
        public ActionResult RemoveFriend(string email)
        {

            userService.RemoveFriend(email, User.Identity.Name);

            return Json(true);
        }

    }
}
