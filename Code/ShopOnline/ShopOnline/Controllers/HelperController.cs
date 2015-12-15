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
        private readonly BrandService _brandService = null;
        private readonly ColorService _colorService = null;
        public HelperController()
        {
            _brandService=new BrandService();
            _configService = new ConfigService();
            _productService=new ProductService();
            _colorService=new ColorService();
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
        public ActionResult RenderBrands()
        {
            var brands = _brandService.GetBrands();
            var brandsModel = AutoMapper.Mapper.Map<List<ProductBrandViewModel>>(brands);
            return PartialView(brandsModel);
        }
        public ActionResult RenderColors()
        {
            var colors = _colorService.GetColors();
            var colorsModel = AutoMapper.Mapper.Map<List<ColorViewModel>>(colors);
            return PartialView(colorsModel);
        }
    }
}
