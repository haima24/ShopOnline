using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ProductBrandViewModel
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ProductCount { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}