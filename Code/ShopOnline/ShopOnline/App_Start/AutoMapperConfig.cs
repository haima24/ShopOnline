using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Models;

namespace ShopOnline.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<ProductImage, ProductImageViewModel>();
            AutoMapper.Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            AutoMapper.Mapper.CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.OrderDetails,
                       opts => opts.MapFrom(s => s.OrderDetails))
                .ForMember(dest => dest.ShippingAddress,
                           opts => opts.ResolveUsing(s =>
                                                    {
                                                        var address = s.ShippingAddress;
                                                        if (s.LocationDistrict != null)
                                                        {
                                                            address += ", " + s.LocationDistrict.LocationName;
                                                        }
                                                        if (s.LocationCity != null)
                                                        {
                                                            address += ", " + s.LocationCity.LocationName;
                                                        }
                                                        return address;
                                                    }));
            AutoMapper.Mapper.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductImageDisplay,
                       opts => opts.MapFrom(s => s.ProductImages.FirstOrDefault()))
                .ForMember(dest => dest.ProductImages,
                       opts => opts.MapFrom(s => s.ProductImages))
             .ForMember(dest => dest.ProductCategories,
                       opts => opts.MapFrom(s => s.ProductCategories.Select(x => x.CategoryId)));

        }
    }
}