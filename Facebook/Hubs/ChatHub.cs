using Common.Dtos;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IUserService userService;
        private static ConcurrentDictionary<string, UserDto> dict = new ConcurrentDictionary<string, UserDto>();
        public ChatHub(IPostService postService, IMessageService messageService, IUserService _userService)
        {

            _messageService = messageService;
            userService = _userService;
        }
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);      
        //}
        public async Task SendMessage(string message, string username)
        {
            UserDto user = userService.GetUserByName(username);
            bool result = _messageService.AddMessage(Int32.Parse(Context.UserIdentifier), user.Id, message, user.ProfileImage);

            await Clients.User(user.Id.ToString()).SendAsync("ReceiveMessage", message, Context.User.Identity.Name, user.ProfileImage);
            //await Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {

            UserDto user = userService.GetUserByName(Context.User.Identity.Name);
            dict.TryAdd(Context.User.Identity.Name, user);


            //await Clients.Others.SendAsync("ConnectMessage", user.UserName, user.Name + " " + user.LastName, user.ProfilePhoto);
            await Clients.Caller.SendAsync("SetUserName", Context.User.Identity.Name);
            await Clients.All.SendAsync("GetOnlines", dict, Context.User.Identity.Name);
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            UserDto user = userService.GetUserByName(Context.User.Identity.Name);

            dict.TryRemove(Context.User.Identity.Name, out user);

            //await Clients.Others.SendAsync("DisConnectUser", user.UserName);
            await Clients.All.SendAsync("GetOnlines", dict, Context.User.Identity.Name);
        }


        public async Task SendReqAlert(string username)
        {

            //await Clients.All.SendAsync("ConnectMessage", Context.User.Identity.Name, Context.UserIdentifier);
            await Clients.User("8").SendAsync("ReqAlert", Context.User.Identity.Name);
        }


    }
}
