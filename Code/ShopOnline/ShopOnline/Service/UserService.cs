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
        public bool ChangePassword(int userId, string oldPass, string newPass)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == userId);
            var result = false;
            if (user != null)
            {
                if (user.Password == oldPass)
                {
                    user.Password = newPass;
                    result = Context.SaveChanges() > 0;
                }
            }
            return result;
        }
        public bool UpdateOrderStatus(int userId, int orderId, int status)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == userId);
            var result = false;
            if (user != null)
            {
                var orderDb = user.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (orderDb != null)
                {
                    orderDb.OrderStatus = status;
                    result = Context.SaveChanges() > 0;
                }
            }
            return result;
        }
        public bool UpdateOrderInfo(int userId, int orderId, string address, int? cityId, int? districtId,string phone)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == userId);
            var result = false;
            if (user != null)
            {
                var orderDb = user.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (orderDb != null)
                {
                    orderDb.ShippingAddress = address;
                    orderDb.LocationCityId = cityId;
                    orderDb.LocationDistrictId = districtId;
                    orderDb.ShippingTelephone = phone;
                    result = Context.SaveChanges() > 0;
                }
            }
            return result;
        }
        public Order GetOrderByUserIdAndOrderId(int userId, int orderId)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == userId);
            var order = new Order();
            if (user != null)
            {
                var orderDb = user.Orders.FirstOrDefault(x => x.OrderId == orderId);
                if (orderDb != null)
                {
                    order = orderDb;
                }
            }
            return order;
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            var user = Context.Users.FirstOrDefault(x => x.UserId == userId);
            var orders = new List<Order>();
            if (user != null)
            {
                orders = user.Orders.OrderByDescending(x => x.UpdatedDate).ThenByDescending(x => x.CreatedDate).ToList();
            }
            return orders;
        }

        public int CreateUser(string username, string password, string signUpRealName, string signUpEmail, string signUpPhone, string signUpStreet, int? locationCityId, int? locationDistrictId)
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
        public User CheckUser(string username, string password)
        {
            var name = username.Trim().ToLower();
            var pass = password.Trim().ToLower();
            var user = new User();
            var userDb = Context.Users.FirstOrDefault(x => x.UserName == name && x.Password == pass && x.Role == Common.RoleUser);
            if (userDb != null)
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
            if (userDb != null)
            {
                user = userDb;
            }
            return user;
        }
    }
}