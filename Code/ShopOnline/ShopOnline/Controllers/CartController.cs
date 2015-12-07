using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Constants;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService;
        public CartController()
        {
            _productService = new ProductService();
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
                    count = string.Format("({0})", cart.OrderDetails.Select(x=>x.Quantity).Sum());
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
            var cart = new CartViewModel();
            var obj = Session[Common.UserCartKey];
            if(obj!=null)
            {
                var cartObj = obj as CartViewModel;
                if(cartObj!=null)
                {
                    cart = cartObj;
                }
            }
            return View(cart);
        }

        public ActionResult CheckOutCart()
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
            return View(cartModel);
        }
        public ActionResult CheckOutStep1()
        {
            return View();
        }
        public ActionResult CheckOutStep2()
        {
            return View();
        }
        public ActionResult CheckOutStep3()
        {
            return View();
        }
        public ActionResult CheckOutStep4()
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
            return View(cartModel);
            return View();
        }
    }
}
