using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos;
using Core.Services.Interfaces;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Facebook.Controllers
{
    public class AccountController : Controller
    {
        public UserManager<User> userManager { get; set; }
        private readonly ILogger<AccountController> _logger;
        private SignInManager<User> _signinManager;
        private readonly IUserService _userService;
        public AccountController(UserManager<User> _userManager, ILogger<AccountController> logger,SignInManager<User> signInManager, IUserService userService)
        {
            this.userManager = _userManager;
            _logger = logger;
            _signinManager = signInManager;
            _userService = userService;

        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserDto userDto)
        {
            Task<IdentityResult> errors = _userService.CreateUser(userDto);
            if (await errors == null)
            {
                
                return View();
            }
            else
            {
                foreach (var  error in errors.Result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View(userDto);
                }
            }
            return View();
        }
        public async Task<IActionResult> SignIn(UserDto userdto)
        {
            Task<bool> identityResultsuccess = _userService.SignIn(userdto);
            if (await identityResultsuccess)
            {
                return Json(true);
            }

            ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Hatalı");
            return Json(false);


        }
    }
}
