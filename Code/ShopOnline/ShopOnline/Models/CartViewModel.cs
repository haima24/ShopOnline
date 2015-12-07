using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}