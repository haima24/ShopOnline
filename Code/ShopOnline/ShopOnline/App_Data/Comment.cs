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
    
    public partial class Comment
    {
        public Comment()
        {
            this.ChildsComment = new HashSet<Comment>();
        }
    
        public int CommentId { get; set; }
        public string CommentDetail { get; set; }
        public System.DateTime CommentDate { get; set; }
        public int CommentUserId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> ParentCommentId { get; set; }
    
        public virtual ICollection<Comment> ChildsComment { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
