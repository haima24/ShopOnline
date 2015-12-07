using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;
using ShopOnline.Service;

namespace ShopOnline.Controllers
{
    public class AdminController : Controller
    {
        private readonly CategoryService _categoryService = null;
        private readonly ProductService _productService = null;

        public AdminController()
        {
            _categoryService=new CategoryService();
            _productService=new ProductService();
        }

        #region Categories
        public ActionResult Index()
        {
            return RedirectToAction("CategoriesParent");
        }
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
        public ActionResult CreateProduct(IEnumerable<HttpPostedFileBase> files,string categoryIds,string code,string name,decimal? price,string shortDescription,string detailDescription)
        {
            var ids = new List<int>();
            if(!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.CreateProduct(ids,files,code, name, price, shortDescription, detailDescription);
            return Json(new {result});
        }
        [ValidateInput(false)]
        public ActionResult UpdateProduct(IEnumerable<HttpPostedFileBase> files,int id,string categoryIds,string code, string name, decimal? price, string shortDescription, string detailDescription)
        {
            var ids = new List<int>();
            if (!string.IsNullOrEmpty(categoryIds))
            {
                ids = categoryIds.Split(',').Select(int.Parse).ToList();
            }
            var result = _productService.UpdateProduct(files, id, ids, code, name, price, shortDescription, detailDescription);
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
           return Json(new {result});
       }

        #endregion
    }
}
