using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Extension;

namespace ShopOnline.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public bool? IsNew { get; set; }
        public decimal? Price { get; set; }
        public string PriceString { get { return Price.ToSystemFormat(); } }
        public string ProductShortDescription { get; set; }
        public string ProductDetailDescription { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<ProductImageViewModel> ProductImages { get; set; }
        public List<int> ProductCategories { get; set; }
        public ProductImageViewModel ProductImageDisplay { get; set; }
        public List<int> ProductColors { get; set; }
        public List<string> ColorCodes { get; set; } 
        public int? BrandId { get; set; }
        public List<CommentViewModel> Comments { get; set; }
    }
}