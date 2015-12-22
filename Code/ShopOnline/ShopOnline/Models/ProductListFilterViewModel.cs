using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Constants;

namespace ShopOnline.Models
{
    public class ProductListFilterViewModel
    {
        public int? FilterCategoryId { get; set; }
        public int[] FilterBrandIds { get; set; }
        public int[] FilterColorIds { get; set; }
        public List<SelectListItem> Sorts { get; set; }
    }
}