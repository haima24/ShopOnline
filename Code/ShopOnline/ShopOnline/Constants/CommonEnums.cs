using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ShopOnline.Constants
{
    public enum DeliveryMethods
    {
        Shipping
    }
    public enum PaymentMethods
    {
        CashOnDelivery
    }

    public enum Sorts
    {
        [Description("Sản Phẩm Mới Nhất")]
        New,
        [Description("Sản Phẩm Cũ Nhất")]
        Old,
        [Description("Sản Phẩm Bán Chạy Nhất")]
        BestSell,
        [Description("Giá Từ Cao Đến Thấp")]
        DescPrice,
        [Description("Giá Từ Thấp Đến Cao")]
        AscPrice,
        [Description("Sắp Xếp Từ A - Z")]
        Az,
        [Description("Sắp Xếp Từ Z - A")]
        Za

    }

}