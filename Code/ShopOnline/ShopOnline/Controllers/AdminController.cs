using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.App_Data;
using ShopOnline.Constants;
using ShopOnline.Models;
using ShopOnline.Service;
using ShopOnline.Extension;
namespace ShopOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly CategoryService _categoryService = null;
        private readonly ProductService _productService = null;
        private readonly ConfigService _configService = null;
        private readonly OrderService _orderService = null;
        private readonly UserService _userService = null;
        public AdminController()
        {
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _configService = new ConfigService();
            _orderService = new OrderService();
            _userService = new UserService();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var result = false;
            if ((actionName.ToLower() != "index" && actionName.ToLower() != "login") && controllerName.ToLower() == "admin")
            {
                var userObj = Session[Common.AdminIdKey];
                if (userObj != null)
                {
                    var uId = 0;
                    int.TryParse(userObj.ToString(), out uId);
                    if (uId > 0)
                    {
                        result = true;
                    }
                }
                if (!result)
                {
                    filterContext.Result = RedirectToAction("Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(Common.AdminIdKey);
            return RedirectToAction("Index");
        }

        public ActionResult Login(string adminLoginName, string adminLoginPassword)
        {
            var login = _userService.CheckAdmin(adminLoginName, adminLoginPassword);
            if (login.UserId > 0)
            {
                Session[Common.AdminIdKey] = login.UserId;
                return RedirectToAction("ProductList", "Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        #region Categories

        public ActionResult CategoriesParent()
        {
            return View();
        }
        public ActionResult CategoriesSub()
        {
            var availableParents = _categoryService.GetParentCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName });
            return View(availableParents);
        }
        public ActionResult GetParentCategories()
        {
            var categories = _categoryService.GetParentCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName });
            return Json(new { data = categories }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSubCategories()
        {
            var categories = _categoryService.GetSubCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ParentCategoryId = x.ParentCategoryId, ParentCategoryName = x.Parent.CategoryName });
            return Json(new { data = categories }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateParentCategory(string categoryName)
        {
            var result = _categoryService.CreateParentCategory(categoryName);
            return Json(new { result });
        }
        public ActionResult EditParentCategory(int id, string categoryName)
        {
            var result = _categoryService.UpdateParentCategory(id, categoryName);
            return Json(new { result });
        }
        public ActionResult DeleteParentCategory(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return Json(new { result });
        }

        public ActionResult CreateSubCategory(int parentId, string categoryName)
        {
            var result = _categoryService.CreateSubCategory(parentId, categoryName);
            return Json(new { result });
        }
        public ActionResult EditSubCategory(int id, int parentId, string categoryName)
        {
            var result = _categoryService.UpdateSubCategory(id, parentId, categoryName);
            return Json(new { result });
        }
        public ActionResult DeleteSubCategory(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return Json(new { result });
        }
        #endregion

        #region Product
        public ActionResult ProductList()
        {
            var categories = _categoryService.GetSubCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ParentCategoryId = x.ParentCategoryId, ParentCategoryName = x.Parent.CategoryName });
            return View(categories);
        }
        public ActionResult GetProductList()
        {
            var products = _productService.GetAllProduct();
            var productsViewModel = AutoMapper.Mapper.Map<List<ProductViewModel>>(products);
            foreach (var productViewModel in productsViewModel)
            {
                var productImages = productViewModel.ProductImages;
                foreach (var productImage in productImages)
                {
                    productImage.ProductImageUrl = Url.Content(productImage.ProductImageUrl);
                }
            }
            return Json(new { data = productsViewModel }, JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult CreateProduct(IEnumerable<HttpPostedFileBase> files, string categoryIds, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.CreateProduct(ids, files, code, name, isNew, price, shortDescription, detailDescription);
            return Json(new { result });
        }
        [ValidateInput(false)]
        public ActionResult UpdateProduct(IEnumerable<HttpPostedFileBase> files, int id, string categoryIds, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.UpdateProduct(files, id, ids, code, name, isNew, price, shortDescription, detailDescription);
            return Json(new { result });
        }
        public ActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            return Json(new { result });
        }
        public ActionResult DeleteProductImage(int productImageId)
        {
            var result = _productService.DeleteProductImage(productImageId);
            return Json(new { result });
        }

        #endregion

        #region Config
        public ActionResult ConfigLogo()
        {
            var config = _configService.GetConfig(Common.Logo);
            return View(config);
        }
        public ActionResult ChangeLogo(HttpPostedFileBase file)
        {
            var result = _configService.ChangeLogo(file);
            return Json(new { result });
        }
        public ActionResult ConfigContact()
        {
            var contact = _configService.GetConfig(Common.Contact);
            var address = _configService.GetConfig(Common.Address);
            var phone = _configService.GetConfig(Common.Phone);
            var email = _configService.GetConfig(Common.Email);
            var intro = _configService.GetConfig(Common.Intro);
            var employee1 = _configService.GetConfig(Common.Employee1);
            var employee2 = _configService.GetConfig(Common.Employee2);
            var employee3 = _configService.GetConfig(Common.Employee3);
            return View(new List<Config>() { contact, address, phone, email, intro, employee1, employee2, employee3 });
        }
        [ValidateInput(false)]
        public ActionResult SaveConfigContact(string value)
        {
            var result = _configService.SaveConfig(Common.Contact, value);
            return Json(new { result });
        }
        [ValidateInput(false)]
        public ActionResult SaveConfigAddress(string value)
        {
            var result = _configService.SaveConfig(Common.Address, value);
            return Json(new { result });
        }
        public ActionResult SaveConfigPhone(string value)
        {
            var result = _configService.SaveConfig(Common.Phone, value);
            return Json(new { result });
        }
        public ActionResult SaveConfigIntro(string value)
        {
            var result = _configService.SaveConfig(Common.Intro, value);
            return Json(new { result });
        }
        public ActionResult SaveConfigEmail(string value)
        {
            var result = _configService.SaveConfig(Common.Email, value);
            return Json(new { result });
        }
        public ActionResult SaveConfigEmployee(string employee1, string employee2, string employee3)
        {
            var result1 = _configService.SaveConfig(Common.Employee1, employee1);
            var result2 = _configService.SaveConfig(Common.Employee2, employee2);
            var result3 = _configService.SaveConfig(Common.Employee3, employee3);
            return Json(new { result = result1 && result2 && result3 });
        }

        #endregion

        #region Orders
        public ActionResult OrdersNewIndex()
        {
            return View();
        }
        public ActionResult OrdersProcessingIndex()
        {
            return View();
        }
        public ActionResult OrdersProcessedIndex()
        {
            return View();
        }
        public ActionResult OrdersDisabledIndex()
        {
            return View();
        }
        public ActionResult OrdersNew()
        {
            var orders = _orderService.GetOrdersNew();
            var ordersModel = AutoMapper.Mapper.Map<List<OrderViewModel>>(orders);
            return Json(new { data = ordersModel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrdersProcessing()
        {
            var orders = _orderService.GetOrdersProcessing();
            var ordersModel = AutoMapper.Mapper.Map<List<OrderViewModel>>(orders);
            return Json(new { data = ordersModel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrdersProcessed()
        {
            var orders = _orderService.GetOrdersProcessed();
            var ordersModel = AutoMapper.Mapper.Map<List<OrderViewModel>>(orders);
            return Json(new { data = ordersModel }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrdersDisabled()
        {
            var orders = _orderService.GetOrdersDisabled();
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
        public ActionResult OrderToProcessing(int orderId)
        {
            var result = _orderService.UpdateOrderStatus(orderId, Common.OrderStatusProcessing);
            return Json(new { result });
        }
        public ActionResult OrderToProcessed(int orderId)
        {
            var result = _orderService.UpdateOrderStatus(orderId, Common.OrderStatusProcessed);
            return Json(new { result });
        }
        public ActionResult OrderToDisabled(int orderId)
        {
            var result = _orderService.UpdateOrderStatus(orderId, Common.OrderStatusDisabled);
            return Json(new { result });
        }
        public ActionResult OrderToNew(int orderId)
        {
            var result = _orderService.UpdateOrderStatus(orderId, Common.OrderStatusNew);
            return Json(new { result });
        }
        #endregion
    }
}
