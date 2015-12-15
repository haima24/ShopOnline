using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.App_Data;
using ShopOnline.Constants;
using ShopOnline.Extension;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class ContactController : BaseFrontController
    {
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        private readonly ConfigService _configService;
        private readonly OrderService _orderService;
        public ContactController()
        {
            _userService = new UserService();
            _locationService = new LocationService();
            _configService = new ConfigService();
            _orderService=new OrderService();
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
            var dictrictsModel = AutoMapper.Mapper.Map<List<LocationViewModel>>(dictricts);
            return Json(new { result = true, dictrictsModel });
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
        public ActionResult ChangePassword(string oldPass,string newPass)
        {
            var result = _userService.ChangePassword(this.UserId??0,oldPass,newPass);
            return Json(new {result});
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult OrderHistoryList()
        {
            var orders = _userService.GetOrdersByUserId(UserId ?? 0);
            var ordersModel = AutoMapper.Mapper.Map<List<OrderViewModel>>(orders);
            return Json(new { data = ordersModel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewOrderDetails(int orderId)
        {
            var order = _orderService.GetOrder(orderId);
            var orderViewModel = AutoMapper.Mapper.Map<OrderViewModel>(order);
            var html = this.RenderPartialViewToString("ViewOrderDetails", orderViewModel);
            return Json(new { result = true, html });
        }
        public ActionResult ViewEditOrderInfo(int orderId)
        {
            var order = _userService.GetOrderByUserIdAndOrderId(UserId??0,orderId);
            var orderViewModel = AutoMapper.Mapper.Map<OrderViewModel>(order);
            int? cityFilterValue = orderViewModel.LocationCityId;
            var cities = _locationService.GetCities().Select(x => new SelectListItem() { Selected = false, Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
            foreach (var selectListItem in cities)
            {
                if (selectListItem.Value == cityFilterValue.ToString())
                {
                    selectListItem.Selected = true;
                }
            }
            orderViewModel.CitySource = cities;
            int? districtFilterValue = orderViewModel.LocationDistrictId;
            var districts = _locationService.GetDistricts(cityFilterValue ?? 0).Select(x => new SelectListItem() { Selected = false, Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
            foreach (var selectListItem in districts)
            {
                if (selectListItem.Value == districtFilterValue.ToString())
                {
                    selectListItem.Selected = true;
                }
            }
            orderViewModel.DistrictSource = districts;
            var html = this.RenderPartialViewToString("ViewEditOrderInfo", orderViewModel);
            return Json(new { result = true, html });
        }
        public ActionResult OrderToDisabled(int orderId)
        {
            var result = _userService.UpdateOrderStatus(UserId??0,orderId, Common.OrderStatusDisabled);
            return Json(new { result });
        }
        public ActionResult SaveEditOrderInfo(int orderId, string address, int? cityId, int? districtId, string phone)
        {
            var result = _userService.UpdateOrderInfo(UserId ?? 0, orderId, address,cityId,districtId,phone);
            return Json(new { result });
        }
    }
}
