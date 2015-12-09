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
        public int CreateUser(string username, string password, string signUpRealName,string signUpEmail, string signUpPhone, string signUpStreet,int? locationCityId,int? locationDistrictId)
        {
            var user = new User();
            user.UserName = username;
            user.Password = password;
            user.Email = signUpEmail;
            user.RealName = signUpRealName;
            user.Telephone = signUpPhone;
            user.Address = signUpStreet;
            user.LocationCityId = locationCityId;
            user.LocationDistrictId = locationDistrictId;
            user.Role = Common.RoleUser;
            user.CreatedDate = DateTime.Now;
            Context.Users.Add(user);
            Context.SaveChanges();
            return user.UserId;
        }
        public User CheckUser(string username,string password)
        {
            var name = username.Trim().ToLower();
            var pass = password.Trim().ToLower();
            var user = new User();
            var userDb = Context.Users.FirstOrDefault(x => x.UserName == name && x.Password == pass&&x.Role==Common.RoleUser);
            if(userDb!=null)
            {
                user = userDb;
            }
            return user;
        }
        public User CheckAdmin(string username, string password)
        {
            var name = username.Trim().ToLower();
            var pass = password.Trim().ToLower();
            var user = new User();
            var userDb = Context.Users.FirstOrDefault(x => x.UserName == name && x.Password == pass && x.Role == Common.RoleAdmin);
            if (userDb != null)
            {
                user = userDb;
            }
            return user;
        }
        public User GetUser(int userId)
        {
            var user = new User();
            var userDb = Context.Users.FirstOrDefault(x => x.UserId == userId);
            if(userDb!=null)
            {
                user = userDb;
            }
            return user;
        }
    }
}