using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ProductInfoViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<ProductBrandViewModel> Brands { get; set; }
        public IEnumerable<ColorViewModel> Colors { get; set; } 
    }
}