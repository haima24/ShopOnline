using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;

namespace ShopOnline.Service
{
    public class BrandService : BaseService
    {
        public List<ProductBrand>  GetBrands()
        {
            return Context.ProductBrands.OrderByDescending(x=>x.UpdatedDate).ThenByDescending(x=>x.CreatedDate).ToList();
        }
        public bool CreateBrand(string name)
        {
            var brand = new ProductBrand();
            brand.BrandName = name;
            brand.CreatedDate = DateTime.Now;
            brand.UpdatedDate = DateTime.Now;
            Context.ProductBrands.Add(brand);
            Context.SaveChanges();
            return brand.BrandId > 0;
        }
        public bool UpdateBrand(int brandId,string name)
        {
            var brand = Context.ProductBrands.FirstOrDefault(x => x.BrandId == brandId);
            var result = 0;
            if (brand != null)
            {
                brand.BrandName = name;
                brand.UpdatedDate = DateTime.Now;
                result = Context.SaveChanges();
            }
            return brand != null && result > 0;
        }
        public bool DeleteBrand(int id)
        {
            var result = 0;
            var brand = Context.ProductBrands.FirstOrDefault(x => x.BrandId == id);
            if (brand != null)
            {
                Context.ProductBrands.Remove(brand);
                result = Context.SaveChanges();
            }
            return result > 0;
        }
    }
}