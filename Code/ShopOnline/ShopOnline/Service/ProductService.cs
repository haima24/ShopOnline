using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using System.IO;
using ShopOnline.Constants;

namespace ShopOnline.Service
{
    public class ProductService : BaseService
    {
        public List<Product> GetAllProduct()
        {
            var products = Context.Products.OrderByDescending(x => x.UpdatedDate).ThenByDescending(x => x.CreatedDate).ToList();
            return products;
        }
        public List<Product> GetSixProducts()
        {
            var products = Context.Products.OrderByDescending(x => x.UpdatedDate).ThenByDescending(x => x.CreatedDate).Take(6).ToList();
            return products;
        }

        public bool CreateProduct(List<int> categoryIds,int? brandId,List<int> colorIds, IEnumerable<HttpPostedFileBase> files, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var product = new Product();
            product.ProductCode = code;
            product.ProductName = name;
            product.Price = price;
            product.BrandId = brandId;
            product.ProductShortDescription = shortDescription;
            product.ProductDetailDescription = detailDescription;
            product.CreatedDate = DateTime.Now;
            product.UpdatedDate = DateTime.Now;
            product.IsNew = isNew;
            if(files==null)
            {
                files=new List<HttpPostedFileBase>();
            }
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
            if (colorIds != null)
            {
                foreach (var colorId in colorIds)
                {
                    var productColor = new ProductColor();
                    productColor.ColorId = colorId;
                    product.ProductColors.Add(productColor);
                }

            }
            Context.Products.Add(product);
            Context.SaveChanges();
            return product.ProductId > 0;
        }
        public bool UpdateProduct(IEnumerable<HttpPostedFileBase> files, int id, List<int> categoryIds, int? brandId, List<int> colorIds, string code, string name, bool isNew, decimal? price, string shortDescription, string detailDescription)
        {
            var product = Context.Products.FirstOrDefault(x => x.ProductId == id);
            var result = false;
            if (product != null)
            {
                if (files == null)
                {
                    files = new List<HttpPostedFileBase>();
                }
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
                if (colorIds != null)
                {
                    var currentColorIds = product.ProductColors.Select(x => x.ColorId).ToList();
                    foreach (var colorId in colorIds)
                    {
                        if (!currentColorIds.Contains(colorId))
                        {
                            var productColor = new ProductColor();
                            productColor.ColorId = colorId;
                            productColor.ProductId = id;
                            product.ProductColors.Add(productColor);
                        }
                    }
                    foreach (var currentColorId in currentColorIds)
                    {
                        if (!colorIds.Contains(currentColorId))
                        {
                            var productColor =
                                product.ProductColors.FirstOrDefault(x => x.ColorId == currentColorId);
                            if (productColor != null)
                            {
                                product.ProductColors.Remove(productColor);
                                Context.ProductColors.Remove(productColor);
                            }
                        }
                    }
                }
                product.BrandId = brandId;
                product.ProductCode = code;
                product.ProductName = name;
                product.IsNew = isNew;
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
                if(product.OrderDetails.Count > 0)
                {
                    result = false;
                    return result;
                }
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
                count = product.ProductColors.Count;
                var arrayProductColors = product.ProductColors.ToArray();
                for (int i = 0; i < count; i++)
                {
                    product.ProductColors.Remove(arrayProductColors[i]);
                    Context.ProductColors.Remove(arrayProductColors[i]);
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

        public List<Product> GetProductsByCondition(int page, int pageSize, out bool isLastPage, int? categoryId,int? parentCategoryId,List<int> brandIds,List<int> colorIds,Sorts? sortCondition )
        {
            Func<Product, bool> whereClause = delegate(Product p)
                                  {
                                      
                                      if (!categoryId.HasValue)
                                      {
                                          if (!parentCategoryId.HasValue)
                                          {
                                              return true;
                                          }
                                          else
                                          {
                                              return
                                                  p.ProductCategories.Select(x => x.Category.ParentCategoryId).Contains(
                                                      parentCategoryId.Value);
                                          }
                                      }
                                      else
                                      {
                                          return p.ProductCategories.Select(k => k.CategoryId).Contains(categoryId.Value);
                                      }
                                  };
            Func<Product, bool> whereBrandClause = delegate(Product p)
            {
                if(brandIds!=null&&brandIds.Count!=0)
                {
                    if(p.BrandId.HasValue)
                    {
                        return brandIds.Contains(p.BrandId.Value);
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                else
                {
                    return true;
                }

            };
            Func<Product, bool> whereColorsClause = delegate(Product p)
                                                        {
                                                            if(colorIds!=null)
                                                            {
                                                                if(colorIds.Count==0)
                                                                {
                                                                    return true;
                                                                }
                                                                else
                                                                {
                                                                    var productColors = p.ProductColors.Select(x => x.ColorId).Distinct().ToList();
                                                                    return productColors.Any(colorIds.Contains);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return true;
                                                            }
                                                           
                                                        };
            var productsList = Context.Products
                .Where(whereClause).Where(whereBrandClause).Where(whereColorsClause);
            if(sortCondition.HasValue)
            {
                switch (sortCondition.Value)
                {
                    case Sorts.New:
                        productsList =
                            productsList.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate);
                        break;
                    case Sorts.Old:
                        productsList =
                           productsList.OrderBy(x => x.CreatedDate).ThenBy(x => x.UpdatedDate);
                        break;
                    case Sorts.BestSell:
                        productsList = productsList.OrderBy(x =>
                                                                {
                                                                    var countInOrder = 0;
                                                                    if(x.OrderDetails!=null)
                                                                    {
                                                                        countInOrder =
                                                                            x.OrderDetails.Sum(k => k.Quantity);
                                                                    }
                                                                    return countInOrder;
                                                                });
                        break;
                    case Sorts.DescPrice:
                        productsList = productsList.OrderByDescending(x => x.Price);
                        break;
                    case Sorts.AscPrice:
                        productsList = productsList.OrderBy(x => x.Price);
                        break;
                    case Sorts.Az:
                        productsList = productsList.OrderBy(x => x.ProductName);
                        break;
                    case Sorts.Za:
                        productsList = productsList.OrderByDescending(x => x.ProductName);
                        break;
                }
            }
            else
            {
                productsList =
                           productsList.OrderByDescending(x => x.CreatedDate).ThenByDescending(x => x.UpdatedDate);
            }
            var products = productsList.Skip((page) * pageSize)
                      .Take(pageSize);
            isLastPage = !Context.Products
                .Where(whereClause).Where(whereBrandClause).Where(whereColorsClause).Skip((page + 1) * pageSize).Take(pageSize).Any();
            return products.ToList();
        }
        public bool DeleteProductImage(int productImageId)
        {
            var result = false;
            var productImage = Context.ProductImages.FirstOrDefault(x => x.ProductImageId == productImageId);
            if (productImage != null)
            {

                //remove old file
                var oldPath = HttpContext.Current.Server.MapPath(productImage.ProductImageUrl);
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
                Context.ProductImages.Remove(productImage);

                result = Context.SaveChanges() > 0;
            }
            return result;
        }
    }
}