using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ShopOnline.Constants;
using ShopOnline.Extension;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class CartController : BaseFrontController
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly UserService _userService;
        private readonly LocationService _locationService;
        public CartController()
        {
            _productService = new ProductService();
            _orderService = new OrderService();
            _userService = new UserService();
            _locationService = new LocationService();
        }
        private CartViewModel GetCart()
        {
            var cartModel = new CartViewModel();
            cartModel.OrderDetails = new List<OrderDetailViewModel>();
            var objCart = Session[Common.UserCartKey];
            if (objCart != null)
            {
                var cart = objCart as CartViewModel;
                if (cart != null)
                {
                    cartModel = cart;
                }
            }
            return cartModel;
        }
        private void SaveCart(CartViewModel cart)
        {
            Session[Common.UserCartKey] = cart;
        }

        public ActionResult CountCart()
        {
            var count = string.Empty;
            var obj = Session[Common.UserCartKey];
            if (obj != null)
            {
                var cart = obj as CartViewModel;
                if (cart != null)
                {
                    count = string.Format("({0})", cart.OrderDetails.Select(x => x.Quantity).Sum());
                }

            }
            return Content(count);
        }

        public ActionResult AddToCart(int productId)
        {
            var product = _productService.GetProductById(productId);
            var productViewModel = AutoMapper.Mapper.Map<ProductViewModel>(product);
            var result = false;
            var currentCount = 0;
            if (product.ProductId > 0)
            {
                var obj = Session[Common.UserCartKey];

                if (obj != null)
                {
                    var cart = obj as CartViewModel;
                    if (cart != null)
                    {
                        var orderDetail = cart.OrderDetails.FirstOrDefault(x => x.Product.ProductId == productViewModel.ProductId);
                        if (orderDetail != null)
                        {
                            orderDetail.Quantity += 1;


                        }
                        else
                        {
                            orderDetail = new OrderDetailViewModel();
                            orderDetail.Product = productViewModel;
                            orderDetail.Quantity = 1;
                            cart.OrderDetails.Add(orderDetail);
                        }
                        currentCount = cart.OrderDetails.Select(x => x.Quantity).Sum();
                        Session[Common.UserCartKey] = cart;
                    }
                }
                else
                {
                    var cart = new CartViewModel();
                    cart.OrderDetails = new List<OrderDetailViewModel>();
                    var orderDetail = new OrderDetailViewModel();
                    orderDetail.Quantity = 1;
                    orderDetail.Product = productViewModel;
                    cart.OrderDetails.Add(orderDetail);
                    currentCount = cart.OrderDetails.Select(x => x.Quantity).Sum();
                    Session[Common.UserCartKey] = cart;
                }
                result = true;
            }
            return Json(new { result, currentCount });
        }
        public ActionResult RenderOrderSummary()
        {
            var cart = GetCart();
            return View(cart);
        }

        public ActionResult CheckOutCart()
        {
            var cartModel = GetCart();
            return View(cartModel);
        }
        public ActionResult CheckOutStep1()
        {
            //get current user info
            var user = _userService.GetUser(UserId ?? 0);
            var cart = GetCart();
            if (string.IsNullOrEmpty(cart.Name))
            {
                cart.Name = user.RealName;
            }
            if (string.IsNullOrEmpty(cart.Telephone))
            {
                cart.Telephone = user.Telephone;
            }
            if (string.IsNullOrEmpty(cart.Street))
            {
                cart.Street = user.Address;
            }
            if (string.IsNullOrEmpty(cart.Email))
            {
                cart.Email = user.Email;
            }
            var userCityId = user.LocationCityId;
            var userDistrictId = user.LocationDistrictId;
            var cities = _locationService.GetCities().Select(x => new SelectListItem() { Selected = false, Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
            int? cityFilterValue = cart.CityId.HasValue
                                      ? cart.CityId
                                      : (userCityId.HasValue ? userCityId : null);
            foreach (var selectListItem in cities)
            {
                if (selectListItem.Value == cityFilterValue.ToString())
                {
                    selectListItem.Selected = true;
                }
            }
            int? districtFilterValue = cart.DistrictId.HasValue
                                           ? cart.DistrictId
                                           : (userDistrictId.HasValue ? userDistrictId : null);
            var districts = _locationService.GetDistricts(cityFilterValue ?? 0).Select(x => new SelectListItem() { Selected = false, Value = x.LocationId.ToString(), Text = x.LocationName }).ToList(); 
            foreach (var selectListItem in districts)
            {
                if (selectListItem.Value == districtFilterValue.ToString())
                {
                    selectListItem.Selected = true;
                }
            }
            cart.CitySource = cities;
            cart.DistrictSource = districts;

            return View(cart);
        }
        public ActionResult CheckOutStep2(string name, string telephone, string street, string email, int? city, int? district)
        {
            var cart = GetCart();
            cart.Name = name;
            cart.Telephone = telephone;
            cart.Street = street;
            cart.Email = email;
            cart.CityId = city;
            cart.DistrictId = district;
            SaveCart(cart);
            return View();
        }
        public ActionResult CheckOutStep3(DeliveryMethods delivery)
        {
            var cart = GetCart();
            cart.Delivery = delivery;
            SaveCart(cart);
            return View();
        }
        public ActionResult CheckOutStep4(PaymentMethods payment)
        {
            var cartModel = GetCart();
            cartModel.Payment = payment;
            SaveCart(cartModel);
            return View(cartModel);
        }
        public ActionResult CheckOutComplete()
        {
            var cart = GetCart();
            var userId = UserId;
            var isOrder = _orderService.CreateOrder(userId, cart);
            Session.Remove(Common.UserCartKey);
            return View();
        }
        public ActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var result = false;
            decimal? totalLine = 0;
            decimal? totalAll = 0;
            var detail = cart.OrderDetails.FirstOrDefault(x => x.Product.ProductId == productId);
            if (detail != null)
            {
                result = true;
                detail.Quantity = quantity;
                totalLine = detail.TotalPrice;
                totalAll = cart.Total;
            }
            SaveCart(cart);
            return Json(new { result, totalLine = totalLine.ToSystemFormat(), totalAll = totalAll.ToSystemFormat(), itemsCount = cart.ItemsCount });
        }
        public ActionResult RemoveLineCart(int productId)
        {
            var cart = GetCart();
            var result = false;
            decimal? totalAll = 0;
            var detail = cart.OrderDetails.FirstOrDefault(x => x.Product.ProductId == productId);
            if (detail != null)
            {
                result = true;
                cart.OrderDetails.Remove(detail);
                totalAll = cart.Total;
            }
            SaveCart(cart);
            return Json(new { result, totalAll = totalAll.ToSystemFormat(), itemsCount = cart.ItemsCount });
        }
    }
}
