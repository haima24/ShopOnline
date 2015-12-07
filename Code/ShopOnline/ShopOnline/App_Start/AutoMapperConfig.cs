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
            AutoMapper.Mapper.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.ProductImageDisplay,
                       opts => opts.MapFrom(s => s.ProductImages.FirstOrDefault()))
                .ForMember(dest => dest.ProductImages,
                       opts => opts.MapFrom(s => s.ProductImages))
             .ForMember(dest => dest.ProductCategories,
                       opts => opts.MapFrom(s=>s.ProductCategories.Select(x=>x.CategoryId)));
                
        }
    }
}