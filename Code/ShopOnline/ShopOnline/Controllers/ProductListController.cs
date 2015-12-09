using ShopOnline.Models;
using ShopOnline.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Extension;
namespace ShopOnline.Controllers
{
    public class ProductListController : BaseFrontController
    {
        private readonly CategoryService _categoryService = null;
        private readonly ProductService _productService = null;


        public ProductListController()
        {
            _categoryService = new CategoryService();
            _productService=new ProductService();
        }
      
        public ActionResult Index(ProductListFilterViewModel filterModel)
        {
            
            return View(filterModel);
        }
        public ActionResult GetProducts(int page,int pageSize,int? categoryId)
        {
            var isLastPage = false;
            var products = _productService.GetProductsByCondition(page, pageSize, out isLastPage, categoryId);
            var productsModel = AutoMapper.Mapper.Map<List<ProductViewModel>>(products);
            var html = this.RenderPartialViewToString("GetProducts", productsModel);
            return Json(new {isLastPage, html});
        }

        public ActionResult GetCategoryList()
        {
            var categories = _categoryService.GetParentCategories()
               .Select(x =>
               {
                   var categoryViewModel = new CategoryViewModel();
                   categoryViewModel.CategoryId = x.CategoryId;
                   categoryViewModel.CategoryName = x.CategoryName;
                   categoryViewModel.Childs =
                       x.Childs.Where(k=>k.ProductCategories.Any()).Select(k => new CategoryViewModel() { CategoryId = k.CategoryId, CategoryName = k.CategoryName }).ToList();
                   categoryViewModel.ProductCount = x.Childs.SelectMany(k => k.ProductCategories.Select(o=>o.ProductId)).Distinct().Count();
                   return categoryViewModel;
               });
            return PartialView(categories);
        }
        public ActionResult Detail(int id)
        {
            var product = _productService.GetProductById(id);
            var productModel = AutoMapper.Mapper.Map<ProductViewModel>(product);
            return View(productModel);
        }
    }
}
