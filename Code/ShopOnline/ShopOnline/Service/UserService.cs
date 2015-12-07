using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Constants;

namespace ShopOnline.Service
{
    public class UserService : BaseService
    {
        public int CreateUser(string username,string password)
        {
            var user = new User();
            user.UserName = username;
            user.Password = password;
            user.Role = Common.RoleForUser;
            Context.Users.Add(user);
            Context.SaveChanges();
            return user.UserId;
        }
        public User CheckUser(string username,string password)
        {
            var name = username.Trim().ToLower();
            var pass = password.Trim().ToLower();
            var user = new User();
            var userDb = Context.Users.FirstOrDefault(x => x.UserName == name && x.Password == pass);
            if(userDb!=null)
            {
                user = userDb;
            }
            return user;
        }
    }
}