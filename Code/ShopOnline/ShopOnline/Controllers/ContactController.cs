using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.App_Data;
using ShopOnline.Constants;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ContactController : BaseFrontController
    {
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        private readonly ConfigService _configService;
        public ContactController()
        {
            _userService = new UserService();
            _locationService = new LocationService();
            _configService = new ConfigService();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "ProductList");
        }
        public ActionResult SignUpPage()
        {
            var locations = _locationService.GetCities();
            return View(locations);
        }
        public ActionResult SignUp(string signUpUserName, string signUpPassWord, string signUpRealName, string signUpEmail, string signUpPhone, string signUpStreet, int? signUpCity, int? signUpDistrict)
        {

            var user = _userService.CreateUser(signUpUserName, signUpPassWord, signUpRealName, signUpEmail, signUpPhone, signUpStreet, signUpCity, signUpDistrict);

            return RedirectToAction("Login", new { signInUsername = signUpUserName, signInPassword = signUpPassWord });
        }

        public ActionResult Login(string signInUsername, string signInPassword)
        {
            var login = _userService.CheckUser(signInUsername, signInPassword);
            if (login.UserId > 0)
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
        public ActionResult LoginAjax(string signInUserName, string signInPassword)
        {
            var result = false;
            var login = _userService.CheckUser(signInUserName, signInPassword);
            if (login.UserId > 0)
            {
                Session[Common.UserIdKey] = login.UserId;
                Session[Common.UserNameKey] = login.UserName;
                result = true;
            }
            else
            {
                result = false;
            }
            return Json(new { result });
        }

        public ActionResult GetDistricts(int locationId)
        {
            var dictricts = _locationService.GetDistricts(locationId);
            return Json(new { result = true, dictricts });
        }
        public ActionResult RenderUserPanel()
        {
            ViewBag.IsUserLoggedIn = IsUserLoggedIn;
            return PartialView();
        }
        public ActionResult Contact()
        {
            var contact = _configService.GetConfig(Common.Contact);
            return View(contact);
        }
        public ActionResult Employees()
        {
            var employee1 = _configService.GetConfig(Common.Employee1);
            var employee2 = _configService.GetConfig(Common.Employee2);
            var employee3 = _configService.GetConfig(Common.Employee3);
            return PartialView(new List<Config>() {employee1, employee2, employee3});
        }
    }
}
