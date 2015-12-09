using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class HelperController : BaseFrontController
    {
        private readonly ConfigService _configService = null;
        private readonly ProductService _productService = null;
        public HelperController()
        {
            _configService = new ConfigService();
            _productService=new ProductService();
        }

        public ActionResult RenderLoginBar()
        {
            return PartialView(new LoginViewModel{IsUserLoggedIn = IsUserLoggedIn,UserId=UserId,UserName=UserName});
        }
        public ActionResult GetConfigByCode(string code)
        {
            var config = _configService.GetConfig(code);
            return Content(config.ConfigValue);
        }
        public ActionResult GetSixImages()
        {
            var products = _productService.GetSixProducts();
            var productsModel = AutoMapper.Mapper.Map<List<ProductViewModel>>(products);
            return PartialView(productsModel);
        }
    }
}
