using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Constants;
using ShopOnline.Models;

namespace ShopOnline.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Comment, CommentViewModel>()
                  .ForMember(dest => dest.ChildsComment,
                       opts => opts.MapFrom(s => s.ChildsComment.OrderByDescending(x=>x.CommentDate)))
                 .ForMember(dest => dest.CommentUserName,
                       opts => opts.ResolveUsing(s =>
                                                     {
                                                         var name = s.User.UserName;
                                                         if(!string.IsNullOrEmpty(s.User.RealName))
                                                         {
                                                             name = s.User.RealName;
                                                         }
                                                         return name;
                                                     }));
            AutoMapper.Mapper.CreateMap<ProductBrand, ProductBrandViewModel>()
                 .ForMember(dest => dest.ProductCount,
                       opts => opts.MapFrom(s => s.Products.Count));
            AutoMapper.Mapper.CreateMap<Color, ColorViewModel>()
                 .ForMember(dest => dest.ProductCount,
                       opts => opts.MapFrom(s => s.ProductColors.Count));
            AutoMapper.Mapper.CreateMap<ProductImage, ProductImageViewModel>();
            AutoMapper.Mapper.CreateMap<OrderDetail, OrderDetailViewModel>();
            AutoMapper.Mapper.CreateMap<Location, LocationViewModel>();
            AutoMapper.Mapper.CreateMap<Order, OrderViewModel>()
                 .ForMember(dest => dest.OriginalAddress,
                       opts => opts.MapFrom(s => s.ShippingAddress))
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
                       opts => opts.ResolveUsing(s =>
                                                     {
                                                         var pImage = s.ProductImages.FirstOrDefault();
                                                         if (pImage != null)
                                                         {
                                                             return pImage;
                                                         }
                                                         else
                                                         {
                                                             return new ProductImage()
                                                                        {
                                                                            ProductId = 0,
                                                                            ProductImageUrl = Common.CommingSoonImage
                                                                        };
                                                         }
                                                     }))
                                                     .ForMember(dest => dest.ColorCodes,
                       opts => opts.MapFrom(s => s.ProductColors.Select(x => x.Color.ColorValue)))
                .ForMember(dest => dest.Comments,
                       opts => opts.MapFrom(s => s.Comments.OrderByDescending(x=>x.CommentDate)))
                       .ForMember(dest => dest.ProductImages,
                       opts => opts.MapFrom(s => s.ProductImages))
                        .ForMember(dest => dest.ProductColors,
                       opts => opts.MapFrom(s => s.ProductColors.Select(x => x.ColorId)))

             .ForMember(dest => dest.ProductCategories,
                       opts => opts.MapFrom(s => s.ProductCategories.Select(x => x.CategoryId)));

        }
    }
}