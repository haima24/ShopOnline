using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Constants;

namespace ShopOnline.Models
{
    public class CartViewModel
    {
        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public int ItemsCount
        {
            get
            {
                var result = 0;
                if(OrderDetails!=null)
                {
                    result= OrderDetails.Select(x => x.Quantity).Sum();
                }
                return result;
            }
        }
        public decimal? Total
        {
            get
            {
                decimal? result=0;
                if (OrderDetails != null)
                {
                    result = OrderDetails.Select(x => x.TotalPrice).Sum();
                }
                return result;
            }
        }

        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Street { get; set; }
        public string Email { get; set; }
        public DeliveryMethods Delivery { get; set; }
        public PaymentMethods Payment { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public List<SelectListItem> CitySource { get; set; }
        public List<SelectListItem> DistrictSource { get; set; }
    }
}