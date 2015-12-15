using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    public class ColorViewModel
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public string ColorValue { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int ProductCount { get; set; }
    }
}