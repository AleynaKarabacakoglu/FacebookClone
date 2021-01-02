using Common.Dtos;
using Core.Services.Interfaces;
using Domain.Context;
using Domain.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete
{
    public class UserService:IUserService
    {
        private FbContext context;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public UserService(FbContext fbContext, UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            context = fbContext;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public async Task<IdentityResult> CreateUser(UserDto userDto)
        {
            User user = new User();
            user.Email = userDto.Email;
            user.UserName = userDto.Email;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.CreatedDate = DateTime.Now;
            user.PasswordHash = userDto.Password;
            user.IsActive = false;
            IdentityResult result = await userManager.CreateAsync(user, user.PasswordHash);
            //bool check= result.Succeeded;
            
            //return check;
            if(result.Succeeded)
            {
                return null;
            }
            return result;

        }
        public async Task<bool> SignIn(UserDto user)
        {
            var identityResult = await signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
            var identityResultsuccess = identityResult.Succeeded;

            return identityResultsuccess;
        }
        public UserDto GetUserByName(string name)
        {
            UserDto user = context.Users.Where(x => x.Email == name).Select(x => new UserDto
            {
                Id=x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                Password = x.PasswordHash,
                BgImage=x.BackGroundImage,
                ProfileImage=x.ProfilePhoto
                
            }).FirstOrDefault();

            return user;
        }
        public UserDto GetUserByFriendName(string name)
        {
            UserDto user = context.Users.Where(x => x.Name == name).Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                Password = x.PasswordHash

            }).FirstOrDefault();
            

            return user;
        }
        public bool IsFollowButton(string name,string identity)
        {
            UserDto friend = GetUserByName(name);
            UserDto user = GetUserByName(identity);

            bool connection = context.Connections.Any(x => x.UserId == user.Id && x.FriendId == friend.Id);//Follow butonu gözüksün
            if (connection)
            {
                return true;
            }
            return false;
        }
        public void LoadImage(string email,string path)
        {
            User user = context.Users.Where(x => x.Email == email).FirstOrDefault();
            user.ProfilePhoto = path;
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void AddFriend(string email, string userMail)
        {
            UserDto friend = GetUserByName(email);
            UserDto user = GetUserByName(userMail);
          
            var connection = context.Connections.FirstOrDefault(x => x.UserId == user.Id && x.FriendId== friend.Id && x.IsPending == false && x.isDeleted==true);
            if (connection != null)
            {
                connection.IsPending = true;
                connection.isDeleted = false;
                context.Connections.Update(connection);
            }
            else
            {
                Connection newconnection = new Connection();
                newconnection.isDeleted = true;
                newconnection.IsPending = false;
                newconnection.UserId = user.Id;
                newconnection.FriendId = friend.Id;
                context.Connections.Add(newconnection);                            
            }
            User me = context.Users.Find(user.Id);
            if (me != null)
            {
                me.friendsCount += 1;
                context.Users.Update(me);
            }
            User frnd = context.Users.Find(friend.Id);
            if (frnd != null)
            {
                frnd.friendsCount += 1;
                context.Users.Update(frnd);
            }

            context.SaveChanges();
        }

        public void RemoveFriend(string email, string userMail)
        {
            UserDto friend = GetUserByName(email);
            UserDto user = GetUserByName(userMail);

            var connection = context.Connections.FirstOrDefault(x => x.UserId == user.Id && x.FriendId == friend.Id && x.IsPending == true && x.isDeleted == false);
            if (connection != null)
            {
                connection.IsPending = false;
                connection.isDeleted = true;
                context.Connections.Update(connection);
            }
            else
            {
                Connection newconnection = new Connection();
                newconnection.isDeleted = false;
                newconnection.IsPending = true;
                newconnection.UserId = user.Id;
                newconnection.FriendId = friend.Id;
                context.Connections.Remove(newconnection);
            }
            User me = context.Users.Find(user.Id);
            if (me != null)
            {
                me.friendsCount -= 1;
                context.Users.Update(me);
            }
            User frnd = context.Users.Find(friend.Id);
            if (frnd != null)
            {
                frnd.friendsCount -= 1;
                context.Users.Update(frnd);
            }

            context.SaveChanges();
        }





    }
}
