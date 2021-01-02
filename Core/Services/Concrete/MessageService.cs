using Common.Dtos;
using Core.Services.Interfaces;
using Domain.Context;
using Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services.Concrete
{  
        public class MessageService : IMessageService
        {
            private readonly FbContext _context;
            public MessageService(FbContext context)
            {
                _context = context;
            }

            public bool AddMessage(int OwnerId, int SentTo, string message, string photo)
            {
                Message msg = new Message();

                msg.CreatedDate = DateTime.Now;
                msg.OwnerUserId = OwnerId;
                msg.SentTo = SentTo;
                msg.Text = message;
                msg.UserPhoto = photo;

                _context.Messages.Add(msg);

                int result = _context.SaveChanges();
                if (result > 0)
                {
                    return true;
                }

                return false;
            }

            public UserMessageDto GetMessages(string currentUser, string username)
            {
                UserMessageDto userMessageDto = new UserMessageDto();
                List<MessageDto> messageDtoList = new List<MessageDto>();
                User CurrentUser = _context.Users.Where(u => u.UserName == currentUser).FirstOrDefault();
                User TargetUser = _context.Users.Where(u => u.UserName == username).FirstOrDefault();

                List<Message> messages = _context.Messages.Where(m => (m.OwnerUserId == CurrentUser.Id && m.SentTo == TargetUser.Id) || (m.OwnerUserId == TargetUser.Id && m.SentTo == CurrentUser.Id)).ToList();

                foreach (var item in messages)
                {
                    MessageDto messageDto = new MessageDto();
                    User user = new User();
                    user = _context.Users.Where(u => u.Id == item.OwnerUserId).FirstOrDefault();

                    messageDto.CreatedDate = item.CreatedDate;
                    messageDto.Id = item.Id;
                    messageDto.OwnerUserId = item.OwnerUserId;
                    messageDto.SentTo = item.SentTo;
                    messageDto.Text = item.Text;
                    messageDto.UserPhoto = user.ProfilePhoto;
                    messageDto.OwnerUserName = user.UserName;

                    messageDtoList.Add(messageDto);
                }

                UserDto TargetUserDto = new UserDto();

                TargetUserDto.Email = TargetUser.Email;
                TargetUserDto.Id = TargetUser.Id;
                TargetUserDto.Name = TargetUser.Name;
                TargetUserDto.Surname = TargetUser.Surname;
                TargetUserDto.ProfileImage = TargetUser.ProfilePhoto;
                TargetUserDto.UserName = TargetUser.UserName;

                userMessageDto.TargetUser = TargetUserDto;
                userMessageDto.Messages = messageDtoList;

                return userMessageDto;
            }
        }
    }

