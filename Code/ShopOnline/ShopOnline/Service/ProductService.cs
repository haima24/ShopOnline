using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using System.IO;

namespace ShopOnline.Service
{
    public class ProductService : BaseService
    {
        public List<Product> GetAllProduct()
        {
            var products = Context.Products.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate).ToList();
            return products;
        }
        public bool CreateProduct(List<int> categoryIds, IEnumerable<HttpPostedFileBase> files, string code, string name, decimal? price, string shortDescription, string detailDescription)
        {
            var product = new Product();
            product.ProductCode = code;
            product.ProductName = name;
            product.Price = price;
            product.ProductShortDescription = shortDescription;
            product.ProductDetailDescription = detailDescription;
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
            foreach (HttpPostedFileBase file in files)
            {
                var fName = file.FileName;
                var fNameIndex = fName.LastIndexOf('.');
                fName = fName.Insert(fNameIndex, "_" + DateTime.Now.Ticks.ToString());

                var folderPath = HttpContext.Current.Server.MapPath("~/images/product");
                string filePath = Path.Combine(folderPath, fName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));
                var productImage = new ProductImage();
                productImage.ProductImageUrl = "~/images/product/" + fName;
                product.ProductImages.Add(productImage);
            }
            if (categoryIds != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    var productCategory = new ProductCategory();
                    productCategory.CategoryId = categoryId;
                    product.ProductCategories.Add(productCategory);
                }

            }
            Context.Products.Add(product);
            Context.SaveChanges();
            return product.ProductId > 0;
        }
        public bool UpdateProduct(IEnumerable<HttpPostedFileBase> files, int id, List<int> categoryIds, string code, string name, decimal? price, string shortDescription, string detailDescription)
        {
            var product = Context.Products.FirstOrDefault(x => x.ProductId == id);
            var result = false;
            if (product != null)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    var fName = file.FileName;
                    var fNameIndex = fName.LastIndexOf('.');
                    fName = fName.Insert(fNameIndex, "_" + DateTime.Now.Ticks.ToString());

                    var folderPath = HttpContext.Current.Server.MapPath("~/images/product");
                    string filePath = Path.Combine(folderPath, fName);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));
                    var productImage = new ProductImage();
                    productImage.ProductImageUrl = "~/images/product/" + fName;
                    product.ProductImages.Add(productImage);
                }
                if (categoryIds != null)
                {
                    var currentCategoryIds = product.ProductCategories.Select(x => x.CategoryId).ToList();
                    foreach (var categoryId in categoryIds)
                    {
                        if (!currentCategoryIds.Contains(categoryId))
                        {
                            var productCategory = new ProductCategory();
                            productCategory.CategoryId = categoryId;
                            productCategory.ProductId = id;
                            product.ProductCategories.Add(productCategory);
                        }
                    }
                    foreach (var currentCategoryId in currentCategoryIds)
                    {
                        if (!categoryIds.Contains(currentCategoryId))
                        {
                            var productCategory =
                                product.ProductCategories.FirstOrDefault(x => x.CategoryId == currentCategoryId);
                            if (productCategory != null)
                            {
                                product.ProductCategories.Remove(productCategory);
                                Context.ProductCategories.Remove(productCategory);
                            }
                        }
                    }
                }

                product.ProductCode = code;
                product.ProductName = name;
                product.Price = price;
                product.ProductShortDescription = shortDescription;
                product.ProductDetailDescription = detailDescription;
                product.UpdatedDate = DateTime.Now;
                result = Context.SaveChanges() > 0;
            }
            return result;
        }
        public bool DeleteProduct(int id)
        {
            var product = Context.Products.FirstOrDefault(x => x.ProductId == id);
            var result = false;
            if (product != null)
            {
                var count = product.ProductImages.Count;
                var arrayProductImage = product.ProductImages.ToArray();
                for (int i = 0; i < count; i++)
                {
                    product.ProductImages.Remove(arrayProductImage[i]);
                    Context.ProductImages.Remove(arrayProductImage[i]);
                }
                count = product.ProductCategories.Count;
                var arrayProductCategories = product.ProductCategories.ToArray();
                for (int i = 0; i < count; i++)
                {
                    product.ProductCategories.Remove(arrayProductCategories[i]);
                    Context.ProductCategories.Remove(arrayProductCategories[i]);
                }
                Context.Products.Remove(product);
                result = Context.SaveChanges() > 0;
            }
            return result;
        }
        public Product GetProductById(int id)
        {
            var product = new Product();
            var pro = Context.Products.FirstOrDefault(x => x.ProductId == id);
            if (pro != null)
            {
                product = pro;
            }
            return product;
        }

        public List<Product> GetProductsByCondition(int page, int pageSize, out bool isLastPage, int? categoryId)
        {
            isLastPage = !Context.Products
                .Where(delegate(Product p)
                           {
                               if (!categoryId.HasValue)
                               {
                                   return true;
                               }
                               else
                               {
                                   return p.ProductCategories.Select(k => k.CategoryId).Contains(categoryId.Value);
                               }
                           }).Skip((page + 1)*pageSize).Take(pageSize).Any();
            var products = Context.Products
                .Where(delegate(Product p)
                           {
                               if (!categoryId.HasValue)
                               {
                                   return true;
                               }
                               else
                               {
                                   return p.ProductCategories.Select(k => k.CategoryId).Contains(categoryId.Value);
                               }
                           }).Skip((page) * pageSize)
                       .Take(pageSize); 
            return products.ToList();
        }
        public bool DeleteProductImage(int productImageId)
        {
            var result = false;
            var productImage = Context.ProductImages.FirstOrDefault(x => x.ProductImageId == productImageId);
            if (productImage != null)
            {
                Context.ProductImages.Remove(productImage);
                result = Context.SaveChanges() > 0;
            }
            return result;
        }
    }
}