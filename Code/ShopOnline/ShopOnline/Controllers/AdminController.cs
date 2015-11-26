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

        public AdminController()
        {
            _categoryService=new CategoryService();
        }

        public ActionResult Index()
        {
            return RedirectToAction("Categories");
        }
        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult GetParentCategories()
        {
            var categories = _categoryService.GetParentCategories().Select(x => new CategoryViewModel() { CategoryId = x.CategoryId, CategoryName = x.CategoryName });
            return Json(new {data = categories},JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateParentCategory(string categoryName)
        {
            var result = _categoryService.CreateCategory(categoryName);
            return Json(new { result });
        }
        public ActionResult EditParentCategory(int id, string categoryName)
        {
            var result = _categoryService.UpdateCategory(id, categoryName);
            return Json(new { result });
        }
        public ActionResult DeleteParentCategory(int id)
        {
            var result = _categoryService.DeleteCategory(id);
            return Json(new { result });
        }
    }
}
