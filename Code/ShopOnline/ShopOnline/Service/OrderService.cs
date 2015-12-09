using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using System.IO;
using ShopOnline.Constants;
using ShopOnline.Models;

namespace ShopOnline.Service
{
    public class OrderService : BaseService
    {
       public bool CreateOrder(int? userId,CartViewModel cart)
       {
           var order = new Order();
           order.UserId = userId;
           order.ShippingAddress = cart.Street;
           order.ShippingTelephone = cart.Telephone;
           order.OrderDetails=new List<OrderDetail>();
           foreach (var oDetail in cart.OrderDetails)
           {
               var orderDetail = new OrderDetail();
               orderDetail.ProductId = oDetail.Product.ProductId;
               orderDetail.Price = oDetail.Product.Price??0;
               orderDetail.Quantity = oDetail.Quantity;
               order.OrderDetails.Add(orderDetail);
           }
           order.LocationCityId = cart.CityId;
           order.LocationDistrictId = cart.DistrictId;
           order.OrderStatus = Common.OrderStatusNew;
           order.CreatedDate = DateTime.Now;
           order.UpdatedDate = DateTime.Now;
           Context.Orders.Add(order);
           Context.SaveChanges();
           return order.OrderId > 0;
       }
       public Order GetOrder(int orderid)
       {
           var order = new Order();
           order.OrderDetails=new List<OrderDetail>();
           var orderDb = Context.Orders.FirstOrDefault(x => x.OrderId == orderid);
           if(orderDb!=null)
           {
               order = orderDb;
           }
           return order;
       }
        public List<Order> GetOrdersNew()
        {
            var orders =
                Context.Orders.Where(x => x.OrderStatus == Common.OrderStatusNew).OrderByDescending(x => x.UpdatedDate).
                    ThenByDescending(x => x.CreatedDate);
            return orders.ToList();
        }
        public List<Order> GetOrdersProcessing()
        {
            var orders =
                Context.Orders.Where(x => x.OrderStatus == Common.OrderStatusProcessing).OrderByDescending(x => x.UpdatedDate).
                    ThenByDescending(x => x.CreatedDate);
            return orders.ToList();
        }
        public List<Order> GetOrdersProcessed()
        {
            var orders =
                Context.Orders.Where(x => x.OrderStatus == Common.OrderStatusProcessed).OrderByDescending(x => x.UpdatedDate).
                    ThenByDescending(x => x.CreatedDate);
            return orders.ToList();
        }
        public List<Order> GetOrdersDisabled()
        {
            var orders =
                Context.Orders.Where(x => x.OrderStatus == Common.OrderStatusDisabled).OrderByDescending(x => x.UpdatedDate).
                    ThenByDescending(x => x.CreatedDate);
            return orders.ToList();
        }
        public bool UpdateOrderStatus(int orderId,int status)
        {
            var order = Context.Orders.FirstOrDefault(x => x.OrderId == orderId);
            var result = false;
            if(order!=null)
            {
                order.OrderStatus = status;
                result = Context.SaveChanges() > 0;
            }
            
            return result;
        }
    }
}