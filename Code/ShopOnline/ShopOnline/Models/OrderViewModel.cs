using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopOnline.Models
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string ShippingAddress { get; set; }
        public Nullable<int> UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ShippingTelephone { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public Nullable<int> LocationCityId { get; set; }
        public Nullable<int> LocationDistrictId { get; set; }
        public Nullable<int> OrderStatus { get; set; }
        public List<SelectListItem> CitySource { get; set; }
        public List<SelectListItem> DistrictSource { get; set; }
        public string OriginalAddress { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public decimal? Total
        {
            get
            {
                decimal? result = 0;
                if (OrderDetails != null)
                {
                    result = OrderDetails.Select(x => x.TotalPrice).Sum();
                }
                return result;
            }
        }

     
    }
}