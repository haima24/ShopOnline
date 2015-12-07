using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Constants;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ContactController : BaseFrontController
    {
        private readonly UserService _userService;
        public ContactController()
        {
            _userService=new UserService();
        }


        public ActionResult SignUpPage()
        {
            return View();
        }
        public ActionResult SignUp(string signUpUserName, string signUpPassWord)
        {
            var user = _userService.CreateUser(signUpUserName, signUpPassWord);

            return RedirectToAction("Login", new { signInUsername = signUpUserName, signInPassword = signUpPassWord });
        }

        public ActionResult Login(string signInUsername,string signInPassword)
        {
            var login = _userService.CheckUser(signInUsername, signInPassword);
            if(login.UserId>0)
            {
                Session[Common.UserIdKey] = login.UserId;
                Session[Common.UserNameKey] = login.UserName;
                return RedirectToAction("Index", "ProductList");
            }
            else
            {
                return RedirectToAction("SignUpPage");
            }
        }
    }
}
