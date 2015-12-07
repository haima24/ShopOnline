using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;

namespace ShopOnline.Models
{
    public class OrderDetailViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice
        {
            get { return (Product.Price ?? 0)*Quantity; }
        }
    }
}