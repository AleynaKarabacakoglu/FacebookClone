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
    public class ConnectionService : IConnectionService
    {
        private FbContext context;

        public ConnectionService(FbContext fbContext)
        {
            context = fbContext;

        }

        public List<ConnectionDto> getFriendList(int userid)
        {
            List<int> Idlist1 = new List<int>();
            List<int> Idlist2 = new List<int>();
            List<int> Idlist3 = new List<int>();
            List<User> users = new List<User>();
            List<ConnectionDto> myfriends = new List<ConnectionDto>();
            //context.Connections.Where(x => x.UserId == userid && x.IsPending == false && x.isDeleted == false); 
            List<Connection> FriendList1 = context.Connections.Where(x => x.UserId == userid).ToList();
            List<Connection> FriendList2 = context.Connections.Where(x => x.FriendId == userid).ToList();
            foreach (var item in FriendList1)
            {
                Idlist1.Add(item.FriendId);
            }
            foreach (var item in FriendList2)
            {
                Idlist2.Add(item.UserId);
            }
            foreach (var item in Idlist1)
            {
                foreach (var item2 in Idlist2)
                {
                    if (item == item2)
                    {
                        Idlist3.Add(item);
                    }
                }
            }
            foreach (var item in Idlist3)
            {
                users = context.Users.Where(x => x.Id == item).ToList();
            }
            foreach (var item in users)
            {
                ConnectionDto connectionDto = new ConnectionDto();
                connectionDto.FriendId = item.Id;
                connectionDto.FriendImage = item.ProfilePhoto;
                connectionDto.FriendName = item.Name;
                connectionDto.FriendSurname = item.Surname;
                myfriends.Add(connectionDto);
            }
            return myfriends;
        }
    }
}
