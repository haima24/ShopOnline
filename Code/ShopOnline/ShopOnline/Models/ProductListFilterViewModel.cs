using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ProductListFilterViewModel
    {
        public int? FilterCategoryId { get; set; }
        public int[] FilterBrandIds { get; set; }
        public int[] FilterColorIds { get; set; }
    }
}