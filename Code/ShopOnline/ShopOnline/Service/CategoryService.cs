using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;

namespace ShopOnline.Service
{
    public class CategoryService:BaseService
    {
        public List<Category>  GetParentCategories()
        {
            return Context.Categories.Where(x=>x.CategoryLevel==0).OrderByDescending(x=>x.CreatedDate).ThenByDescending(x=>x.UpdatedDate).ToList();
        }
        public List<Category> GetSubCategories()
        {
            return Context.Categories.Where(x => x.CategoryLevel == 1).OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
        }
        public bool UpdateParentCategory(int id,string name)
        {
            var category = Context.Categories.FirstOrDefault(x => x.CategoryId == id);
            var result = 0;
            if(category!=null)
            {
                category.CategoryName = name;
                category.UpdatedDate = DateTime.Now;
                result=Context.SaveChanges();
            }
            return category!=null&&result>0;
        }
        public bool UpdateSubCategory(int id,int parentId, string name)
        {
            var category = Context.Categories.FirstOrDefault(x => x.CategoryId == id);
            var result = 0;
            if (category != null)
            {
                category.CategoryName = name;
                category.ParentCategoryId = parentId;
                category.UpdatedDate = DateTime.Now;
                result = Context.SaveChanges();
            }
            return category != null && result > 0;
        }
        public bool CreateParentCategory(string name)
        {
            var category = new Category();
            category.CategoryName = name;
            category.CategoryLevel = 0;
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            category.CreatedUserId = 1; //todo change user
            Context.Categories.Add(category);
            Context.SaveChanges();
            return category.CategoryId > 0;
        }
        public bool CreateSubCategory(int parentId,string name)
        {
            var category = new Category();
            category.ParentCategoryId = parentId;
            category.CategoryName = name;
            category.CategoryLevel = 1;
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            category.CreatedUserId = 1; //todo change user
            Context.Categories.Add(category);
            Context.SaveChanges();
            return category.CategoryId > 0;
        }
        public bool DeleteCategory(int id)
        {
            var result = 0;
            var category = Context.Categories.FirstOrDefault(x => x.CategoryId == id);
            if(category!=null)
            {
                Context.Categories.Remove(category);
                result = Context.SaveChanges();
            }
            return result > 0;
        }
        public Category GetCategory(int id)
        {
            var category = Context.Categories.FirstOrDefault(x => x.CategoryId == id);
            return category;
        }
    }
}