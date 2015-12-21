//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.App_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public Product()
        {
            this.ProductImages = new HashSet<ProductImage>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ProductColors = new HashSet<ProductColor>();
            this.Comments = new HashSet<Comment>();
        }
    
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string ProductShortDescription { get; set; }
        public string ProductDetailDescription { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<decimal> SaleOffPrice { get; set; }
        public Nullable<bool> IsSaleOff { get; set; }
        public Nullable<bool> IsNew { get; set; }
        public Nullable<int> BrandId { get; set; }
    
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ProductBrand ProductBrand { get; set; }
        public virtual ICollection<ProductColor> ProductColors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
