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
        private int AdminId { get; set; }
        private readonly CategoryService _categoryService = null;
        private readonly ProductService _productService = null;
        private readonly ConfigService _configService = null;
        private readonly OrderService _orderService = null;
        private readonly UserService _userService = null;
        private readonly ColorService _colorService = null;
        private readonly BrandService _brandService = null;
        public AdminController()
        {
            _colorService = new ColorService();
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _configService = new ConfigService();
            _orderService = new OrderService();
            _userService = new UserService();
            _brandService = new BrandService();
        }
        private bool CheckAuthentication()
        {
            var result = false;
            if (Request.Cookies.AllKeys.Contains(Common.UserCookieRemember))
            {
                var cookie = Request.Cookies[Common.UserCookieRemember];
                if (cookie != null)
                {
                    var adminId = cookie.Value;
                    var id = 0;
                    if (int.TryParse(adminId, out id))
                    {
                        Session[Common.AdminIdKey] = id;
                        AdminId = id;
                    }

                }

            }
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
            return result;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var result = false;
            if ((actionName.ToLower() != "index" && actionName.ToLower() != "login") && controllerName.ToLower() == "admin")
            {
                result = CheckAuthentication();
                if (!result)
                {
                    filterContext.Result = RedirectToAction("Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            if (CheckAuthentication())
            {
                return RedirectToAction("ProductList", "Admin");
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(Common.AdminIdKey);
            if (Request.Cookies.AllKeys.Contains(Common.UserCookieRemember))
            {
                var cookie = Request.Cookies[Common.UserCookieRemember];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult Login(string adminLoginName, string adminLoginPassword, bool rememberme)
        {
            var login = _userService.CheckAdmin(adminLoginName, adminLoginPassword);
            if (login.UserId > 0)
            {
                Session[Common.AdminIdKey] = login.UserId;
                //add to cookie for remember
                var cookie = new HttpCookie(Common.UserCookieRemember);
                cookie.Expires = DateTime.Now.AddDays(Common.CookieRememberDays);
                cookie.Value = login.UserId.ToString();
                Response.Cookies.Add(cookie);
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
            var result = _categoryService.CreateParentCategory(AdminId, categoryName);
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
            var result = _categoryService.CreateSubCategory(AdminId, parentId, categoryName);
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
            var productInfo = new ProductInfoViewModel();
            productInfo.Categories=_categoryService.GetSubCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName, ParentCategoryId = x.ParentCategoryId, ParentCategoryName = x.Parent.CategoryName });
            var brands = _brandService.GetBrands();
            productInfo.Brands = AutoMapper.Mapper.Map<List<ProductBrandViewModel>>(brands);
            var colors = _colorService.GetColors();
            productInfo.Colors = AutoMapper.Mapper.Map<List<ColorViewModel>>(colors);
            return View(productInfo);
        }
        public ActionResult GetProductList()
        {
            var products = _productService.GetAllProduct();
            var productsViewModel = AutoMapper.Mapper.Map<List<ProductViewModel>>(products);
            foreach (var productViewModel in productsViewModel)
            {
                productViewModel.ProductImageDisplay.ProductImageUrl = Url.Content(productViewModel.ProductImageDisplay.ProductImageUrl);
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
        public ActionResult CreateProduct(IEnumerable<HttpPostedFileBase> files, string categoryIds, int? brandId, string colorIds, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var colorIdValues = new List<int>();
            if(!string.IsNullOrEmpty(colorIds))
            {
                colorIdValues = colorIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.CreateProduct(ids, brandId, colorIdValues, files, code, name, isNew, price, shortDescription, detailDescription);
            return Json(new { result });
        }
        [ValidateInput(false)]
        public ActionResult UpdateProduct(IEnumerable<HttpPostedFileBase> files, int id, string categoryIds, int? brandId, string colorIds, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var colorIdValues = new List<int>();
            if (!string.IsNullOrEmpty(colorIds))
            {
                colorIdValues = colorIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.UpdateProduct(files, id, ids,brandId,colorIdValues, code, name, isNew, price, shortDescription, detailDescription);
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

        #region Brands
        public ActionResult BrandListIndex()
        {
            return View();
        }
        public ActionResult BrandList()
        {
            var brands = _brandService.GetBrands();
            var brandsModal = AutoMapper.Mapper.Map<List<ProductBrandViewModel>>(brands);
            return Json(new { data = brandsModal }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateBrand(string brandName)
        {
            var result = _brandService.CreateBrand(brandName);
            return Json(new { result });
        }
        public ActionResult EditBrand(int brandId, string brandName)
        {
            var result = _brandService.UpdateBrand(brandId, brandName);
            return Json(new { result });
        }
        public ActionResult DeleteBrand(int brandId)
        {
            var result = _brandService.DeleteBrand(brandId);
            return Json(new { result });
        }

        #endregion

        #region Color

        public ActionResult ColorListIndex()
        {
            return View();
        }
        public ActionResult ColorList()
        {
            var colors = _colorService.GetColors();
            var colorsModel = AutoMapper.Mapper.Map<List<ColorViewModel>>(colors);
            return Json(new { data = colorsModel }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateColor(string name,string value)
        {
            var result = _colorService.CreateColor(name,value);
            return Json(new { result });
        }
        public ActionResult EditColor(int colorId, string name,string value)
        {
            var result = _colorService.UpdateColor(colorId, name,value);
            return Json(new { result });
        }
        public ActionResult DeleteColor(int colorId)
        {
            var result = _colorService.DeleteColor(colorId);
            return Json(new { result });
        }
        #endregion
    }
}
